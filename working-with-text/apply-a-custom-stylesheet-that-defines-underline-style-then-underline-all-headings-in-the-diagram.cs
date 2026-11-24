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

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom stylesheet that defines an underline style
                // ------------------------------------------------------------
                StyleSheet underlineStyle = new StyleSheet();
                underlineStyle.ID = diagram.StyleSheets.Count + 1;
                underlineStyle.Name = "CustomUnderlineStyle";

                // Define a character style with underline
                Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
                underlineChar.IX = 0; // character index
                underlineChar.Style.Value = StyleValue.Underline;
                underlineStyle.Chars.Add(underlineChar);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(underlineStyle);

                // ------------------------------------------------------------
                // Apply the underline stylesheet to all heading shapes
                // For this example, a heading shape is identified by its name containing "Heading"
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify heading shapes by name (case‑insensitive)
                        if (!string.IsNullOrEmpty(shape.Name) &&
                            shape.Name.IndexOf("Heading", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Assign the custom underline stylesheet to the shape's text style
                            shape.TextStyle = underlineStyle;

                            // Ensure each character explicitly has the underline style
                            for (int i = 0; i < shape.Chars.Count; i++)
                            {
                                shape.Chars[i].Style.Value = StyleValue.Underline;
                            }
                        }
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}