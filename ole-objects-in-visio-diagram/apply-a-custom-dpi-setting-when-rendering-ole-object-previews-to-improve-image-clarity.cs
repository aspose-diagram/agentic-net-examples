using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output folder, desired DPI (integer)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder> <dpi>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        string dpiArg = args[2];

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Validate output folder
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        // Parse DPI value
        if (!int.TryParse(dpiArg, out int dpi) || dpi <= 0)
        {
            Console.Error.WriteLine($"Invalid DPI value: {dpiArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages using index to avoid non‑existent Page.Index property
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Page page = diagram.Pages[pageIdx];

                // Determine if the page contains any OLE (foreign) shapes
                bool containsOle = false;
                foreach (Shape shape in page.Shapes)
                {
                    // Verify shape is a foreign OLE object and has embedded data
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        containsOle = true;
                        break;
                    }
                }

                if (!containsOle)
                {
                    // Skip pages without OLE objects
                    continue;
                }

                // Prepare image save options with custom DPI (Resolution)
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Set the DPI – higher DPI yields clearer preview
                saveOptions.Resolution = dpi;
                // Render only the current page
                saveOptions.PageIndex = pageIdx;
                saveOptions.PageCount = 1;

                // Build output file name for the page image
                string fileName = $"Page{pageIdx + 1}_OLEPreview.png";
                string outputPath = Path.Combine(outputFolder, fileName);

                // Save the page as an image with the specified DPI
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Saved OLE preview for page {pageIdx + 1}: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}