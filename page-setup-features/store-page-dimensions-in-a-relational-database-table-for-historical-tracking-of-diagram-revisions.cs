using System.IO;
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

            // Connection string to the relational database (adjust as needed)
            string connectionString = "Data Source=.;Initial Catalog=DiagramHistory;Integrated Security=True";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (values are in inches)
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                int pageId = page.ID;
                string pageName = page.Name;

                // Store the dimensions in the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO PageDimensions
                        (DiagramPath, PageId, PageName, WidthInches, HeightInches, RevisionDate)
                        VALUES
                        (@DiagramPath, @PageId, @PageName, @Width, @Height, @RevisionDate)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@DiagramPath", diagramPath);
                        cmd.Parameters.AddWithValue("@PageId", pageId);
                        cmd.Parameters.AddWithValue("@PageName", (object)pageName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Width", width);
                        cmd.Parameters.AddWithValue("@Height", height);
                        cmd.Parameters.AddWithValue("@RevisionDate", DateTime.UtcNow);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Stored dimensions for page {pageId} ({pageName}): {width} x {height} inches.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
