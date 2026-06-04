using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Implements page saving callback to display progress in console
    public class ConsolePageSavingCallback : IPageSavingCallback
    {
        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve input and output file paths
            string inputPath;
            string outputPath;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputPath = args[1];
            }
            else
            {
                Console.Write("Enter the path to the source Visio file: ");
                inputPath = Console.ReadLine();

                Console.Write("Enter the desired output PDF path: ");
                outputPath = Console.ReadLine();
            }

            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
                return;
            }

            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Configure PDF save options and attach the progress callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageSavingCallback = new ConsolePageSavingCallback()
                };

                // Save the diagram as PDF with progress reporting
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Conversion completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during conversion: {ex.Message}");
            }
        }
    }
}