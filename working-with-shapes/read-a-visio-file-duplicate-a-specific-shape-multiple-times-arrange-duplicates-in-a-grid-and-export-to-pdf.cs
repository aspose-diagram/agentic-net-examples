using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate required arguments: input file, shape name, rows, columns, output PDF.
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <shapeName> <rows> <columns> <outputPdf>");
            return;
        }

        // Assign and guard input Visio file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign shape name to duplicate.
        string targetShapeName = args[1];

        // Parse grid row count with validation.
        if (!int.TryParse(args[2], out int rowCount) || rowCount <= 0)
        {
            Console.Error.WriteLine("Invalid row count.");
            return;
        }

        // Parse grid column count with validation.
        if (!int.TryParse(args[3], out int columnCount) || columnCount <= 0)
        {
            Console.Error.WriteLine("Invalid column count.");
            return;
        }

        // Assign and guard output PDF file path's directory.
        string outputPath = args[4];
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram has no pages.");
                return;
            }

            // Work with the first page (index 0).
            Page page = diagram.Pages[0];

            // Locate the shape to duplicate by its universal name (case‑insensitive).
            Shape? originalShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Compare NameU property; ignore case.
                if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                {
                    originalShape = shape;
                    break;
                }
            }

            // Abort if the target shape was not found.
            if (originalShape == null)
            {
                Console.Error.WriteLine($"Shape named \"{targetShapeName}\" not found on the first page.");
                return;
            }

            // Guard against missing master (required for adding new shapes).
            if (originalShape.Master == null)
            {
                Console.Error.WriteLine("The target shape does not have an associated master.");
                return;
            }

            // Retrieve master name for reuse.
            string masterName = originalShape.Master.Name;

            // Retrieve original shape dimensions (in inches).
            double shapeWidth = originalShape.XForm.Width.Value;
            double shapeHeight = originalShape.XForm.Height.Value;

            // Determine starting position (top‑left corner) based on original shape's PinX/Y.
            double startX = originalShape.XForm.PinX.Value - shapeWidth / 2.0;
            double startY = originalShape.XForm.PinY.Value - shapeHeight / 2.0;

            // Define spacing between shapes (in inches).
            const double horizontalSpacing = 0.5;
            const double verticalSpacing = 0.5;

            // Loop through rows and columns to create duplicates.
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    // Compute new shape center coordinates.
                    double newPinX = startX + col * (shapeWidth + horizontalSpacing) + shapeWidth / 2.0;
                    double newPinY = startY + row * (shapeHeight + verticalSpacing) + shapeHeight / 2.0;

                    // Add a new shape using the same master; isCalculate = false.
                    long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                    // Retrieve the newly added shape for optional further customization.
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Example: copy the text from the original shape to the duplicate.
                    if (!string.IsNullOrWhiteSpace(originalShape.Text.Value.ToString()))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(originalShape.Text.Value.ToString()));
                    }
                }
            }

            // Prepare PDF save options (set a default font to avoid missing‑font warnings).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as a PDF file.
            diagram.Save(outputPath, pdfOptions);

            // Inform the user of successful completion.
            Console.WriteLine($"Diagram exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}