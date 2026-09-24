using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Emphasis" style sheet (if it exists)
            StyleSheet emphasisStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Emphasis")
                {
                    emphasisStyle = ss;
                    break;
                }
            }

            // If the style does not exist, skip creation (the built‑in style is expected to be present)
            if (emphasisStyle == null)
            {
                Console.Error.WriteLine("Emphasis style not found in the diagram. No style will be applied.");
            }

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine whether the shape contains any paragraph with a bullet
                    bool hasBullet = false;
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        Para para = shape.Paras[i];
                        // BulletValue.Undefined means no bullet; any other value indicates a bullet
                        if (para.Bullet != null && para.Bullet.Value != BulletValue.Undefined)
                        {
                            hasBullet = true;
                            break;
                        }
                    }

                    // Apply the Emphasis style to shapes that are bullet lists
                    if (hasBullet && emphasisStyle != null)
                    {
                        shape.TextStyle = emphasisStyle;
                    }
                }
            }

            // Output Visio file path
            string outputPath = "output.vsdx";
            // Save the modified diagram using the correct overload (second argument is a SaveFileFormat)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}