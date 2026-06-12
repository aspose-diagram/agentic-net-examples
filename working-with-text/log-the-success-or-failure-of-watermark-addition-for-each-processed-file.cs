using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing Visio files
        string inputFolder = @"C:\VisioFiles";
        // Output folder for processed files
        string outputFolder = @"C:\VisioFiles\Processed";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all Visio files (VSDX, VSD, VDX, etc.) in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Process only supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
            {
                continue;
            }

            try
            {
                // Load the diagram
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Add watermark to each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Add full‑page watermark text (positional arguments)
                        page.AddText(
                            0,               // pinX
                            0,               // pinY
                            pageWidth,       // width
                            pageHeight,      // height
                            "Watermark",     // text
                            "Calibri",       // fontName
                            "#a5a5a5",       // fontColor (hex)
                            0.25);           // fontSize in inches
                    }

                    // Save the modified diagram to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"SUCCESS: Watermark added to '{Path.GetFileName(filePath)}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"FAILURE: Could not process '{Path.GetFileName(filePath)}'. Error: {ex.Message}");
            }
        }
    }
}