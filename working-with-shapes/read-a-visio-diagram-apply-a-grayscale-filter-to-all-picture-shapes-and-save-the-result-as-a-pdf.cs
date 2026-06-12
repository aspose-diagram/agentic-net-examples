using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: VisioGrayscalePdf <input.vsdx> <output.pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture (foreign) shapes
                    if (shape.Type == TypeValue.Foreign && shape.Image != null)
                    {
                        // Apply a simple grayscale effect by adjusting image properties
                        // (Aspose.Diagram does not provide a direct grayscale filter,
                        //  so we approximate it by setting gamma, brightness, and contrast.)
                        try
                        {
                            shape.Image.Gamma.Value = 0.5;        // Reduce gamma
                            shape.Image.Brightness.Value = 0;    // Neutral brightness
                            shape.Image.Contrast.Value = 0;      // Neutral contrast
                        }
                        catch (Exception imgEx)
                        {
                            Console.WriteLine($"Failed to adjust image on shape ID {shape.ID}: {imgEx.Message}");
                        }
                    }
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };

            // Save the modified diagram as PDF
            try
            {
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram saved as PDF to: {outputPath}");
            }
            catch (Exception saveEx)
            {
                Console.WriteLine($"Failed to save PDF: {saveEx.Message}");
            }
        }
    }