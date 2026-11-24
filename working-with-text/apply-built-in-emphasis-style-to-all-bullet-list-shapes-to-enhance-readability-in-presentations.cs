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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in "Emphasis" style sheet.
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name != null && ss.Name.Equals("Emphasis", StringComparison.OrdinalIgnoreCase))
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                if (emphasisStyle == null)
                {
                    Console.Error.WriteLine("Emphasis style sheet not found in the diagram.");
                    return;
                }

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply the Emphasis style to shapes that contain text (paragraphs).
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Assign the style sheet to the shape's text style.
                            shape.TextStyle = emphasisStyle;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}