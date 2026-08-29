using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape originalShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        originalShape = shape;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    throw new Exception("No non‑deleted shape found to duplicate.");
                }

                // Retrieve the master name of the original shape
                string masterName = originalShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    throw new Exception("Original shape does not have an associated master.");
                }

                // Get the original shape's position
                double originalPinX = originalShape.XForm.PinX.Value;
                double originalPinY = originalShape.XForm.PinY.Value;

                // Define an offset for the duplicated shape (e.g., 2 inches right and down)
                double offsetX = 2.0;
                double offsetY = 2.0;

                double newPinX = originalPinX + offsetX;
                double newPinY = originalPinY + offsetY;

                // Add a new shape using the same master and the adjusted position
                long newShapeId = page.AddShape(newPinX, newPinY, masterName);

                // Optionally retrieve the newly added shape (not required for duplication)
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }