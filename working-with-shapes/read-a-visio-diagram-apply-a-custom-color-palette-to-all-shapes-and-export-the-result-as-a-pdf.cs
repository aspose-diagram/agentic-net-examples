using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define a custom color palette (hex color strings)
                string[] palette = new string[]
                {
                    "#FF5733", // reddish
                    "#33FF57", // greenish
                    "#3357FF", // bluish
                    "#FF33A8", // pink
                    "#A833FF", // purple
                    "#33FFF5"  // cyan
                };

                // Apply colors to all shapes in all pages
                int colorIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Fill object
                        if (shape.Fill != null)
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid
                            // Assign a color from the palette
                            shape.Fill.FillForegnd.Value = palette[colorIndex % palette.Length];
                            colorIndex++;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }