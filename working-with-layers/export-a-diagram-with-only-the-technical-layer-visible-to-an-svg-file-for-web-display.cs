using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and adjust layer visibility.
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of layers on the page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Make only the layer named "Technical" visible.
                        if (layer.Name.Value == "Technical")
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure SVG export options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                // Set the target format explicitly.
                svgOptions.SaveFormat = SaveFileFormat.Svg;
                // Export the first page (index 0). Adjust if needed.
                svgOptions.PageIndex = 0;
                // Optional: export all pages by setting PageCount if the property exists.
                // svgOptions.PageCount = diagram.Pages.Count; // Uncomment if supported.

                // Export the diagram to SVG using the configured options.
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Diagram exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}