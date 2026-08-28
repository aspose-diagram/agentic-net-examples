using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Text; // Required for system font enumeration

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: the path to the Visio file to validate
        string diagramPath = args.Length > 0 ? args[0] : string.Empty;
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Define company style guidelines (example: required fill color and font)
        const string requiredFillColor = "#FF0000"; // Red in hex
        const string requiredFontName = "Calibri";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Verify that the diagram's font collection contains the required font
            bool fontAvailable = false;
            foreach (Font font in diagram.Fonts)
            {
                if (string.Equals(font.Name, requiredFontName, StringComparison.OrdinalIgnoreCase))
                {
                    fontAvailable = true;
                    break;
                }
            }

            // If the required font is not installed, report and continue (fallback may occur)
            if (!fontAvailable)
            {
                Console.WriteLine($"Warning: Required font \"{requiredFontName}\" is not installed on this system.");
            }

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Flag to indicate whether the current master complies with the style
                bool masterCompliant = true;

                // Iterate through each shape that belongs to the master
                foreach (Shape shape in master.Shapes)
                {
                    // ----- Check fill color -----
                    // Ensure the shape has a FillForegnd cell and compare its value
                    if (shape.Fill != null && shape.Fill.FillForegnd != null && shape.Fill.FillForegnd.Value != null)
                    {
                        string shapeColor = shape.Fill.FillForegnd.Value.Trim();
                        if (!string.Equals(shapeColor, requiredFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Master \"{master.Name}\" - Shape ID {shape.ID} has fill color \"{shapeColor}\" (expected \"{requiredFillColor}\").");
                            masterCompliant = false;
                        }
                    }

                    // ----- Check font name -----
                    // Examine character formatting runs; if none exist, skip font check
                    if (shape.Chars != null && shape.Chars.Count > 0)
                    {
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Ensure the FontName cell is present
                            if (ch.FontName != null && ch.FontName.Value != null)
                            {
                                string shapeFont = ch.FontName.Value.Trim();
                                if (!string.Equals(shapeFont, requiredFontName, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine($"Master \"{master.Name}\" - Shape ID {shape.ID} uses font \"{shapeFont}\" (expected \"{requiredFontName}\").");
                                    masterCompliant = false;
                                }
                            }
                        }
                    }
                }

                // Report overall compliance for the master
                if (masterCompliant)
                {
                    Console.WriteLine($"Master \"{master.Name}\" complies with the style guidelines.");
                }
                else
                {
                    Console.WriteLine($"Master \"{master.Name}\" does NOT comply with the style guidelines.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and write to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}