using System.IO;
using System;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // In‑memory table that mimics a relational database table for comments
            DataTable commentTable = new DataTable("ShapeComments");
            commentTable.Columns.Add("ShapeId", typeof(int));
            commentTable.Columns.Add("ShapeName", typeof(string));
            commentTable.Columns.Add("CommentId", typeof(long));
            commentTable.Columns.Add("CommentText", typeof(string));
            commentTable.Columns.Add("ReviewerId", typeof(int));

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Annotations (comments) are stored at the page level
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Extract comment details
                    long commentId = annotation.MarkerIndex.Value;
                    string commentText = annotation.Comment.Value;
                    int reviewerId = annotation.ReviewerID.Value;
                    int shapeId = annotation.ShapeID; // primitive int

                    // Try to obtain the shape name; the shape may have been deleted
                    string shapeName = null;
                    try
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);
                        shapeName = shape.NameU;
                    }
                    catch
                    {
                        // Shape not found – leave shapeName null
                    }

                    // Add a row to the in‑memory table
                    DataRow row = commentTable.NewRow();
                    row["ShapeId"] = shapeId;
                    row["ShapeName"] = (object)shapeName ?? DBNull.Value;
                    row["CommentId"] = commentId;
                    row["CommentText"] = commentText;
                    row["ReviewerId"] = reviewerId;
                    commentTable.Rows.Add(row);
                }
            }

            // Example output: display extracted comments
            foreach (DataRow r in commentTable.Rows)
            {
                Console.WriteLine(
                    $"Shape ID: {r["ShapeId"]}, Name: {r["ShapeName"]}, " +
                    $"Comment ID: {r["CommentId"]}, Text: {r["CommentText"]}, " +
                    $"Reviewer: {r["ReviewerId"]}");
            }

            // -----------------------------------------------------------------
            // Real database insertion (illustrative – requires a DB provider)
            // -----------------------------------------------------------------
            // string connectionString = "your-connection-string-here";
            // using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            // {
            //     connection.Open();
            //     foreach (DataRow r in commentTable.Rows)
            //     {
            //         using (var cmd = new System.Data.SqlClient.SqlCommand(
            //             "INSERT INTO ShapeComments (ShapeId, ShapeName, CommentId, CommentText, ReviewerId) " +
            //             "VALUES (@ShapeId, @ShapeName, @CommentId, @CommentText, @ReviewerId)", connection))
            //         {
            //             cmd.Parameters.AddWithValue("@ShapeId", r["ShapeId"]);
            //             cmd.Parameters.AddWithValue("@ShapeName", r["ShapeName"] ?? (object)DBNull.Value);
            //             cmd.Parameters.AddWithValue("@CommentId", r["CommentId"]);
            //             cmd.Parameters.AddWithValue("@CommentText", r["CommentText"]);
            //             cmd.Parameters.AddWithValue("@ReviewerId", r["ReviewerId"]);
            //             cmd.ExecuteNonQuery();
            //         }
            //     }
            // }

            Console.WriteLine("Comment extraction completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
