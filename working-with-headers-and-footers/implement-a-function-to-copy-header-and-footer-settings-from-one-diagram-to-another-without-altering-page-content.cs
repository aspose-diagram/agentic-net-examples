using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Example file paths – replace with actual paths as needed
                string sourcePath = "sourceDiagram.vsdx";
                string targetPath = "targetDiagram.vsdx";
                string outputPath = "targetDiagram_WithHeaderFooterCopied.vsdx";

                try
                {
                    CopyHeaderFooter(sourcePath, targetPath, outputPath);
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

        /// <summary>
        /// Copies header and footer settings from the source diagram to the target diagram.
        /// Page content (shapes, pages, etc.) in the target diagram remains unchanged.
        /// </summary>
        /// <param name="sourceFile">Path to the source Visio file.</param>
        /// <param name="targetFile">Path to the target Visio file.</param>
        /// <param name="outputFile">Path where the updated target diagram will be saved.</param>
        static void CopyHeaderFooter(string sourceFile, string targetFile, string outputFile)
        {
            // Load source and target diagrams
            Diagram sourceDiagram = new Diagram(sourceFile);
            Diagram targetDiagram = new Diagram(targetFile);

            // Copy textual header/footer fields
            targetDiagram.HeaderFooter.HeaderLeft   = sourceDiagram.HeaderFooter.HeaderLeft;
            targetDiagram.HeaderFooter.HeaderCenter = sourceDiagram.HeaderFooter.HeaderCenter;
            targetDiagram.HeaderFooter.HeaderRight  = sourceDiagram.HeaderFooter.HeaderRight;
            targetDiagram.HeaderFooter.FooterLeft   = sourceDiagram.HeaderFooter.FooterLeft;
            targetDiagram.HeaderFooter.FooterCenter = sourceDiagram.HeaderFooter.FooterCenter;
            targetDiagram.HeaderFooter.FooterRight  = sourceDiagram.HeaderFooter.FooterRight;

            // Copy margin values (in inches)
            targetDiagram.HeaderFooter.HeaderMargin.Value = sourceDiagram.HeaderFooter.HeaderMargin.Value;
            targetDiagram.HeaderFooter.FooterMargin.Value = sourceDiagram.HeaderFooter.FooterMargin.Value;

            // Copy header/footer text color
            targetDiagram.HeaderFooter.HeaderFooterColor = sourceDiagram.HeaderFooter.HeaderFooterColor;

            // Copy header/footer font settings
            var srcFont = sourceDiagram.HeaderFooter.HeaderFooterFont;
            var tgtFont = targetDiagram.HeaderFooter.HeaderFooterFont;

            tgtFont.FaceName = srcFont.FaceName;      // Font family name
            tgtFont.Height   = srcFont.Height;        // Point size (negative value per library convention)
            tgtFont.Weight   = srcFont.Weight;        // 700 = Bold, 400 = Regular
            tgtFont.Italic   = srcFont.Italic;        // BOOL.True / BOOL.False
            tgtFont.Underline = srcFont.Underline;    // BOOL.True / BOOL.False

            // Save the updated target diagram
            targetDiagram.Save(outputFile, SaveFileFormat.Vsdx);
        }
    }