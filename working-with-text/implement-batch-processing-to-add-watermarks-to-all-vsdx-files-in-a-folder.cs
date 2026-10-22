using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing .vsdx files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all .vsdx files in the specified folder.
        string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

        if (vsdxFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified folder.");
            return;
        }

        foreach (string filePath in vsdxFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page to add a full‑page watermark.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches).
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Define watermark properties.
                        string watermarkText = "CONFIDENTIAL";
                        string fontName = "Arial";
                        string fontColor = "#FF0000"; // Red color in hex.
                        double fontSize = 72; // Points.

                        // Add the watermark text covering the entire page.
                        // pinX and pinY are set to 0 to start at the page origin.
                        page.AddText(0, 0, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSize);
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Watermark added to: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}