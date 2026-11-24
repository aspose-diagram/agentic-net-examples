using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeId> <targetPageName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string targetPageName = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the source shape on any page.
                Shape sourceShape = null;
                Page sourcePage = null;
                foreach (Page pg in diagram.Pages)
                {
                    sourceShape = pg.Shapes.GetShape(shapeId);
                    if (sourceShape != null)
                    {
                        sourcePage = pg;
                        break;
                    }
                }

                if (sourceShape == null || sourcePage == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                    return;
                }

                // Retrieve the master name of the source shape.
                string masterName = sourceShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Source shape does not have an associated master.");
                    return;
                }

                // Find the target page by name; create it if it does not exist.
                Page targetPage = diagram.Pages.GetPage(targetPageName);
                if (targetPage == null)
                {
                    targetPage = new Page();
                    diagram.Pages.Add(targetPage);
                    // Set the page name. // Fixed: Page.Name is a string, not a cell.
                    targetPage.Name = targetPageName; // corrected assignment
                }

                // Duplicate the shape onto the target page.
                // AddShape returns the new shape ID.
                long newShapeId = targetPage.AddShape(
                    sourceShape.XForm.PinX.Value,
                    sourceShape.XForm.PinY.Value,
                    sourceShape.XForm.Width.Value,
                    sourceShape.XForm.Height.Value,
                    masterName,
                    false); // isCalculate = false

                // Retrieve the newly added shape instance.
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy text content from the source shape to the new shape.
                if (sourceShape.Text != null && sourceShape.Text.Value != null)
                {
                    newShape.Text.Value.Clear();
                    foreach (var item in sourceShape.Text.Value)
                    {
                        // Preserve both Txt and Cp items.
                        newShape.Text.Value.Add(item);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape duplicated successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
