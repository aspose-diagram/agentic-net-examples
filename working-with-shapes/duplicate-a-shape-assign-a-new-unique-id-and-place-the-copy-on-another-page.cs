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

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block (required for proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages (source and target)
                if (diagram.Pages.Count < 2)
                {
                    // Add a new blank page to serve as the target page
                    Page newPage = new Page();
                    diagram.Pages.Add(newPage);
                }

                // Retrieve source (first) and target (second) pages
                Page sourcePage = diagram.Pages[0];
                Page targetPage = diagram.Pages[1];

                // Find the first shape on the source page to duplicate
                Shape sourceShape = null;
                foreach (Shape shp in sourcePage.Shapes)
                {
                    sourceShape = shp;
                    break; // only need the first shape
                }

                if (sourceShape == null)
                {
                    Console.Error.WriteLine("No shapes found on the source page to duplicate.");
                    return;
                }

                // Ensure the shape has an associated master (required for AddShape)
                if (sourceShape.Master == null || string.IsNullOrEmpty(sourceShape.Master.Name))
                {
                    Console.Error.WriteLine("Source shape does not have an associated master.");
                    return;
                }

                // Retrieve geometry from the source shape
                double pinX = sourceShape.XForm.PinX.Value;
                double pinY = sourceShape.XForm.PinY.Value;
                double width = sourceShape.XForm.Width.Value;
                double height = sourceShape.XForm.Height.Value;
                string masterName = sourceShape.Master.Name;

                // Add a new shape to the target page using the same master and geometry
                // The AddShape method returns the new shape's ID (long)
                long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName, false);

                // Retrieve the newly added shape object for further modifications
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy text from the source shape to the new shape
                // Clear any existing text first
                newShape.Text.Value.Clear();
                foreach (var txtObj in sourceShape.Text.Value)
                {
                    if (txtObj is Txt txt)
                    {
                        // Add a new Txt run with the same text content
                        newShape.Text.Value.Add(new Txt(txt.Text));
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape duplicated successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}