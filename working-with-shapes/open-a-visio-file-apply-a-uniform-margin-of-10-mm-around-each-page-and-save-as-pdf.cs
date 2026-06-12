using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input Visio file path and output PDF file path as arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioMarginPdfExport <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define margin of 10 mm (converted to inches)
                double marginInches = 10.0 / 25.4; // 1 inch = 25.4 mm

                // Apply the margin to each page
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps of the page sheet
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Set all four margins
                    printProps.PageTopMargin.Value = marginInches;
                    printProps.PageBottomMargin.Value = marginInches;
                    printProps.PageLeftMargin.Value = marginInches;
                    printProps.PageRightMargin.Value = marginInches;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Successfully saved PDF to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }