using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the Visio file path, second (optional) is the output folder.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output folder.
        string outputFolder = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputFolder))
        {
            outputFolder = Directory.GetCurrentDirectory();
        }

        // Ensure the output directory exists.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: Unable to create output directory \"{outputFolder}\": {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages using explicit Page type (no var).
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Prepare a safe file name for the page.
                    string pageName = !string.IsNullOrEmpty(page.NameU) ? page.NameU : $"Page_{pageIndex + 1}";
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        pageName = pageName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(outputFolder, $"{pageName}_{pageIndex + 1}.png");

                    // Configure image save options for high‑resolution PNG.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page.
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Set a high DPI resolution (e.g., 300 DPI).
                        Resolution = 300f,
                        // Enable anti‑aliasing for better quality.
                        SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias
                    };

                    // Perform the export inside a try/catch to handle per‑page errors.
                    try
                    {
                        diagram.Save(outputPath, saveOptions);
                        Console.WriteLine($"Exported page {pageIndex + 1} to \"{outputPath}\"");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error exporting page {pageIndex + 1}: {ex.Message}");
                    }

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}