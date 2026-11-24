using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape is a group.
                        bool isGroup = shape.Type == TypeValue.Group;
                        if (!isGroup)
                            continue;

                        // Retrieve the group's transformation values.
                        double groupPinX = shape.XForm.PinX.Value;
                        double groupPinY = shape.XForm.PinY.Value;
                        double groupLocPinX = shape.XForm.LocPinX.Value;
                        double groupLocPinY = shape.XForm.LocPinY.Value;

                        // Iterate through each sub‑shape within the group.
                        foreach (Aspose.Diagram.Shape child in shape.Shapes)
                        {
                            // Skip deleted sub‑shapes.
                            if (child.Del == BOOL.True)
                                continue;

                            // Child's relative coordinates.
                            double childPinX = child.XForm.PinX.Value;
                            double childPinY = child.XForm.PinY.Value;

                            // Calculate absolute coordinates based on the group's transform.
                            double absolutePinX = groupPinX + childPinX - groupLocPinX;
                            double absolutePinY = groupPinY + childPinY - groupLocPinY;

                            // Log the results.
                            Console.WriteLine(
                                $"Page: {page.Name}, Group Shape ID: {shape.ID}, Child Shape ID: {child.ID}, " +
                                $"Absolute PinX: {absolutePinX}, Absolute PinY: {absolutePinY}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}