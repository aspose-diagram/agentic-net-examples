using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (command‑line args or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block (Diagram implements IDisposable)
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in "Emphasis" style sheet once (reuse for all shapes)
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.NameU == "Emphasis")
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine whether the shape contains at least one bullet paragraph
                        bool hasBulletParagraph = false;
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            // Use the correct paragraph type 'Para'
                            Para para = shape.Paras[i];
                            // Bullet.Value == 0 means no bullet; non‑zero indicates a bullet
                            if (para.Bullet != null && para.Bullet.Value != 0)
                            {
                                hasBulletParagraph = true;
                                break;
                            }
                        }

                        // If no bullet paragraphs, move to the next shape
                        if (!hasBulletParagraph)
                            continue;

                        if (emphasisStyle != null)
                        {
                            // Apply the Emphasis style sheet to the shape's text
                            shape.TextStyle = emphasisStyle;
                        }
                        else
                        {
                            // Fallback: apply bold formatting to all characters in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.Style.Value = StyleValue.Bold;
                            }
                        }
                    }
                }

                // Save the modified diagram using the VSDX format (requires a second argument)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
