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

            // Paths for the original diagram, backup copy, and the final output
            string inputPath = "input.vsdx";
            string backupPath = "input_backup.vsdx";
            string outputPath = "output.vsdx";

            try
            {
                // Load the original diagram
                Diagram diagram = new Diagram(inputPath);

                // Save a backup copy before any modifications
                diagram.Save(backupPath, SaveFileFormat.Vsdx);

                // Apply a watermark to the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add watermark text covering the full page area
                // Font size is specified in inches (0.25 inches ≈ 18 points)
                page.AddText(pinX, pinY, pageWidth, pageHeight,
                             "CONFIDENTIAL", "Calibri", "#a5a5a5", 0.25);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Simple error handling
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
