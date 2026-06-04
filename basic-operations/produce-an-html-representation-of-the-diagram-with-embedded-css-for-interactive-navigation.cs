using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (VSDX) path
            string inputPath = "input.vsdx";

            // Output HTML file path
            string outputPath = "output.html";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Add simple navigation hyperlinks: each page's first shape links to the next page
            for (int i = 0; i < diagram.Pages.Count - 1; i++)
            {
                Page page = diagram.Pages[i];

                // Ensure the page has at least one shape
                if (page.Shapes.Count > 0)
                {
                    Shape shape = page.Shapes[0];

                    // Create a hyperlink that points to the next page by its universal name
                    Hyperlink link = new Hyperlink
                    {
                        Name = $"LinkToPage{i + 2}",
                        SubAddress = { Value = diagram.Pages[i + 1].NameU },
                        Description = { Value = $"Navigate to page {i + 2}" }
                    };

                    // Add the hyperlink to the shape's collection
                    shape.Hyperlinks.Add(link);
                }
            }

            // Configure HTML export options with embedded resources (CSS, images)
            HTMLSaveOptions options = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,      // Embed CSS and images into a single HTML file
                ExportHiddenPage = false,     // Do not export hidden pages
                IsExportComments = false,     // Skip comments
                Title = "Diagram HTML Export"
            };

            // Save the diagram as an HTML file using the configured options
            diagram.Save(outputPath, options);

            Console.WriteLine("HTML export completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
