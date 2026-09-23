using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath;
        // Output folder for PNG files
        string outputFolder;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputFolder = args[1];
        }
        else
        {
            Console.WriteLine("Enter the full path to the Visio file:");
            inputPath = Console.ReadLine();

            Console.WriteLine("Enter the folder path where PNG pages will be saved:");
            outputFolder = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputFolder))
        {
            Console.WriteLine("Invalid input or output path.");
            return;
        }

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Ensure the output folder exists
        if (!System.IO.Directory.Exists(outputFolder))
        {
            System.IO.Directory.CreateDirectory(outputFolder);
        }

        // Export each page as a separate PNG file
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Export only the current page
                PageIndex = i,
                PageCount = 1,
                // Do not export hidden pages
                ExportHiddenPage = false
            };

            string outputFile = System.IO.Path.Combine(outputFolder, $"Page_{i + 1}.png");
            diagram.Save(outputFile, pngOptions);
            Console.WriteLine($"Saved page {i + 1} to {outputFile}");
        }

        Console.WriteLine("Export completed.");
    }
}
