using System.IO;
using System;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string visioPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Create an in‑memory table that mimics a relational database table for comments
            DataTable commentTable = new DataTable("Comments");
            commentTable.Columns.Add("CommentId", typeof(int));
            commentTable.Columns.Add("ShapeId", typeof(int));
            commentTable.Columns.Add("CommentText", typeof(string));
            commentTable.Columns.Add("ReviewerId", typeof(int));

            // Iterate through all pages and extract comment (annotation) data
            foreach (Page page in diagram.Pages)
            {
                // Annotations are stored at the page level
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    int commentId = annotation.MarkerIndex.Value;   // Unique identifier of the comment
                    int shapeId = annotation.ShapeID;               // ID of the shape the comment is attached to
                    string text = annotation.Comment.Value;        // Comment text
                    int reviewerId = annotation.ReviewerID.Value;  // Index of the reviewer who created the comment

                    // Add a row to the simulated table
                    commentTable.Rows.Add(commentId, shapeId, text, reviewerId);
                }
            }

            // Output the extracted data (simulating a DB insert)
            Console.WriteLine("Exported Comments:");
            foreach (DataRow row in commentTable.Rows)
            {
                Console.WriteLine($"CommentId: {row["CommentId"]}, ShapeId: {row["ShapeId"]}, Text: {row["CommentText"]}, ReviewerId: {row["ReviewerId"]}");
            }

            // -----------------------------------------------------------------
            // Real database insertion would use ADO.NET (e.g., SqlConnection).
            // The following is a placeholder illustrating how it could be done.
            // -----------------------------------------------------------------
            /*
            string connectionString = "your_connection_string";
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataRow row in commentTable.Rows)
                {
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Comments (CommentId, ShapeId, CommentText, ReviewerId) VALUES (@CommentId, @ShapeId, @CommentText, @ReviewerId)", connection))
                    {
                        command.Parameters.AddWithValue("@CommentId", row["CommentId"]);
                        command.Parameters.AddWithValue("@ShapeId", row["ShapeId"]);
                        command.Parameters.AddWithValue("@CommentText", row["CommentText"]);
                        command.Parameters.AddWithValue("@ReviewerId", row["ReviewerId"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            */

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
