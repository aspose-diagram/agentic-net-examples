using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output directory.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioEpsExport <inputVisioPath> <outputDirectory>");
                return;
            }

            string inputPath = args[0];
            string outputDir = args[1];

            // Verify input file exists.
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Ensure output directory exists.
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            int pageCount = diagram.Pages.Count;

            // Aspose.Diagram does not provide a direct EPS export format.
            // As a vector alternative, we use EMF (Enhanced Metafile) which is supported.
            // Each page is saved as an EMF file; you may convert EMF to EPS with external tools if needed.
            for (int i = 0; i < pageCount; i++)
            {
                // Construct output file name for the current page.
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.emf");

                // Configure image save options for EMF format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf)
                {
                    // Export only the current page.
                    PageIndex = i,
                    // Do not export hidden pages.
                    ExportHiddenPage = false
                };

                // Save the specific page using the configured options.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Saved page {i + 1} to {outputPath}");
            }

            Console.WriteLine("Export completed.");
        }
    }