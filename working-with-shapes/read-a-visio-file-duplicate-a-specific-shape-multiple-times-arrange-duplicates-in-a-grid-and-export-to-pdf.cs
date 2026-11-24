using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        // Name of the shape to duplicate (NameU property)
        string targetShapeName = "Rectangle";

        // Grid configuration
        int rows = 3;
        int columns = 4;
        double horizontalSpacing = 2.0; // inches
        double verticalSpacing = 2.0;   // inches

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for operations (avoid ActivePage per guidelines)
                Page page = diagram.Pages[0];

                // Locate the source shape by its NameU
                Shape sourceShape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.NameU == targetShapeName)
                    {
                        sourceShape = s;
                        break;
                    }
                }

                if (sourceShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU '{targetShapeName}' not found on the first page.");
                    return;
                }

                // Verify the source shape has an associated master (required for duplication)
                if (sourceShape.Master == null)
                {
                    Console.Error.WriteLine("Source shape does not have a master; cannot duplicate.");
                    return;
                }

                // Retrieve the master name to reuse for new shapes
                string masterName = sourceShape.Master.Name;

                // Base position (PinX, PinY) of the source shape
                double basePinX = sourceShape.XForm.PinX.Value;
                double basePinY = sourceShape.XForm.PinY.Value;

                // Duplicate the shape in a rows x columns grid
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        // Calculate new position for each duplicate
                        double newPinX = basePinX + col * horizontalSpacing;
                        double newPinY = basePinY + row * verticalSpacing;

                        // Add a new shape using the same master as the source shape
                        long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                        // Retrieve the newly added shape for further modifications
                        Shape newShape = page.Shapes.GetShape(newShapeId);

                        // Copy the text from the source shape to the new shape
                        string sourceText = sourceShape.Text.Value.Text;
                        if (!string.IsNullOrEmpty(sourceText))
                        {
                            // Clear any existing text and add the source text
                            newShape.Text.Value.Clear();
                            newShape.Text.Value.Add(new Txt(sourceText));
                        }
                    }
                }

                // Prepare PDF save options (optional configuration)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false; // Export only visible pages
                pdfOptions.DefaultFont = "Arial";    // Fallback font

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}