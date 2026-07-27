using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConversion <inputVisioFile> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the output folder exists
            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            // Total number of pages to process
            int totalPages = diagram.Pages.Count;
            Console.WriteLine($"Total pages to convert: {totalPages}");

            // Loop through each page and export it as an image
            for (int i = 0; i < totalPages; i++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[i];

                // Prepare the output file name (e.g., Page_1.png)
                string outputPath = System.IO.Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Configure image save options for the current page
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = i; // Export only the current page

                // Save the page as an image
                diagram.Save(outputPath, saveOptions);

                // Update progress in the console
                int percentComplete = (int)(((i + 1) / (double)totalPages) * 100);
                Console.WriteLine($"Page {i + 1}/{totalPages} exported. Progress: {percentComplete}%");
            }

            Console.WriteLine("Conversion completed successfully.");
        }
    }