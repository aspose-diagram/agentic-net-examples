using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Configure the folder that contains system fonts (recursive search enabled)
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Path to the source Visio file
            string diagramPath = "input.vsdx";
            // Verify the diagram file exists before attempting to load it
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Prepare a collection of installed system fonts for validation
            InstalledFontCollection systemFonts = new InstalledFontCollection();

            bool shapeProcessed = false; // Tracks whether any shape with text was handled

            // Iterate over all shapes on the page
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                // Ensure the shape actually contains text
                if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                {
                    shapeProcessed = true;

                    // Validate each character run's font against installed system fonts
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        string fontName = ch.FontName.Value;
                        bool fontFound = false;

                        // Use implicit typing for system font entries (type may vary across environments)
                        foreach (var sysFont in systemFonts.Families)
                        {
                            // Most font objects expose a Name property; compare case‑insensitively
                            if (string.Equals(sysFont.Name, fontName, StringComparison.OrdinalIgnoreCase))
                            {
                                fontFound = true;
                                break;
                            }
                        }

                        // Throw if the required font is missing on the host system
                        if (!fontFound)
                        {
                            throw new Exception($"Font \"{fontName}\" used in shape ID {shape.ID} is not installed on the system.");
                        }
                    }

                    // Export the shape to an SVG file
                    string svgOutputPath = $"shape_{shape.ID}.svg";
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    shape.ToSvg(svgOutputPath, svgOptions);

                    Console.WriteLine($"Shape ID {shape.ID} exported to SVG successfully: {svgOutputPath}");
                }
            }

            // Inform the user if no text‑containing shapes were found
            if (!shapeProcessed)
            {
                Console.WriteLine("No shapes with text were found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}