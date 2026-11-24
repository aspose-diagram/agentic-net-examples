using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
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
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only group shapes (containers of sub‑shapes).
                        if (shape.Type == TypeValue.Group)
                        {
                            // Validate that the group's LocPinX and LocPinY contain numeric values.
                            double groupLocPinX = shape.XForm.LocPinX.Value;
                            double groupLocPinY = shape.XForm.LocPinY.Value;

                            bool groupLocPinXIsNumeric = !double.IsNaN(groupLocPinX);
                            bool groupLocPinYIsNumeric = !double.IsNaN(groupLocPinY);

                            if (!groupLocPinXIsNumeric || !groupLocPinYIsNumeric)
                            {
                                Console.WriteLine($"Warning: Group shape ID {shape.ID} has non‑numeric LocPinX/LocPinY. Skipping its children.");
                                continue;
                            }

                            // Iterate through each child shape within the group.
                            foreach (Shape child in shape.Shapes)
                            {
                                // Ensure child shape's PinX and PinY are numeric.
                                double childPinX = child.XForm.PinX.Value;
                                double childPinY = child.XForm.PinY.Value;

                                bool childPinXIsNumeric = !double.IsNaN(childPinX);
                                bool childPinYIsNumeric = !double.IsNaN(childPinY);

                                if (!childPinXIsNumeric || !childPinYIsNumeric)
                                {
                                    Console.WriteLine($"Warning: Child shape ID {child.ID} in group {shape.ID} has non‑numeric PinX/PinY. Skipping.");
                                    continue;
                                }

                                // Perform absolute pin calculations using the validated numeric values.
                                double absolutePinX = shape.XForm.PinX.Value + childPinX - groupLocPinX;
                                double absolutePinY = shape.XForm.PinY.Value + childPinY - groupLocPinY;

                                // Output the results.
                                Console.WriteLine($"Group ID {shape.ID}, Child ID {child.ID}: Absolute PinX = {absolutePinX}, Absolute PinY = {absolutePinY}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}