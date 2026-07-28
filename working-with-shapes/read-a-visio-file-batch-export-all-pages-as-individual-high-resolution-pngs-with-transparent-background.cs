using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Verify the input file exists
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Visio file not found: {inputPath}");
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Base name for output PNG files
                string baseName = Path.GetFileNameWithoutExtension(inputPath);

                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image export options
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1,

                        // High‑resolution output (300 DPI)
                        Resolution = 300f
                    };

                    // Build the output file name (e.g., input_Page1.png)
                    string outputPath = $"{baseName}_Page{i + 1}.png";

                    // Save the current page as a PNG with transparent background
                    diagram.Save(outputPath, options);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
