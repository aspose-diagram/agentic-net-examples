using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // 1. Create a custom StyleSheet with the desired font and size
                // ------------------------------------------------------------
                StyleSheet titleStyle = new StyleSheet();

                // Assign a unique ID (Visio IDs start at 1)
                titleStyle.ID = diagram.StyleSheets.Count + 1;

                // Give the style a readable name
                titleStyle.Name = "CustomTitleStyle";

                // Define character formatting: font name and size (size in inches)
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();

                // Set the font name (e.g., Arial)
                charFormat.FontName.Value = "Arial";

                // Set the font size to 24 points (24/72 inches)
                charFormat.Size.Value = 24.0 / 72.0;

                // Add the character format to the stylesheet's Char collection
                titleStyle.Chars.Add(charFormat);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(titleStyle);

                // ------------------------------------------------------------
                // 2. Apply the stylesheet to all shapes that are titles
                // ------------------------------------------------------------
                // A shape is considered a "title" if its NameU contains the word "Title"
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a NameU and check for "Title"
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply the custom stylesheet to the shape's text style
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // ------------------------------------------------------------
                // 3. Save the modified diagram to the specified output path
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}