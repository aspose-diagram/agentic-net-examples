using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input_diagram_path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify group shapes (TypeValue.Group)
                        if (shape.Type == TypeValue.Group)
                        {
                            // Iterate through each sub‑shape within the group
                            foreach (Aspose.Diagram.Shape child in shape.Shapes)
                            {
                                // Skip deleted sub‑shapes
                                if (child.Del == BOOL.True)
                                    continue;

                                // Calculate absolute PinX and PinY for the sub‑shape
                                double absolutePinX = shape.XForm.PinX.Value
                                                      + child.XForm.PinX.Value
                                                      - shape.XForm.LocPinX.Value;

                                double absolutePinY = shape.XForm.PinY.Value
                                                      + child.XForm.PinY.Value
                                                      - shape.XForm.LocPinY.Value;

                                // Log the results to the console
                                Console.WriteLine(
                                    $"Page: {page.Name}, Group Shape ID: {shape.ID}, Child Shape ID: {child.ID}, " +
                                    $"Absolute PinX: {absolutePinX}, Absolute PinY: {absolutePinY}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}