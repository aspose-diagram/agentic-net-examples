using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // In-memory representation of the relational database (simulated)
        DataTable commentTable = new DataTable("ShapeComments");
        commentTable.Columns.Add("ShapeID", typeof(int));
        commentTable.Columns.Add("Comment", typeof(string));

        try
        {
            // Load the Visio document
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access page-level annotations (comments)
                if (page.PageSheet != null && page.PageSheet.Annotations != null)
                {
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve the associated shape ID and comment text
                        int shapeId = ann.ShapeID; // ShapeID is an int
                        string commentText = ann.Comment?.Value ?? string.Empty;

                        // Add a row to the simulated database table
                        DataRow row = commentTable.NewRow();
                        row["ShapeID"] = shapeId;
                        row["Comment"] = commentText;
                        commentTable.Rows.Add(row);
                    }
                }
            }

            // Example: display extracted comments
            Console.WriteLine("Extracted Shape Comments:");
            foreach (DataRow row in commentTable.Rows)
            {
                Console.WriteLine($"ShapeID: {row["ShapeID"]}, Comment: {row["Comment"]}");
            }

            // -----------------------------------------------------------------
            // Real database insertion example (commented out):
            // -----------------------------------------------------------------
            // using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            // {
            //     connection.Open();
            //     foreach (DataRow row in commentTable.Rows)
            //     {
            //         using (var command = new System.Data.SqlClient.SqlCommand(
            //             "INSERT INTO ShapeComments (ShapeID, Comment) VALUES (@ShapeID, @Comment)", connection))
            //         {
            //             command.Parameters.AddWithValue("@ShapeID", (int)row["ShapeID"]);
            //             command.Parameters.AddWithValue("@Comment", (string)row["Comment"]);
            //             command.ExecuteNonQuery();
            //         }
            //     }
            // }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}