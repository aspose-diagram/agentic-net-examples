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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through user‑defined cells of the shape.
                    foreach (User userCell in shape.Users)
                    {
                        // Example: look for a user‑defined cell named "Status".
                        if (string.Equals(userCell.NameU, "Status", StringComparison.OrdinalIgnoreCase))
                        {
                            string cellValue = userCell.Value?.Val ?? string.Empty;

                            // Apply conditional fill color based on the cell value.
                            // "High"   -> Red
                            // "Medium" -> Orange
                            // "Low"    -> Green
                            // Any other value -> Light Gray
                            if (string.Equals(cellValue, "High", StringComparison.OrdinalIgnoreCase))
                            {
                                shape.Fill.FillForegnd.Ufe.F = "RGB(255,0,0)"; // Red
                            }
                            else if (string.Equals(cellValue, "Medium", StringComparison.OrdinalIgnoreCase))
                            {
                                shape.Fill.FillForegnd.Ufe.F = "RGB(255,165,0)"; // Orange
                            }
                            else if (string.Equals(cellValue, "Low", StringComparison.OrdinalIgnoreCase))
                            {
                                shape.Fill.FillForegnd.Ufe.F = "RGB(0,128,0)"; // Green
                            }
                            else
                            {
                                shape.Fill.FillForegnd.Ufe.F = "RGB(211,211,211)"; // Light Gray
                            }

                            // Once the relevant user cell is processed, break to avoid redundant checks.
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}