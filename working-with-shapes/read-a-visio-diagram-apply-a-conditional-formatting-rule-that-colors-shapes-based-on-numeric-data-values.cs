using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use command‑line arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Look for a custom property named "Score"
                        foreach (Prop prop in shape.Props)
                        {
                            // Ensure the property label exists and matches "Score" (case‑insensitive)
                            if (prop.Label != null && prop.Label.Value != null &&
                                prop.Label.Value.Equals("Score", StringComparison.OrdinalIgnoreCase))
                            {
                                // Retrieve the property value as a string
                                string valStr = prop.Value?.Val;
                                if (double.TryParse(valStr, out double numericValue))
                                {
                                    // Determine fill colour based on the numeric value
                                    // Green for > 80, Yellow for > 50, Red otherwise
                                    int rgb;
                                    if (numericValue > 80)
                                        rgb = 0x00FF00; // Green
                                    else if (numericValue > 50)
                                        rgb = 0xFFFF00; // Yellow
                                    else
                                        rgb = 0xFF0000; // Red

                                    // Convert the integer RGB value to a hex string (e.g., "#FF0000")
                                    string hexColor = $"#{rgb:X6}";

                                    // Apply the fill colour to the shape (FillForegnd.Value expects a string)
                                    shape.Fill.FillForegnd.Value = hexColor;
                                }

                                // Once the "Score" property has been processed, exit the inner loop
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram using the correct overload and enum member
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}