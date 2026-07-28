using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Define which shape (by its universal name) should link to which page
            var navigationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "HomeShape", "HomePage" },
                { "DetailsShape", "DetailsPage" },
                { "SummaryShape", "SummaryPage" }
            };

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // If the shape's universal name is in the navigation map, add a hyperlink
                        if (navigationMap.TryGetValue(shape.NameU, out string targetPage))
                        {
                            // Create a new hyperlink that points to the target page
                            Hyperlink link = new Hyperlink();
                            // Internal navigation uses SubAddress
                            link.SubAddress.Value = targetPage;
                            // Optional tooltip/description
                            link.Description.Value = $"Navigate to {targetPage}";

                            // Ensure the Hyperlinks collection exists before adding
                            if (shape.Hyperlinks != null)
                            {
                                shape.Hyperlinks.Add(link);
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Hyperlinks have been assigned and the diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
