using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Prepare an in‑memory DataTable to simulate relational database storage
            DataTable commentTable = new DataTable("Comments");
            commentTable.Columns.Add("CommentId", typeof(int));
            commentTable.Columns.Add("ShapeId", typeof(int));
            commentTable.Columns.Add("CommentText", typeof(string));
            commentTable.Columns.Add("ReviewerId", typeof(int));

            // Iterate through all pages and extract comment metadata
            foreach (Page page in diagram.Pages)
            {
                // Annotations (comments) are stored at the page level
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    // Extract required fields
                    int commentId = ann.MarkerIndex.Value;   // Unique identifier for the comment
                    int shapeId = ann.ShapeID;               // Associated shape ID (0 if not linked)
                    string commentText = ann.Comment.Value ?? string.Empty;
                    int reviewerId = ann.ReviewerID.Value;   // Author identifier

                    // Add a row to the DataTable
                    DataRow row = commentTable.NewRow();
                    row["CommentId"] = commentId;
                    row["ShapeId"] = shapeId;
                    row["CommentText"] = commentText;
                    row["ReviewerId"] = reviewerId;
                    commentTable.Rows.Add(row);
                }
            }

            // Simulate inserting rows into a relational database.
            // In a real scenario you would use ADO.NET (e.g., SqlConnection, SqlCommand) here.
            // Example (commented out because external DB drivers are not available):
            /*
            using (var connection = new System.Data.SqlClient.SqlConnection("your-connection-string"))
            {
                connection.Open();
                foreach (DataRow row in commentTable.Rows)
                {
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Comments (CommentId, ShapeId, CommentText, ReviewerId) VALUES (@CommentId, @ShapeId, @CommentText, @ReviewerId)", connection))
                    {
                        command.Parameters.AddWithValue("@CommentId", (int)row["CommentId"]);
                        command.Parameters.AddWithValue("@ShapeId", (int)row["ShapeId"]);
                        command.Parameters.AddWithValue("@CommentText", (string)row["CommentText"]);
                        command.Parameters.AddWithValue("@ReviewerId", (int)row["ReviewerId"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            */

            // For demonstration, output the extracted comment metadata to the console
            Console.WriteLine("Extracted comment metadata:");
            foreach (DataRow row in commentTable.Rows)
            {
                Console.WriteLine($"CommentId: {row["CommentId"]}, ShapeId: {row["ShapeId"]}, ReviewerId: {row["ReviewerId"]}, Text: {row["CommentText"]}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}