using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get folder path from argument or ask the user
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing .vsdx files: ");
            folderPath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.WriteLine("Invalid folder path.");
            return;
        }

        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");
        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified folder.");
            return;
        }

        foreach (string filePath in vsdxFiles)
        {
            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Add a full‑page watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center of the page (pin position)
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add watermark text covering the whole page
                    page.AddText(
                        pinX,
                        pinY,
                        pageWidth,
                        pageHeight,
                        "CONFIDENTIAL",   // watermark text
                        "Calibri",        // font name
                        "#a5a5a5",        // font color (hex)
                        0.25);            // font size in inches
                }

                // Overwrite the original file with the watermarked diagram
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark added to: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
