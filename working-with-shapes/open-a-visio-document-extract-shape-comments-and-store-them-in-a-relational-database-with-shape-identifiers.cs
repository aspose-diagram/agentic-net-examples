using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Database connection string (second argument or default)
        string connectionString = args.Length > 1 ? args[1] : "Data Source=.;Initial Catalog=VisioComments;Integrated Security=True";

        // Load the Visio document inside a try/catch to capture Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio file: {ex.Message}");
            return;
        }

        // Ensure the target table exists (simple schema)
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string createTableSql = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ShapeComments' AND xtype='U')
CREATE TABLE ShapeComments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DiagramPath NVARCHAR(260),
    PageName NVARCHAR(100),
    ShapeId INT,
    CommentText NVARCHAR(MAX),
    ReviewerId INT
);";
                using (SqlCommand cmd = new SqlCommand(createTableSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Database setup error: {ex.Message}");
            return;
        }

        // Iterate each page and extract annotations (comments)
        foreach (Page page in diagram.Pages)
        {
            // Retrieve the page's universal name for reporting
            string pageName = page.NameU;

            // Access the collection of annotations via the page's PageSheet
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // Extract the shape identifier the comment is attached to
                int shapeId = annotation.ShapeID;
                // Extract the comment text (use .Value to get the string)
                string commentText = annotation.Comment.Value;
                // Extract the reviewer identifier (author index)
                int reviewerId = annotation.ReviewerID.Value;

                // Insert the comment record into the database
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string insertSql = @"
INSERT INTO ShapeComments (DiagramPath, PageName, ShapeId, CommentText, ReviewerId)
VALUES (@DiagramPath, @PageName, @ShapeId, @CommentText, @ReviewerId);";
                        using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                        {
                            // Parameterize to avoid SQL injection and handle special characters
                            cmd.Parameters.Add("@DiagramPath", SqlDbType.NVarChar, 260).Value = visioPath;
                            cmd.Parameters.Add("@PageName", SqlDbType.NVarChar, 100).Value = pageName;
                            cmd.Parameters.Add("@ShapeId", SqlDbType.Int).Value = shapeId;
                            cmd.Parameters.Add("@CommentText", SqlDbType.NVarChar).Value = commentText;
                            cmd.Parameters.Add("@ReviewerId", SqlDbType.Int).Value = reviewerId;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error inserting comment for shape {shapeId} on page '{pageName}': {ex.Message}");
                    // Continue processing other comments despite the error
                }
            }
        }

        Console.WriteLine("Comment extraction and storage completed successfully.");
    }
}