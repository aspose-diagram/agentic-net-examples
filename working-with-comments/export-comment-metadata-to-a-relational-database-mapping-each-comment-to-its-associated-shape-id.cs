using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file
        string visioPath = "input.vsdx";
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Database connection string (replace with actual values)
        string connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;";

        // Prepare the INSERT command
        const string insertSql = "INSERT INTO Comments (ShapeId, CommentText) VALUES (@ShapeId, @CommentText)";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    // Define parameters
                    SqlParameter shapeIdParam = command.Parameters.Add("@ShapeId", SqlDbType.Int);
                    SqlParameter commentParam = command.Parameters.Add("@CommentText", SqlDbType.NVarChar, -1);

                    // Iterate through all pages and their annotations (comments)
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve the associated shape ID and comment text
                            int shapeId = annotation.ShapeID;
                            string commentText = annotation.Comment.Value ?? string.Empty;

                            // Set parameter values and execute the insert
                            shapeIdParam.Value = shapeId;
                            commentParam.Value = commentText;

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            Console.WriteLine("Comment metadata export completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}