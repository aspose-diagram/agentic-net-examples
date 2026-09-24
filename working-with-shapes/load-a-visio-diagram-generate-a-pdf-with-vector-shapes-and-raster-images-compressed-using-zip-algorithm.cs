using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output file paths
            string inputPath;
            string outputPath;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputPath = args[1];
            }
            else
            {
                Console.WriteLine("Enter the full path to the Visio file to load:");
                inputPath = Console.ReadLine();

                Console.WriteLine("Enter the full path for the PDF output file:");
                outputPath = Console.ReadLine();
            }

            // Validate paths
            if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputPath))
            {
                throw new Exception("Input and output paths must be provided.");
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Use a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";
            // Apply ZIP (Flate) compression to streams (including text and images)
            pdfOptions.TextCompression = PdfTextCompression.Flate;
            // Explicitly set the save format
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as a PDF with compression
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
    }