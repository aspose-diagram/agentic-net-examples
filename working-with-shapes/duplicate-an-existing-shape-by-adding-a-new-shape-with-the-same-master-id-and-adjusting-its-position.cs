using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape to duplicate
                Shape originalShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        originalShape = shape;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found to duplicate.");
                    return;
                }

                // Ensure the shape has an associated master (template)
                if (originalShape.Master == null)
                {
                    Console.Error.WriteLine("The selected shape does not have a master and cannot be duplicated.");
                    return;
                }

                // Retrieve the master name to use for the new shape
                string masterName = originalShape.Master.Name;

                // Get the original shape position
                double originalPinX = originalShape.XForm.PinX.Value;
                double originalPinY = originalShape.XForm.PinY.Value;

                // Define an offset (in inches) for the duplicated shape's position
                const double offset = 1.0;
                double newPinX = originalPinX + offset;
                double newPinY = originalPinY + offset;

                // Add a new shape using the same master; isCalculate set to false
                long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                // Retrieve the newly added shape (optional, for further adjustments)
                Shape newShape = page.Shapes.GetShape((int)newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}