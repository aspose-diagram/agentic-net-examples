using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

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

        // Desired font settings for title shapes.
        const string desiredFontName = "Calibri";
        const double desiredFontSize = 24.0; // points

        // Validate that the desired font is installed on the system.
        bool fontInstalled = false;
        try
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            fontInstalled = fontCollection.Families.Any(f => string.Equals(f.Name, desiredFontName, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to enumerate installed fonts: {ex.Message}");
            // Continue; fallback will be applied later.
        }

        // If the font is not installed, configure a fallback default font.
        if (!fontInstalled)
        {
            // Set global default font for Aspose.Diagram.
            FontConfigs.DefaultFontName = "Arial";
            Console.WriteLine($"Font \"{desiredFontName}\" not found. Falling back to default font \"Arial\".");
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new stylesheet for title formatting.
                StyleSheet titleStyle = new StyleSheet();

                // Assign a unique ID (next available ID).
                titleStyle.ID = diagram.StyleSheets.Count + 1;

                // Configure character formatting: font name and size.
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.FontName.Value = desiredFontName;
                charFormat.Size.Value = desiredFontSize;
                titleStyle.Chars.Add(charFormat);

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(titleStyle);

                // Iterate through all pages and shapes to find title shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify title shapes by checking the NameU property for the word "Title".
                        // Adjust this condition as needed for different naming conventions.
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply the custom stylesheet to the shape's text style.
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}