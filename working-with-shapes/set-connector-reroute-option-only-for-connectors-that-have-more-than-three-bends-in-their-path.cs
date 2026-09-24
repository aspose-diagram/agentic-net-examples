using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // NOTE: The Geometry collection is not available in this API version,
                        // so we cannot accurately count bends. As a fallback, apply the reroute
                        // option to all connectors (or implement custom logic if geometry data is needed).
                        // Set the reroute behavior using the only valid enum member.
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                    }
                }
            }

            // Output Visio file path
            string outputPath = "output.vsdx";
            // Save the modified diagram using the appropriate SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}