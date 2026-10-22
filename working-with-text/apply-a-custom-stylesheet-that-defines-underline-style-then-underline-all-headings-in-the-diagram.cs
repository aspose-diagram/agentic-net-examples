using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom stylesheet that defines an underline style
                // ------------------------------------------------------------
                StyleSheet underlineStyle = new StyleSheet();
                underlineStyle.ID = diagram.StyleSheets.Count + 1;
                // Optional: give the stylesheet a name (if supported)
                // underlineStyle.NameU = "UnderlineStyle";

                // Define a character style with underline
                Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
                underlineChar.Style.Value = StyleValue.Underline;
                // Add the character definition to the stylesheet
                underlineStyle.Chars.Add(underlineChar);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(underlineStyle);

                // ------------------------------------------------------------
                // Apply the underline stylesheet to all heading shapes
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify heading shapes by name containing "Heading"
                        // (adjust the condition as needed for your diagram)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Heading", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply the stylesheet to the shape's text
                            shape.TextStyle = underlineStyle;

                            // Ensure each character in the shape also has underline style
                            foreach (Aspose.Diagram.Char c in shape.Chars)
                            {
                                c.Style.Value = StyleValue.Underline;
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
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}