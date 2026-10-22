using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default)
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

        try
        {
            // Load the Visio diagram (using mandatory using pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the default master name to assign (must exist in the diagram)
                const string defaultMasterName = "Rectangle";

                // Retrieve the default master from the diagram's master collection
                Master defaultMaster = diagram.Masters.GetMasterByName(defaultMasterName);
                if (defaultMaster == null)
                {
                    Console.Error.WriteLine($"Default master \"{defaultMasterName}\" not found in the diagram. Cannot assign missing masters.");
                    return;
                }

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs that have no master assigned
                    List<long> shapesToReplace = new List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape.Master is null when no master is assigned
                        if (shape.Master == null)
                        {
                            shapesToReplace.Add(shape.ID);
                        }
                    }

                    // Replace each identified shape with a new shape based on the default master
                    foreach (long shapeId in shapesToReplace)
                    {
                        // Retrieve the original shape to copy positional and size information
                        Shape originalShape = page.Shapes.GetShape(shapeId);
                        if (originalShape == null)
                            continue; // Safety check

                        // Store original geometry
                        double pinX = originalShape.XForm.PinX.Value;
                        double pinY = originalShape.XForm.PinY.Value;
                        double width = originalShape.XForm.Width.Value;
                        double height = originalShape.XForm.Height.Value;

                        // Mark the original shape for deletion
                        originalShape.Del = BOOL.True;

                        // Add a new shape using the default master at the same location
                        // The fourth parameter 'isCalculate' is set to false as required
                        long newShapeId = page.AddShape(pinX, pinY, defaultMasterName, false);

                        // Retrieve the newly added shape
                        Shape newShape = page.Shapes.GetShape(newShapeId);
                        if (newShape != null)
                        {
                            // Apply original size to the new shape
                            newShape.SetWidth(width);
                            newShape.SetHeight(height);
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}