using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Prepare an in‑memory table that mimics a relational database table.
        DataTable commentsTable = new DataTable("ShapeComments");
        commentsTable.Columns.Add("ShapeID", typeof(int));
        commentsTable.Columns.Add("CommentText", typeof(string));

        try
        {
            // Load the Visio document.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Ensure the page has an annotation collection.
                if (page.PageSheet?.Annotations == null)
                    continue;

                // Iterate over each annotation (comment) on the page.
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    // ShapeID links the comment to a shape; 0 means no shape association.
                    int shapeId = ann.ShapeID;

                    // Retrieve the comment text; handle possible nulls.
                    string commentText = ann.Comment?.Value ?? string.Empty;

                    // Add the extracted data to the in‑memory table.
                    DataRow row = commentsTable.NewRow();
                    row["ShapeID"] = shapeId;
                    row["CommentText"] = commentText;
                    commentsTable.Rows.Add(row);
                }
            }

            // Output the collected comments to the console.
            Console.WriteLine("Extracted Shape Comments:");
            foreach (DataRow row in commentsTable.Rows)
            {
                Console.WriteLine($"ShapeID: {row["ShapeID"]}, Comment: {row["CommentText"]}");
            }

            // -----------------------------------------------------------------
            // Example of persisting the data to a real relational database.
            // The following code is illustrative; replace the connection string
            // and provider as needed for your environment.
            //
            // using (var connection = new System.Data.SqlClient.SqlConnection("your_connection_string"))
            // {
            //     connection.Open();
            //     foreach (DataRow row in commentsTable.Rows)
            //     {
            //         using (var command = new System.Data.SqlClient.SqlCommand(
            //             "INSERT INTO ShapeComments (ShapeID, CommentText) VALUES (@ShapeID, @CommentText)", connection))
            //         {
            //             command.Parameters.AddWithValue("@ShapeID", (int)row["ShapeID"]);
            //             command.Parameters.AddWithValue("@CommentText", (string)row["CommentText"]);
            //             command.ExecuteNonQuery();
            //         }
            //     }
            // }
            // -----------------------------------------------------------------
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}