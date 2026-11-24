using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file and output root folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputRootFolder>");
            return;
        }

        string inputPath = args[0];
        string outputRoot = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output root folder exists or create it
        try
        {
            Directory.CreateDirectory(outputRoot);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputRoot}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare auto-space options (required before exporting)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5,
                    DistanceInVertical = 0.5
                };

                // Manual page counter because Page does not have an Index property
                int pageCounter = 1;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Apply auto-spacing to the page's shapes
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                    // Determine a safe folder name for the page
                    string pageFolderName = string.IsNullOrWhiteSpace(page.NameU)
                        ? $"Page_{pageCounter}"
                        : page.NameU;

                    // Replace invalid path characters
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        pageFolderName = pageFolderName.Replace(c, '_');
                    }

                    string pageFolderPath = Path.Combine(outputRoot, pageFolderName);
                    Directory.CreateDirectory(pageFolderPath);

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare SVG save options (default settings)
                        SVGSaveOptions svgOptions = new SVGSaveOptions();

                        // Build a file name for the shape SVG
                        string shapeFileName = $"Shape_{shape.ID}.svg";
                        string shapeOutputPath = Path.Combine(pageFolderPath, shapeFileName);

                        try
                        {
                            // Export the shape to SVG
                            shape.ToSvg(shapeOutputPath, svgOptions);
                        }
                        catch (Exception shapeEx)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page '{pageFolderName}': {shapeEx.Message}");
                            // Continue with next shape
                        }
                    }

                    pageCounter++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}