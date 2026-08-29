using System;
using System.IO;
using System.Data.SqlClient;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file – replace with your actual file path or pass as argument.
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify that the Visio file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Connection string for the relational database – adjust to your environment.
        string connectionString = "Data Source=.;Initial Catalog=VisioComments;Integrated Security=True";

        try
        {
            // Load the Visio document.
            Diagram diagram = new Diagram(visioPath);

            // Ensure the Reviewers collection is not null before using it.
            var reviewers = diagram.DocumentSheet?.Reviewers;

            // Open a SQL connection once and reuse it for all inserts.
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();

                // Prepare an INSERT command with parameters to avoid SQL injection.
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO ShapeComments (DiagramPath, PageName, ShapeId, CommentText, ReviewerName)
                      VALUES (@DiagramPath, @PageName, @ShapeId, @CommentText, @ReviewerName)", sqlConn))
                {
                    // Define parameters once; values will be set inside the loops.
                    cmd.Parameters.Add("@DiagramPath", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@PageName", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@ShapeId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@CommentText", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@ReviewerName", System.Data.SqlDbType.NVarChar);

                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page name; fallback to empty string if null.
                        string pageName = page.NameU ?? string.Empty;

                        // Access the collection of annotations (comments) on the page.
                        var annotations = page.PageSheet?.Annotations;
                        if (annotations == null) continue;

                        // Process each annotation.
                        foreach (Annotation annotation in annotations)
                        {
                            // Extract the shape identifier the comment is attached to.
                            int shapeId = annotation.ShapeID;

                            // Retrieve the comment text.
                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Resolve the reviewer name using the ReviewerID index.
                            string reviewerName = string.Empty;
                            if (reviewers != null && annotation.ReviewerID != null)
                            {
                                int reviewerIndex = annotation.ReviewerID.Value;
                                // Reviewer collection is 0‑based; ensure the index is valid.
                                if (reviewerIndex >= 0 && reviewerIndex < reviewers.Count)
                                {
                                    Reviewer reviewer = reviewers[reviewerIndex];
                                    reviewerName = reviewer?.Name?.Value ?? string.Empty;
                                }
                            }

                            // Populate command parameters with current values.
                            cmd.Parameters["@DiagramPath"].Value = visioPath;
                            cmd.Parameters["@PageName"].Value = pageName;
                            cmd.Parameters["@ShapeId"].Value = shapeId;
                            cmd.Parameters["@CommentText"].Value = commentText;
                            cmd.Parameters["@ReviewerName"].Value = reviewerName;

                            // Execute the INSERT statement.
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                sqlConn.Close();
            }

            Console.WriteLine("Comments extraction and database insertion completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading, processing, or DB operations.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}