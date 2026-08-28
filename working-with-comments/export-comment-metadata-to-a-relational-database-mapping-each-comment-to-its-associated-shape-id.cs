using System;
using Aspose.Diagram;
using System.Data.SqlClient;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioFilePath = @"C:\Path\To\YourDiagram.vsdx";

                // Connection string to the relational database (replace with actual values)
                string connectionString = @"Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Open a SQL connection
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    // Prepare an INSERT command with parameters
                    string insertCommandText = @"
                        INSERT INTO DiagramComments (ShapeId, CommentText, MarkerIndex)
                        VALUES (@ShapeId, @CommentText, @MarkerIndex)";

                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText, sqlConnection))
                    {
                        // Define parameters
                        insertCommand.Parameters.Add("@ShapeId", System.Data.SqlDbType.Int);
                        insertCommand.Parameters.Add("@CommentText", System.Data.SqlDbType.NVarChar, -1);
                        insertCommand.Parameters.Add("@MarkerIndex", System.Data.SqlDbType.Int);

                        // Iterate through all pages in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Access the collection of annotations (comments) on the page
                            foreach (Annotation annotation in page.PageSheet.Annotations)
                            {
                                // Retrieve comment text and associated shape ID
                                string commentText = annotation.Comment.Value;
                                int shapeId = annotation.ShapeID; // Primitive int, no .Value needed
                                int markerIndex = annotation.MarkerIndex.Value; // Unique identifier for the comment

                                // Assign values to parameters
                                insertCommand.Parameters["@ShapeId"].Value = shapeId;
                                insertCommand.Parameters["@CommentText"].Value = commentText;
                                insertCommand.Parameters["@MarkerIndex"].Value = markerIndex;

                                // Execute the INSERT command
                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected != 1)
                                {
                                    Console.WriteLine($"Warning: Expected to insert 1 row, but inserted {rowsAffected} rows for ShapeID {shapeId}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Inserted comment for ShapeID {shapeId}: \"{commentText}\"");
                                }
                            }
                        }
                    }

                    sqlConnection.Close();
                }

                // Optionally, save the diagram if any modifications were made (not required for export)
                // diagram.Save("ExportedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }