using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_grid.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the page width (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Define desired shape dimensions and spacing (in inches)
                double shapeWidth = 1.0;      // Width of each shape
                double shapeHeight = 0.6;     // Height of each shape
                double horizontalSpacing = 0.2;
                double verticalSpacing = 0.2;

                // Compute how many columns can fit within the page width
                int columns = (int)Math.Floor(pageWidth / (shapeWidth + horizontalSpacing));
                if (columns < 1)
                    columns = 1; // Ensure at least one column

                Console.WriteLine($"Page width: {pageWidth} inches");
                Console.WriteLine($"Calculated columns: {columns}");

                // Starting position (center of first shape)
                double startX = shapeWidth / 2 + horizontalSpacing;
                double startY = shapeHeight / 2 + verticalSpacing;

                // Index for positioning shapes
                int shapeIndex = 0;

                // Iterate over all shapes on the page and arrange them in a grid
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only regular shapes (skip connectors, groups, etc.)
                    if (shape.Type != TypeValue.Shape)
                        continue;

                    // Compute row and column for the current shape
                    int row = shapeIndex / columns;
                    int col = shapeIndex % columns;

                    // Calculate new PinX and PinY positions
                    double pinX = startX + col * (shapeWidth + horizontalSpacing);
                    double pinY = startY + row * (shapeHeight + verticalSpacing);

                    // Apply new position
                    shape.XForm.PinX.Value = pinX;
                    shape.XForm.PinY.Value = pinY;

                    // Optionally set uniform size for all shapes
                    shape.XForm.Width.Value = shapeWidth;
                    shape.XForm.Height.Value = shapeHeight;

                    shapeIndex++;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Grid layout applied and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }