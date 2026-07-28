using System;
using Aspose.Diagram;
using System.Data.SqlClient;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Connection string to the relational database
                string connectionString = "Data Source=SERVER;Initial Catalog=DatabaseName;Integrated Security=True";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Open a SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Prepare the INSERT command with parameters
                    string insertSql = "INSERT INTO Comments (ShapeId, CommentText) VALUES (@ShapeId, @CommentText)";
                    using (SqlCommand command = new SqlCommand(insertSql, connection))
                    {
                        // Define parameters
                        SqlParameter shapeIdParam = new SqlParameter("@ShapeId", System.Data.SqlDbType.Int);
                        SqlParameter commentTextParam = new SqlParameter("@CommentText", System.Data.SqlDbType.NVarChar, -1);
                        command.Parameters.Add(shapeIdParam);
                        command.Parameters.Add(commentTextParam);

                        // Iterate through all pages in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Ensure the page has annotations
                            if (page.PageSheet.Annotations != null)
                            {
                                // Iterate through each comment (annotation) on the page
                                foreach (Annotation annotation in page.PageSheet.Annotations)
                                {
                                    // Retrieve the shape ID associated with the comment
                                    int shapeId = annotation.ShapeID;

                                    // Retrieve the comment text
                                    string commentText = annotation.Comment.Value;

                                    // Assign parameter values
                                    shapeIdParam.Value = shapeId;
                                    commentTextParam.Value = commentText ?? string.Empty;

                                    // Execute the INSERT command
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    connection.Close();
                }

                Console.WriteLine("Comment export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }