using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: source diagram path and target diagram path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];

        // Guard: ensure source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard: ensure target file exists
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram inside a using block
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram inside a using block
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // ----- Copy header and footer text fields -----
                    targetDiagram.HeaderFooter.HeaderLeft = sourceDiagram.HeaderFooter.HeaderLeft;
                    targetDiagram.HeaderFooter.HeaderCenter = sourceDiagram.HeaderFooter.HeaderCenter;
                    targetDiagram.HeaderFooter.HeaderRight = sourceDiagram.HeaderFooter.HeaderRight;

                    targetDiagram.HeaderFooter.FooterLeft = sourceDiagram.HeaderFooter.FooterLeft;
                    targetDiagram.HeaderFooter.FooterCenter = sourceDiagram.HeaderFooter.FooterCenter;
                    targetDiagram.HeaderFooter.FooterRight = sourceDiagram.HeaderFooter.FooterRight;

                    // ----- Copy header and footer margins -----
                    targetDiagram.HeaderFooter.HeaderMargin.Value = sourceDiagram.HeaderFooter.HeaderMargin.Value;
                    targetDiagram.HeaderFooter.FooterMargin.Value = sourceDiagram.HeaderFooter.FooterMargin.Value;

                    // ----- Copy header/footer font settings -----
                    HeaderFooterFont srcFont = sourceDiagram.HeaderFooter.HeaderFooterFont;
                    HeaderFooterFont tgtFont = targetDiagram.HeaderFooter.HeaderFooterFont;

                    // Font face name
                    tgtFont.FaceName = srcFont.FaceName;
                    // Font height (in points)
                    tgtFont.Height = srcFont.Height;
                    // Font weight (e.g., 400 = normal, 700 = bold)
                    tgtFont.Weight = srcFont.Weight;
                    // Italic flag (BOOL)
                    tgtFont.Italic = srcFont.Italic;
                    // Underline flag (BOOL)
                    tgtFont.Underline = srcFont.Underline;

                    // ----- Copy header/footer color -----
                    targetDiagram.HeaderFooter.HeaderFooterColor = sourceDiagram.HeaderFooter.HeaderFooterColor;

                    // Save the modified target diagram (overwrites the original file)
                    targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine("Header and footer settings copied successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}