using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace 1 with the actual shape ID you want to modify)
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // ------------------------------------------------------------
                // Align the shape's text to the left side of the shape
                // ------------------------------------------------------------

                // 1. Ensure the text block has no rotation (orientation = 0 radians)
                shape.TextXForm.TxtAngle.Value = 0; // 0 radians = 0 degrees

                // 2. Set the horizontal local pin of the text block to the left edge
                //    TxtLocPinX = 0 positions the text block's local origin at its left side.
                shape.TextXForm.TxtLocPinX.Value = 0;

                // 3. Position the text block so that its left edge aligns with the shape's left edge.
                //    The shape's left edge X coordinate is PinX - (Width / 2).
                double shapeLeftEdge = shape.XForm.PinX.Value - (shape.XForm.Width.Value / 2);
                shape.TextXForm.TxtPinX.Value = shapeLeftEdge;

                // 4. (Optional) Vertically center the text within the shape.
                //    This places the text block's vertical pin at the shape's vertical center.
                shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape text aligned left and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }