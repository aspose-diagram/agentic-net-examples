using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (use defaults if not provided)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
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

            if (emphasisStyle == null)
            {
                Console.WriteLine("Emphasis style not found in the document. No changes will be applied.");
            }
            else
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains at least one paragraph with a bullet
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Examine the first paragraph; if it has a bullet other than None, treat it as a bullet list shape
                            if (shape.Paras[0].Bullet != null && shape.Paras[0].Bullet.Value != BulletValue.None)
                            {
                                // Apply the Emphasis style to the shape's text
                                shape.TextStyle = emphasisStyle;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
