using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Example calculation: offset the current PinX/Y by arbitrary values.
                        double calculatedPinX = shape.XForm.PinX.Value + 0.12345;
                        double calculatedPinY = shape.XForm.PinY.Value + 0.67890;

                        // Round the calculated values to two decimal places before applying.
                        shape.XForm.PinX.Value = RoundToTwoDecimalPlaces(calculatedPinX);
                        shape.XForm.PinY.Value = RoundToTwoDecimalPlaces(calculatedPinY);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Rounds a double value to two decimal places using AwayFromZero rounding.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <returns>The rounded value.</returns>
    private static double RoundToTwoDecimalPlaces(double value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}