using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output PDF file path
        string outputPath = "grid_diagram.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first page (do not use ActivePage)
                Page page = diagram.Pages[0];

                // Grid configuration
                int rows = 3;
                int columns = 4;
                double shapeWidth = 2.0;   // inches
                double shapeHeight = 1.0;  // inches
                double horizontalSpacing = 3.0; // inches between shape centers
                double verticalSpacing = 2.5;   // inches between shape centers
                double startX = shapeWidth; // initial X position (center of first shape)
                double startY = shapeHeight; // initial Y position (center of first shape)

                // Add rectangle shapes in a grid layout
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        // Calculate the center position for the current shape
                        double pinX = startX + col * horizontalSpacing;
                        double pinY = startY + row * verticalSpacing;

                        // Add a rectangle shape using the built‑in master name "Rectangle"
                        // The fourth parameter (isCalculate) is a plain bool; we set it to false
                        long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

                        // Retrieve the shape object to modify its properties
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            // Set explicit width and height for the shape
                            shape.XForm.Width.Value = shapeWidth;
                            shape.XForm.Height.Value = shapeHeight;

                            // Clear any existing text and add a label indicating grid position
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt($"R{row + 1}C{col + 1}"));
                        }
                    }
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Set a default font to avoid missing‑font warnings
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
    }
}