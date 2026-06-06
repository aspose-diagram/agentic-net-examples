using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (values are in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define the desired width of each grid cell (including shape width and spacing)
                // For this example we use a fixed cell width of 2 inches
                double cellWidth = 2.0;
                double cellHeight = 2.0; // Fixed cell height for vertical spacing

                // Compute how many columns can fit within the page width
                int columnCount = (int)Math.Floor(pageWidth / cellWidth);
                if (columnCount < 1)
                    columnCount = 1; // Ensure at least one column

                // Position each shape in a grid layout
                int shapeIndex = 0;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        shapeIndex++;
                        continue;
                    }

                    // Calculate row and column for the current shape
                    int row = shapeIndex / columnCount;
                    int col = shapeIndex % columnCount;

                    // Compute the center position of the shape within its cell
                    double pinX = (col + 0.5) * cellWidth;
                    // Visio's Y coordinate grows upwards; we start from the top margin
                    double pinY = pageHeight - ((row + 0.5) * cellHeight);

                    // Apply the new position
                    shape.XForm.PinX.Value = pinX;
                    shape.XForm.PinY.Value = pinY;

                    shapeIndex++;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }