using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];

        // Guard: ensure source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Guard: ensure target file exists.
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source and target diagrams.
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Access HeaderFooter objects.
            HeaderFooter srcHF = sourceDiagram.HeaderFooter;
            HeaderFooter tgtHF = targetDiagram.HeaderFooter;

            // Copy text strings.
            tgtHF.HeaderLeft = srcHF.HeaderLeft;
            tgtHF.HeaderCenter = srcHF.HeaderCenter;
            tgtHF.HeaderRight = srcHF.HeaderRight;
            tgtHF.FooterLeft = srcHF.FooterLeft;
            tgtHF.FooterCenter = srcHF.FooterCenter;
            tgtHF.FooterRight = srcHF.FooterRight;

            // Copy margins.
            tgtHF.HeaderMargin.Value = srcHF.HeaderMargin.Value;
            tgtHF.FooterMargin.Value = srcHF.FooterMargin.Value;

            // Copy font settings.
            HeaderFooterFont srcFont = srcHF.HeaderFooterFont;
            HeaderFooterFont tgtFont = tgtHF.HeaderFooterFont;
            tgtFont.FaceName = srcFont.FaceName;
            tgtFont.Height = srcFont.Height;
            tgtFont.Weight = srcFont.Weight;
            tgtFont.Italic = srcFont.Italic;
            tgtFont.Underline = srcFont.Underline;

            // Copy header/footer color.
            tgtHF.HeaderFooterColor = srcHF.HeaderFooterColor;

            // Save the modified target diagram.
            targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}