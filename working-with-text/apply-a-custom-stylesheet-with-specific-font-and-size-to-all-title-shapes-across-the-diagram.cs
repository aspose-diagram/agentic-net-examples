using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram path (required)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Desired font name for titles (optional, default Arial)
        string fontName = args.Length > 2 ? args[2] : "Arial";

        // Desired font size in points for titles (optional, default 14)
        double fontSizePoints = args.Length > 3 && double.TryParse(args[3], out double sz) ? sz : 14.0;

        // Convert points to inches (Aspose.Diagram uses inches for font size)
        double fontSizeInches = fontSizePoints / 72.0;

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Create a new stylesheet that defines the title font and size
            StyleSheet titleStyle = new StyleSheet();
            // Assign a unique ID (next available) to the stylesheet
            titleStyle.ID = diagram.StyleSheets.Count + 1;
            // Optional: give the stylesheet a readable name
            titleStyle.Name = "TitleStyle";

            // Define character formatting for the stylesheet
            Aspose.Diagram.Char titleChar = new Aspose.Diagram.Char();
            titleChar.IX = 0; // character index within the style
            titleChar.FontName.Value = fontName; // set the font name
            titleChar.Size.Value = fontSizeInches; // set the font size (in inches)
            // Add the character definition to the stylesheet
            titleStyle.Chars.Add(titleChar);

            // Add the new stylesheet to the diagram's collection
            diagram.StyleSheets.Add(titleStyle);

            // Iterate over all pages and shapes to find title shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify title shapes by checking if the universal name contains "Title"
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Apply the custom stylesheet to the shape's text style
                        shape.TextStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}