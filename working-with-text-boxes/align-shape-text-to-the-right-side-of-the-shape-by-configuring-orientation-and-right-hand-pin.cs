using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example uses ID = 1)
                // Ensure the shape exists; otherwise, adjust the ID or iterate to find a target shape.
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Align the shape's text to the right side:
                // - Set the text block's local pin X to 0 (left edge of the text block)
                // - Position the text block's pin X at the shape's right edge (shape width)
                shape.TextXForm.TxtLocPinX.Value = 0;
                shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value;

                // Optional: ensure no rotation is applied to the text
                shape.TextXForm.TxtAngle.Value = 0;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }