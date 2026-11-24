using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string visioPath = "input.vsdx";
        if (!System.IO.File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Prepare an in‑memory DataTable to simulate a relational database table
        DataTable commentTable = new DataTable("Comments");
        commentTable.Columns.Add("CommentText", typeof(string));
        commentTable.Columns.Add("ShapeID", typeof(int));
        commentTable.Columns.Add("ReviewerID", typeof(int));
        commentTable.Columns.Add("PageIndex", typeof(int));

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Access page‑level annotations (comments)
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Extract comment metadata
                    string commentText = annotation.Comment?.Value ?? string.Empty;
                    int shapeId = annotation.ShapeID;
                    int reviewerId = annotation.ReviewerID?.Value ?? 0;

                    // Add a row to the simulated database table
                    DataRow row = commentTable.NewRow();
                    row["CommentText"] = commentText;
                    row["ShapeID"] = shapeId;
                    row["ReviewerID"] = reviewerId;
                    row["PageIndex"] = pageIndex;
                    commentTable.Rows.Add(row);
                }

                pageIndex++;
            }

            // Output the collected comment data to the console
            Console.WriteLine("Exported comment metadata:");
            foreach (DataRow row in commentTable.Rows)
            {
                Console.WriteLine($"Page {row["PageIndex"]}, ShapeID {row["ShapeID"]}, ReviewerID {row["ReviewerID"]}, Comment: {row["CommentText"]}");
            }

            // -----------------------------------------------------------------
            // Real database insertion example (commented out because external
            // database drivers are not available in this environment):
            //
            // using (var connection = new System.Data.SqlClient.SqlConnection("your-connection-string"))
            // {
            //     connection.Open();
            //     foreach (DataRow row in commentTable.Rows)
            //     {
            //         using (var command = new System.Data.SqlClient.SqlCommand(
            //             "INSERT INTO Comments (CommentText, ShapeID, ReviewerID, PageIndex) VALUES (@text, @shape, @reviewer, @page)", connection))
            //         {
            //             command.Parameters.AddWithValue("@text", row["CommentText"]);
            //             command.Parameters.AddWithValue("@shape", row["ShapeID"]);
            //             command.Parameters.AddWithValue("@reviewer", row["ReviewerID"]);
            //             command.Parameters.AddWithValue("@page", row["PageIndex"]);
            //             command.ExecuteNonQuery();
            //         }
            //     }
            // }
            // -----------------------------------------------------------------
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}