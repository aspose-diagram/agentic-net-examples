using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the built‑in style sheet named "Emphasis"
            StyleSheet emphasisStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Emphasis")
                {
                    emphasisStyle = ss;
                    break;
                }
            }

            // If the style is not found, report and exit
            if (emphasisStyle == null)
            {
                Console.WriteLine("Emphasis style sheet not found in the diagram.");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has paragraph collection
                    if (shape.Paras == null || shape.Paras.Count == 0)
                        continue;

                    bool hasBullet = false;

                    // Check each paragraph for a bullet setting
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        Para para = shape.Paras[i];
                        // Bullet.Value indicates the bullet style; any non‑None value means a bullet list
                        if (para.Bullet != null && para.Bullet.Value != BulletValue.None)
                        {
                            hasBullet = true;
                            break;
                        }
                    }

                    // Apply the Emphasis style to shapes that contain bullet lists
                    if (hasBullet)
                    {
                        shape.TextStyle = emphasisStyle;
                        shape.FillStyle = emphasisStyle;
                        shape.LineStyle = emphasisStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with Emphasis style applied to bullet list shapes.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
