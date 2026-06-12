using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // 0 - input Visio file path (e.g., "input.vsdx")
            // 1 - ID of the shape to duplicate (e.g., "123")
            // 2 - number of rows in the grid (e.g., "3")
            // 3 - number of columns in the grid (e.g., "4")
            // 4 - horizontal spacing between shapes (in inches, e.g., "0.5")
            // 5 - vertical spacing between shapes (in inches, e.g., "0.5")
            // 6 - output PDF file path (e.g., "output.pdf")
            if (args.Length < 7)
            {
                Console.WriteLine("Insufficient arguments. Usage:");
                Console.WriteLine("VisioGridDuplicate <inputVisio> <shapeId> <rows> <cols> <spacingX> <spacingY> <outputPdf>");
                return;
            }

            string inputPath = args[0];
            if (!long.TryParse(args[1], out long originalShapeId))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }

            if (!int.TryParse(args[2], out int rows) || rows <= 0)
            {
                Console.WriteLine("Invalid number of rows.");
                return;
            }

            if (!int.TryParse(args[3], out int cols) || cols <= 0)
            {
                Console.WriteLine("Invalid number of columns.");
                return;
            }

            if (!double.TryParse(args[4], out double spacingX))
            {
                Console.WriteLine("Invalid horizontal spacing.");
                return;
            }

            if (!double.TryParse(args[5], out double spacingY))
            {
                Console.WriteLine("Invalid vertical spacing.");
                return;
            }

            string outputPdfPath = args[6];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);
            // Use the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape to duplicate
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                Console.WriteLine($"Shape with ID {originalShapeId} not found on the first page.");
                return;
            }

            // Ensure the shape has an associated master (required for duplication)
            if (originalShape.Master == null)
            {
                Console.WriteLine("The specified shape does not have an associated master and cannot be duplicated via AddShape.");
                return;
            }

            string masterName = originalShape.Master.Name;

            // Capture original shape dimensions and position
            double shapeWidth = originalShape.XForm.Width.Value;
            double shapeHeight = originalShape.XForm.Height.Value;
            double startX = originalShape.XForm.PinX.Value;
            double startY = originalShape.XForm.PinY.Value;

            // Duplicate the shape in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    double posX = startX + col * (shapeWidth + spacingX);
                    double posY = startY + row * (shapeHeight + spacingY);

                    // Add a new shape based on the same master at the calculated position
                    long newShapeId = page.AddShape(posX, posY, masterName);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy the text from the original shape (if any)
                    string originalText = originalShape.Text.Value.Text;
                    if (!string.IsNullOrWhiteSpace(originalText))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(originalText));
                    }
                }
            }

            // Export the diagram to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine($"Diagram saved to PDF at: {outputPdfPath}");
        }
    }