using System;
using Aspose.Diagram;

public class HeaderFooterCopier
{
    /// <summary>
    /// Copies header and footer settings from a source Visio file to a target Visio file.
    /// The page content of the target diagram remains unchanged.
    /// </summary>
    /// <param name="sourcePath">Full path to the source diagram.</param>
    /// <param name="targetPath">Full path to the target diagram.</param>
    /// <param name="outputPath">Full path where the updated target diagram will be saved.</param>
    public static void CopyHeaderFooter(string sourcePath, string targetPath, string outputPath)
    {
        // Load source and target diagrams
        Diagram sourceDiagram = new Diagram(sourcePath);
        Diagram targetDiagram = new Diagram(targetPath);

        // Copy simple text fields
        targetDiagram.HeaderFooter.HeaderLeft = sourceDiagram.HeaderFooter.HeaderLeft;
        targetDiagram.HeaderFooter.HeaderCenter = sourceDiagram.HeaderFooter.HeaderCenter;
        targetDiagram.HeaderFooter.HeaderRight = sourceDiagram.HeaderFooter.HeaderRight;
        targetDiagram.HeaderFooter.FooterLeft = sourceDiagram.HeaderFooter.FooterLeft;
        targetDiagram.HeaderFooter.FooterCenter = sourceDiagram.HeaderFooter.FooterCenter;
        targetDiagram.HeaderFooter.FooterRight = sourceDiagram.HeaderFooter.FooterRight;

        // Copy margins (values are in inches)
        targetDiagram.HeaderFooter.HeaderMargin.Value = sourceDiagram.HeaderFooter.HeaderMargin.Value;
        targetDiagram.HeaderFooter.FooterMargin.Value = sourceDiagram.HeaderFooter.FooterMargin.Value;

        // Copy font settings
        targetDiagram.HeaderFooter.HeaderFooterFont.FaceName = sourceDiagram.HeaderFooter.HeaderFooterFont.FaceName;
        targetDiagram.HeaderFooter.HeaderFooterFont.Weight = sourceDiagram.HeaderFooter.HeaderFooterFont.Weight;
        targetDiagram.HeaderFooter.HeaderFooterFont.Height = sourceDiagram.HeaderFooter.HeaderFooterFont.Height;

        // Copy color (hex string or Visio color index)
        targetDiagram.HeaderFooter.HeaderFooterColor = sourceDiagram.HeaderFooter.HeaderFooterColor;

        // Save the updated target diagram (preserving page content)
        targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Example usage:
            // Ensure the file paths are valid on your system.
            string sourceFile = @"C:\Diagrams\SourceDiagram.vsdx";
            string targetFile = @"C:\Diagrams\TargetDiagram.vsdx";
            string resultFile = @"C:\Diagrams\TargetDiagram_WithHeaderFooter.vsdx";

            try
            {
                HeaderFooterCopier.CopyHeaderFooter(sourceFile, targetFile, resultFile);
                Console.WriteLine("Header and footer settings copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}