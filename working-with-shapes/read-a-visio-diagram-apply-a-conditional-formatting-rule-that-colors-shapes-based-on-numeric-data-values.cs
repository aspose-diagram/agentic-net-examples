using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
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

                        // Ensure the shape has a custom properties collection
                        if (shape.Props == null)
                            continue;

                        // Look for a custom property named "Score"
                        foreach (Prop prop in shape.Props)
                        {
                            // Compare the property label (case‑insensitive) to "Score"
                            if (string.Equals(prop.Label?.Value, "Score", StringComparison.OrdinalIgnoreCase))
                            {
                                // Try to parse the property value as a double
                                if (double.TryParse(prop.Value?.Val, out double numericValue))
                                {
                                    // Apply conditional fill color based on the numeric value
                                    // Example rule: value >= 50 => red, otherwise green
                                    if (numericValue >= 50)
                                    {
                                        // Red fill (hex string)
                                        shape.Fill.FillForegnd.Value = "#FF0000";
                                    }
                                    else
                                    {
                                        // Green fill (hex string)
                                        shape.Fill.FillForegnd.Value = "#00FF00";
                                    }
                                }

                                // Once the "Score" property is processed, exit the inner loop
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
