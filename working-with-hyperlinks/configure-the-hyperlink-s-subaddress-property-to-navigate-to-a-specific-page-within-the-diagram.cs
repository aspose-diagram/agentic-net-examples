using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages; add a second page if necessary
                if (diagram.Pages.Count < 2)
                {
                    diagram.Pages.Add(new Page());
                }

                // Reference the source (first) page and the target (second) page
                Page sourcePage = diagram.Pages[0];
                Page targetPage = diagram.Pages[1];

                // Add a rectangle shape to the source page (using the diagram helper)
                // The AddShape method returns a long shape identifier
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                // Retrieve the Shape object from the source page's collection
                Shape shape = sourcePage.Shapes.GetShape((int)shapeId);

                // Create a new Hyperlink instance
                Hyperlink link = new Hyperlink();

                // Determine the name of the target page for the SubAddress
                string targetPageName = targetPage.Name;
                if (string.IsNullOrEmpty(targetPageName))
                {
                    // Fallback to Visio's default naming convention if the page has no explicit name
                    targetPageName = $"Page-{targetPage.ID}";
                }

                // Set the SubAddress to navigate to the target page within the same diagram
                link.SubAddress.Value = targetPageName;

                // Optional: provide a tooltip/description for the hyperlink
                link.Description.Value = $"Navigate to {targetPageName}";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}