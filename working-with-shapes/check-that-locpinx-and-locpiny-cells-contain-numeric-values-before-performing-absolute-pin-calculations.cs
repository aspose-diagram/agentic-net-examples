using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (optional command line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape is a group
                        bool isGroup = shape.Type == TypeValue.Group;

                        if (isGroup)
                        {
                            // Group shape: iterate its child shapes
                            foreach (Shape child in shape.Shapes)
                            {
                                // Skip deleted child shapes
                                if (child.Del == BOOL.True)
                                    continue;

                                // Retrieve LocPinX and LocPinY values for group and child
                                double groupLocPinX = shape.XForm.LocPinX.Value;
                                double groupLocPinY = shape.XForm.LocPinY.Value;
                                double childLocPinX = child.XForm.LocPinX.Value;
                                double childLocPinY = child.XForm.LocPinY.Value;

                                // Verify that all LocPin values are numeric (not NaN)
                                if (double.IsNaN(groupLocPinX) || double.IsNaN(groupLocPinY) ||
                                    double.IsNaN(childLocPinX) || double.IsNaN(childLocPinY))
                                {
                                    Console.WriteLine($"Skipping child shape ID {child.ID} due to non-numeric LocPin values.");
                                    continue;
                                }

                                // Perform absolute pin calculations for the child shape
                                double absolutePinX = shape.XForm.PinX.Value + child.XForm.PinX.Value - groupLocPinX;
                                double absolutePinY = shape.XForm.PinY.Value + child.XForm.PinY.Value - groupLocPinY;

                                // Output the calculated absolute coordinates
                                Console.WriteLine($"Child shape ID {child.ID} absolute PinX: {absolutePinX}, PinY: {absolutePinY}");
                            }
                        }
                        else
                        {
                            // Non-group shape: check its own LocPin values
                            double locPinX = shape.XForm.LocPinX.Value;
                            double locPinY = shape.XForm.LocPinY.Value;

                            // Verify that LocPin values are numeric
                            if (double.IsNaN(locPinX) || double.IsNaN(locPinY))
                            {
                                Console.WriteLine($"Skipping shape ID {shape.ID} due to non-numeric LocPin values.");
                                continue;
                            }

                            // For non-group shapes, PinX/PinY are already absolute
                            Console.WriteLine($"Shape ID {shape.ID} PinX: {shape.XForm.PinX.Value}, PinY: {shape.XForm.PinY.Value}");
                        }
                    }
                }

                // Save the (potentially unchanged) diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}