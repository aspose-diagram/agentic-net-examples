using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a folder path argument was provided.
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

        // Prepare an output subfolder.
        string outputFolder = Path.Combine(folderPath, "output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get all .vsdx files in the specified folder.
        string[] files = Directory.GetFiles(folderPath, "*.vsdx");

        foreach (string filePath in files)
        {
            // Guard: ensure the file exists (required by rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate over each page to add a full‑page watermark.
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches).
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Add a text shape that spans the whole page.
                        // The AddText overload without font parameters is used.
                        Shape watermarkShape = page.AddText(
                            pinX: 0,
                            pinY: 0,
                            width: pageWidth,
                            height: pageHeight,
                            text: "CONFIDENTIAL");

                        // Clear any existing text runs and add the watermark text.
                        watermarkShape.Text.Value.Clear();
                        watermarkShape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                        // Clear existing character formatting and set new formatting.
                        watermarkShape.Chars.Clear();
                        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                        ch.IX = 0; // character index
                        ch.FontName.Value = "Arial";          // font name
                        ch.Color.Value = "#FF0000";           // red color in hex
                        ch.Size.Value = 72.0 / 72.0;          // 72 points = 1 inch
                        watermarkShape.Chars.Add(ch);
                    }

                    // Determine the output path and save the modified diagram.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}