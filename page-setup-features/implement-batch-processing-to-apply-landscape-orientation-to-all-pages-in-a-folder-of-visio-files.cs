using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files
            string inputFolder = @"C:\VisioFiles";
            // Optional: folder to save processed files (can be same as input)
            string outputFolder = @"C:\VisioFiles\Processed";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process each Visio file in the input folder
            foreach (string filePath in Directory.GetFiles(inputFolder))
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply Landscape orientation to every page
                    foreach (Page page in diagram.Pages)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Determine output file path (same name, processed folder)
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the updated diagram (preserve original format)
                    // Use the file extension to decide the SaveFileFormat
                    string ext = Path.GetExtension(outputPath).ToLowerInvariant();
                    SaveFileFormat format = ext switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vsd" => SaveFileFormat.Vsd,
                        ".vdx" => SaveFileFormat.Vdx,
                        ".vssx" => SaveFileFormat.Vssx,
                        ".vstx" => SaveFileFormat.Vstx,
                        ".vss" => SaveFileFormat.Vss,
                        ".vst" => SaveFileFormat.Vst,
                        ".vsx" => SaveFileFormat.Vsx,
                        ".vtx" => SaveFileFormat.Vtx,
                        ".pdf" => SaveFileFormat.Pdf,
                        ".png" => SaveFileFormat.Png,
                        ".jpeg" => SaveFileFormat.Jpeg,
                        ".jpg" => SaveFileFormat.Jpeg,
                        ".bmp" => SaveFileFormat.Bmp,
                        ".tiff" => SaveFileFormat.Tiff,
                        ".svg" => SaveFileFormat.Svg,
                        ".html" => SaveFileFormat.Html,
                        ".swf" => SaveFileFormat.Swf,
                        ".xaml" => SaveFileFormat.Xaml,
                        ".xps" => SaveFileFormat.Xps,
                        _ => SaveFileFormat.Vsdx // default fallback
                    };

                    diagram.Save(outputPath, format);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }