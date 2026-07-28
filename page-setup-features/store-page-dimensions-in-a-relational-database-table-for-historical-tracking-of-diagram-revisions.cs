using System;
using Aspose.Diagram;
using System.Data.SqlClient;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Connection string to the relational database (replace with actual values)
                string connectionString = "Data Source=SERVER_NAME;Initial Catalog=DatabaseName;Integrated Security=True";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page and store its dimensions
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page width and height (values are in inches)
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Insert the dimensions into the database
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string insertCommand = @"
                            INSERT INTO PageDimensions (DiagramName, PageID, Width, Height, RevisionDate)
                            VALUES (@DiagramName, @PageID, @Width, @Height, @RevisionDate)";

                        using (SqlCommand command = new SqlCommand(insertCommand, connection))
                        {
                            command.Parameters.AddWithValue("@DiagramName", diagramPath);
                            command.Parameters.AddWithValue("@PageID", page.ID);
                            command.Parameters.AddWithValue("@Width", width);
                            command.Parameters.AddWithValue("@Height", height);
                            command.Parameters.AddWithValue("@RevisionDate", DateTime.UtcNow);

                            command.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine($"Stored dimensions for Page ID {page.ID}: Width={width} in, Height={height} in");
                }

                // Optional: Save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }