using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Retrieve the page width (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

            // Configuration for the grid layout
            double shapeWidth = 2.0;      // width of each shape in inches
            double shapeHeight = 1.0;     // height of each shape in inches
            double horizontalSpacing = 0.5; // space between columns in inches
            double verticalSpacing = 0.5;   // space between rows in inches
            double leftMargin = 1.0;        // left margin from page edge in inches
            double topMargin = 1.0;         // top margin from page edge in inches

            // Compute how many columns can fit within the page width
            int columns = (int)Math.Floor(pageWidth / (shapeWidth + horizontalSpacing));
            if (columns < 1) columns = 1; // Ensure at least one column

            // Position shapes in a grid
            int index = 0;
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                int col = index % columns;
                int row = index / columns;

                // Calculate the center position (PinX, PinY) for the shape
                double pinX = leftMargin + col * (shapeWidth + horizontalSpacing) + shapeWidth / 2.0;
                double pinY = topMargin + row * (shapeHeight + verticalSpacing) + shapeHeight / 2.0;

                shape.XForm.PinX.Value = pinX;
                shape.XForm.PinY.Value = pinY;
                shape.XForm.Width.Value = shapeWidth;
                shape.XForm.Height.Value = shapeHeight;

                index++;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
