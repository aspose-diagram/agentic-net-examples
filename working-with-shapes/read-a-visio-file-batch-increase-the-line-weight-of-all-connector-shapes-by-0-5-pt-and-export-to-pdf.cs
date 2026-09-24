using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioConnectorLineWeightUpdater <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Increment value for 0.5 point in inches (1 point = 1/72 inch)
                double incrementInInches = 0.5 / 72.0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Ensure the line weight cell exists
                            if (shape.Line != null && shape.Line.LineWeight != null)
                            {
                                // Increase line weight by 0.5 pt
                                shape.Line.LineWeight.Value += incrementInInches;
                            }
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";               // Fallback font
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // Explicitly set format

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Successfully saved PDF to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    }