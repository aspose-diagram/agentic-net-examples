using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page in the document
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page to use as the source for duplication
                Shape originalShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    originalShape = shp;
                    break;
                }

                if (originalShape == null)
                {
                    Console.WriteLine("No shapes found on the page to duplicate.");
                    return;
                }

                // Master name to use for new shapes (fallback to a basic rectangle if master is missing)
                string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

                // Base position and size of the original shape
                double basePinX = originalShape.XForm.PinX.Value;
                double basePinY = originalShape.XForm.PinY.Value;
                double shapeWidth = originalShape.XForm.Width.Value;
                double shapeHeight = originalShape.XForm.Height.Value;

                // Duplication settings
                int numberOfCopies = 5;          // how many duplicates to create
                double offsetX = 1.0;            // horizontal offset per copy (in inches)
                double offsetY = 0.5;            // vertical offset per copy (in inches)

                // Create duplicates with incremental offsets
                for (int i = 1; i <= numberOfCopies; i++)
                {
                    double newPinX = basePinX + i * offsetX;
                    double newPinY = basePinY + i * offsetY;

                    // Add a new shape based on the same master; AddShape returns a unique shape ID
                    long newShapeId = page.AddShape(newPinX, newPinY, masterName);

                    // Retrieve the newly added shape to copy additional properties
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Preserve the original size
                    newShape.XForm.Width.Value = shapeWidth;
                    newShape.XForm.Height.Value = shapeHeight;

                    // (Optional) Copy visual formatting such as fill and line if desired
                    newShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                    newShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
                    newShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with duplicated shapes to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }