using System.IO;
using System;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Simulate external data feed with shape names and corresponding URLs
            DataTable urlTable = new DataTable();
            urlTable.Columns.Add("ShapeName", typeof(string));
            urlTable.Columns.Add("Url", typeof(string));

            // Sample data – in a real scenario this would come from an external source
            urlTable.Rows.Add("Process", "https://example.com/process");
            urlTable.Rows.Add("Decision", "https://example.com/decision");
            urlTable.Rows.Add("Start", "https://example.com/start");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Find a matching URL for the shape based on its universal name
                    string shapeName = shape.NameU;
                    DataRow[] matches = urlTable.Select($"ShapeName = '{shapeName}'");
                    if (matches.Length == 0)
                        continue; // No URL for this shape

                    string url = matches[0]["Url"].ToString();

                    // Create a new hyperlink and assign the URL
                    Hyperlink link = new Hyperlink();
                    link.Name = $"{shapeName}_Link";
                    link.Address.Value = url;
                    link.Description.Value = $"Link to {shapeName}";

                    // Add the hyperlink to the shape's collection
                    shape.Hyperlinks.Add(link);
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
