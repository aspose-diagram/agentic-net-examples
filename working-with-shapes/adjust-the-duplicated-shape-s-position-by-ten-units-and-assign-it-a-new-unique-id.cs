using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the first non-deleted shape to duplicate
                Shape sourceShape = null;
                Page sourcePage = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        sourceShape = shape;
                        sourcePage = page;
                        break;
                    }

                    if (sourceShape != null)
                        break;
                }

                if (sourceShape == null || sourcePage == null)
                {
                    Console.Error.WriteLine("No suitable shape found to duplicate.");
                    return;
                }

                // Retrieve the master name of the source shape (required for AddShape)
                string masterName = sourceShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Source shape does not have an associated master; cannot duplicate.");
                    return;
                }

                // Get original position and size
                double origPinX = sourceShape.XForm.PinX.Value;
                double origPinY = sourceShape.XForm.PinY.Value;
                double width = sourceShape.XForm.Width.Value;
                double height = sourceShape.XForm.Height.Value;

                // Calculate new position (offset by 10 units on X axis)
                double newPinX = origPinX + 10.0;
                double newPinY = origPinY; // Y remains the same

                // Add the duplicated shape to the same page
                // The overload returns a long representing the new shape's structural ID
                long newShapeId = sourcePage.AddShape(newPinX, newPinY, width, height, masterName, false);

                // Retrieve the newly added shape object
                Shape newShape = sourcePage.Shapes.GetShape(newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Assign the new unique ID (the ID returned by AddShape is already unique)
                newShape.ID = newShapeId;

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}