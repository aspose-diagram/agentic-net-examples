using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Example calculation: scale PinX and PinY by 1.1
                        double calculatedPinX = shape.XForm.PinX.Value * 1.1;
                        double calculatedPinY = shape.XForm.PinY.Value * 1.1;

                        // Round to two decimal places
                        double roundedPinX = Math.Round(calculatedPinX, 2);
                        double roundedPinY = Math.Round(calculatedPinY, 2);

                        // Apply rounded values back to the shape
                        shape.XForm.PinX.Value = roundedPinX;
                        shape.XForm.PinY.Value = roundedPinY;
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}