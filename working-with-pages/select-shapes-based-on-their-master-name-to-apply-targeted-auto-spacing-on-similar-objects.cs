using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the master name to target for auto‑spacing
                const string targetMasterName = "Process";

                // Choose the page to work on (first page in this example)
                Page page = diagram.Pages[0];

                // Collect shape IDs that use the specified master
                var targetShapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master before accessing its name
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        targetShapeIds.Add(shape.ID);
                    }
                }

                // If no matching shapes are found, exit gracefully
                if (targetShapeIds.Count == 0)
                {
                    Console.WriteLine($"No shapes with master \"{targetMasterName}\" were found.");
                    return;
                }

                // Define spacing parameters (in inches)
                double horizontalSpacing = 1.0; // space between shapes horizontally
                double verticalSpacing = 1.0;   // space between shapes vertically

                // Determine starting position (use the position of the first matching shape)
                Shape firstShape = page.Shapes.GetShape(targetShapeIds[0]);
                double startX = firstShape.XForm.PinX.Value;
                double startY = firstShape.XForm.PinY.Value;

                // Arrange shapes in a simple grid layout
                int columns = (int)Math.Ceiling(Math.Sqrt(targetShapeIds.Count));
                int currentColumn = 0;
                int currentRow = 0;

                foreach (long shapeId in targetShapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Calculate new position
                    double newX = startX + (currentColumn * horizontalSpacing);
                    double newY = startY + (currentRow * verticalSpacing);

                    // Apply new position
                    shape.XForm.PinX.Value = newX;
                    shape.XForm.PinY.Value = newY;

                    // Move to next column; wrap to next row when needed
                    currentColumn++;
                    if (currentColumn >= columns)
                    {
                        currentColumn = 0;
                        currentRow++;
                    }
                }

                // Save the modified diagram
                string outputPath = "output_spaced.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Auto‑spacing applied to shapes with master \"{targetMasterName}\" and saved to \"{outputPath}\".");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }