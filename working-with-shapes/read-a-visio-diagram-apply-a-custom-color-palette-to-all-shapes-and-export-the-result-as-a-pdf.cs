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
                    "#FF5733", // Red‑orange
                    "#33FF57", // Green
                    "#3357FF", // Blue
                    "#F1C40F", // Yellow
                    "#9B59B6", // Purple
                    "#E67E22", // Orange
                    "#1ABC9C", // Turquoise
                    "#E74C3C", // Red
                    "#2ECC71", // Emerald
                    "#3498DB"  // Light blue
                };

                // Apply the palette to every shape in every page
                int colorIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set a solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // 1 = solid

                        // Assign a fill foreground color from the palette
                        shape.Fill.FillForegnd.Value = palette[colorIndex % palette.Length];
                        colorIndex++;
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // Fallback font for missing characters

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }