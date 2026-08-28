using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Mapping of shape universal names to target page names for navigation
            var navigationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "HomeButton", "HomePage" },
                { "DetailsButton", "DetailsPage" }
            };

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape == null) continue;

                    // Check if the shape name exists in the navigation map
                    if (navigationMap.TryGetValue(shape.NameU, out string targetPage))
                    {
                        // Create a new hyperlink
                        Hyperlink link = new Hyperlink();
                        // Set the internal page link (SubAddress) to the target page name
                        link.SubAddress.Value = targetPage;
                        // Optional: set a description for the hyperlink
                        link.Description.Value = $"Navigate to {targetPage}";
                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
