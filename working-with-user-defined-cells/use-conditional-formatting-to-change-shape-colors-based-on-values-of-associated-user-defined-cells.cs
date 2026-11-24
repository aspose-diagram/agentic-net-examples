using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip connectors (1-D shapes) – we only format regular shapes
                    if (shape.OneD)
                        continue;

                    // Look for a user-defined cell named "Status"
                    User statusCell = null;
                    foreach (User user in shape.Users)
                    {
                        if (string.Equals(user.NameU, "Status", StringComparison.OrdinalIgnoreCase))
                        {
                            statusCell = user;
                            break;
                        }
                    }

                    // If the "Status" cell exists, apply color based on its value
                    if (statusCell != null)
                    {
                        string value = statusCell.Value.Val?.Trim() ?? string.Empty;

                        // Determine fill color
                        string rgb;
                        if (string.Equals(value, "Critical", StringComparison.OrdinalIgnoreCase))
                        {
                            rgb = "RGB(255,0,0)"; // Red
                        }
                        else if (string.Equals(value, "Warning", StringComparison.OrdinalIgnoreCase))
                        {
                            rgb = "RGB(255,255,0)"; // Yellow
                        }
                        else
                        {
                            rgb = "RGB(0,255,0)"; // Green (default)
                        }

                        // Apply the fill color to the shape
                        shape.Fill.FillForegnd.Ufe.F = rgb;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}