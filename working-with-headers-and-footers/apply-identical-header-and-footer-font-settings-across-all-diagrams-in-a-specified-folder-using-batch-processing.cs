using System;
using System.IO;
using Aspose.Diagram;

using Aspose.Drawing; // For Color structure

namespace DiagramHeaderFooterBatch
{
    class Program
    {
        static void Main()
        {
            // Folder containing the Visio files to process
            string inputFolder = @"C:\VisioFiles";
            // Folder where the updated files will be saved
            string outputFolder = @"C:\VisioFiles\Updated";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Process all Visio files (VSDX, VSD, VDX, etc.) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Filter supported Visio extensions
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vssx" && ext != ".vstx")
                    continue;

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply identical header and footer font settings
                    // Set font family
                    diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
                    // Set bold weight (700 = Bold)
                    diagram.HeaderFooter.HeaderFooterFont.Weight = 700;
                    // Set point size using negative mapping (e.g., -16 corresponds to 12pt)
                    diagram.HeaderFooter.HeaderFooterFont.Height = -16;
                    // Optional: set text color to black
                    diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                    // Example of setting header/footer text (can be customized)
                    diagram.HeaderFooter.HeaderLeft = "Company Confidential";
                    diagram.HeaderFooter.FooterRight = "Page: &p";

                    // Save the updated diagram with a new name
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(filePath) + "_updated" + ext);

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }
}
