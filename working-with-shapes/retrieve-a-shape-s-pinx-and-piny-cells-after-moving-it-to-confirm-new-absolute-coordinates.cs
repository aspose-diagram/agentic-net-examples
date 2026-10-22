using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first shape that is not a group (or use the first shape regardless)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Error: No shapes found on the first page.");
                    return;
                }

                // Display original PinX and PinY
                double originalPinX = targetShape.XForm.PinX.Value;
                double originalPinY = targetShape.XForm.PinY.Value;
                Console.WriteLine($"Original PinX: {originalPinX}, PinY: {originalPinY}");

                // Move the shape by adding an offset (example: +1.0 inches)
                double offsetX = 1.0;
                double offsetY = 1.0;
                targetShape.XForm.PinX.Value = originalPinX + offsetX;
                targetShape.XForm.PinY.Value = originalPinY + offsetY;

                // Retrieve the new PinX and PinY after moving
                double newPinX = targetShape.XForm.PinX.Value;
                double newPinY = targetShape.XForm.PinY.Value;
                Console.WriteLine($"Moved PinX: {newPinX}, PinY: {newPinY}");

                // If the shape is a group, calculate absolute coordinates for its child shapes
                if (targetShape.Type == TypeValue.Group)
                {
                    Console.WriteLine("Shape is a group. Calculating absolute coordinates for child shapes:");
                    // Group's own transformation values
                    double groupPinX = targetShape.XForm.PinX.Value;
                    double groupPinY = targetShape.XForm.PinY.Value;
                    double groupLocPinX = targetShape.XForm.LocPinX.Value;
                    double groupLocPinY = targetShape.XForm.LocPinY.Value;

                    foreach (Shape child in targetShape.Shapes)
                    {
                        // Child's relative coordinates
                        double childPinX = child.XForm.PinX.Value;
                        double childPinY = child.XForm.PinY.Value;

                        // Absolute coordinates calculation
                        double absoluteX = groupPinX + childPinX - groupLocPinX;
                        double absoluteY = groupPinY + childPinY - groupLocPinY;

                        Console.WriteLine($"Child Shape ID {child.ID}: Absolute PinX = {absoluteX}, Absolute PinY = {absoluteY}");
                    }
                }

                // Save the modified diagram to a new file (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}