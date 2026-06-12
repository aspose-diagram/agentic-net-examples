using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output folder path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPreviewGenerator <inputVisioFile> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Console.WriteLine($"Output folder does not exist. Creating: {outputFolder}");
                Directory.CreateDirectory(outputFolder);
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate position and size for the watermark (full page)
                    double pinX = pageWidth / 2.0;   // center horizontally
                    double pinY = pageHeight / 2.0; // center vertically
                    double watermarkWidth = pageWidth;
                    double watermarkHeight = pageHeight;

                    // Add watermark text to the page
                    // Font size is specified in inches (e.g., 0.2 inches ≈ 14.4 points)
                    page.AddText(pinX, pinY, watermarkWidth, watermarkHeight,
                                 "WATERMARK", "Arial", "#CCCCCC", 0.2);

                    // Prepare image save options for the current page
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = (int)page.ID, // Use the page's ID as index
                        PageCount = 1,
                        ExportHiddenPage = false
                    };

                    // Build output file name
                    string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{page.ID}" : page.Name;
                    string outputPath = Path.Combine(outputFolder, $"{safePageName}_preview.png");

                    // Save only the current page as an image
                    diagram.Save(outputPath, imgOptions);

                    Console.WriteLine($"Preview generated for page '{safePageName}' at: {outputPath}");
                }
            }

            Console.WriteLine("All previews generated successfully.");
        }
    }