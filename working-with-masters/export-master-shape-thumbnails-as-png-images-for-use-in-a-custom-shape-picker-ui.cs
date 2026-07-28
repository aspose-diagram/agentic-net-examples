using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file (stencil or diagram) and output folder
        string visioPath;
        string outputFolder;

        if (args.Length >= 2)
        {
            visioPath = args[0];
            // Guard: ensure the provided file exists
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                return;
            }

            outputFolder = args[1];
        }
        else
        {
            Console.Write("Enter the path to the Visio file (e.g., .vssx, .vsdx): ");
            visioPath = Console.ReadLine()?.Trim();
            // Guard: ensure the entered file exists
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                return;
            }

            Console.Write("Enter the folder where thumbnails will be saved: ");
            outputFolder = Console.ReadLine()?.Trim();
        }

        // Validate that both inputs are non‑empty
        if (string.IsNullOrEmpty(visioPath) || string.IsNullOrEmpty(outputFolder))
        {
            Console.WriteLine("Invalid input. Exiting.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the Visio document
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each master shape in the stencil/document
            foreach (Master master in diagram.Masters)
            {
                // The Icon property holds the thumbnail image bytes (usually .ico format)
                if (master.Icon == null || master.Icon.Length == 0)
                {
                    Console.WriteLine($"Master '{master.Name}' does not have an icon. Skipping.");
                    continue;
                }

                // Convert the icon bytes to an Aspose.Drawing.Image (fully qualified to avoid ambiguity)
                using (MemoryStream iconStream = new MemoryStream(master.Icon))
                using (Aspose.Drawing.Image iconImage = Aspose.Drawing.Image.FromStream(iconStream))
                {
                    // Build a safe file name from the master name
                    string safeName = string.IsNullOrWhiteSpace(master.Name) ? "UnnamedMaster" : master.Name;
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        safeName = safeName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(outputFolder, $"{safeName}.png");

                    // Save the image as PNG using Aspose.Drawing.Imaging.ImageFormat
                    iconImage.Save(outputPath, ImageFormat.Png);
                    Console.WriteLine($"Thumbnail saved: {outputPath}");
                }
            }

            Console.WriteLine("Export completed.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}