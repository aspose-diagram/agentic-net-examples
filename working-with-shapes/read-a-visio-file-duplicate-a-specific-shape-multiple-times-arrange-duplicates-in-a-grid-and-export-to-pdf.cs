using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input Visio file and output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the shape to duplicate (identified by its universal name)
            Shape originalShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "TargetShape")
                {
                    originalShape = shape;
                    break;
                }
            }

            if (originalShape == null)
            {
                throw new Exception("Shape with NameU 'TargetShape' not found.");
            }

            // Get the master name of the original shape (used for creating duplicates)
            string masterName = originalShape.Master?.Name;
            if (string.IsNullOrEmpty(masterName))
            {
                throw new Exception("Original shape does not have an associated master.");
            }

            // Grid configuration
            int rows = 3;               // number of rows
            int cols = 4;               // number of columns
            double startX = 2.0;        // starting X coordinate (in inches)
            double startY = 2.0;        // starting Y coordinate (in inches)
            double offsetX = 2.0;       // horizontal spacing between shapes
            double offsetY = 2.0;       // vertical spacing between shapes

            // Duplicate the shape and arrange copies in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    double pinX = startX + col * offsetX;
                    double pinY = startY + row * offsetY;

                    // Add a new shape based on the original master
                    long newShapeId = page.AddShape(pinX, pinY, masterName, false);

                    // Retrieve the newly added shape to set its text (optional)
                    Shape newShape = page.Shapes.GetShape(newShapeId);
                    if (newShape != null)
                    {
                        // Copy the text from the original shape
                        string originalText = originalShape.Text.Value.Text;
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(originalText));
                    }
                }
            }

            // Export the diagram to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
