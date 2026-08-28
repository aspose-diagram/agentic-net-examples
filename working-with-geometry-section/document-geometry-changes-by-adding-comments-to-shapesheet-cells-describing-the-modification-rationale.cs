using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (if any)
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Get the shape by its ID
                long shapeId = page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Record original position for comment
                double originalPinX = shape.XForm.PinX.Value;
                double originalPinY = shape.XForm.PinY.Value;

                // Modify geometry: move the shape by a specific offset
                double offsetX = 1.0; // inches
                double offsetY = 0.5; // inches
                shape.XForm.PinX.Value += offsetX;
                shape.XForm.PinY.Value += offsetY;

                // Add a comment annotation describing the change
                string commentText = $"Geometry updated: moved from ({originalPinX:F2}, {originalPinY:F2}) " +
                                     $"to ({shape.XForm.PinX.Value:F2}, {shape.XForm.PinY.Value:F2}) " +
                                     $"by offsets X={offsetX}in, Y={offsetY}in.";
                page.AddComment(shape, commentText);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'. Comment added to shape ID {shapeId}.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }