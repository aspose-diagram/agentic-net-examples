using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

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
                    // Default theme (no change)
                    bool themeApplied = false;

                    // Look for a custom property named "Theme"
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "Theme")
                        {
                            string themeValue = prop.Value.Val?.Trim();

                            // Apply fill color based on the theme value
                            if (!string.IsNullOrEmpty(themeValue))
                            {
                                // Example theme handling: Red, Green, Blue
                                if (themeValue.Equals("Red", StringComparison.OrdinalIgnoreCase))
                                {
                                    shape.Fill.FillForegnd.Value = "RGB(255,0,0)";
                                    themeApplied = true;
                                }
                                else if (themeValue.Equals("Green", StringComparison.OrdinalIgnoreCase))
                                {
                                    shape.Fill.FillForegnd.Value = "RGB(0,255,0)";
                                    themeApplied = true;
                                }
                                else if (themeValue.Equals("Blue", StringComparison.OrdinalIgnoreCase))
                                {
                                    shape.Fill.FillForegnd.Value = "RGB(0,0,255)";
                                    themeApplied = true;
                                }
                                // Add more theme handling as needed
                            }

                            // Once the Theme property is processed, exit the inner loop
                            break;
                        }
                    }

                    // Optional: log shapes that did not have a Theme property
                    if (!themeApplied)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page {page.Name} has no Theme property or unrecognized value.");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}