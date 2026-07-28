using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify.
                // Here we simply take the first shape on the page.
                // Adjust the logic if you need to target a specific shape by ID or name.
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get the first shape (ID is a long)
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Ensure the shape has a text block; otherwise, add some sample text.
                if (string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                {
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Sample Text"));
                }

                // Align text to the right side of the shape.
                // TxtLocPinX defines the local pin (reference point) inside the text block.
                // Setting it to 0 aligns the text block's left edge to the shape's right edge.
                // TxtPinX defines the absolute position of the text block within the shape.
                // Setting it to the shape's width positions the text block at the right side.
                shape.TextXForm.TxtLocPinX.Value = 0;                         // Left edge of text block
                shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value;     // Position at shape's right edge

                // Optional: Ensure no rotation is applied to the text.
                shape.TextXForm.TxtAngle.Value = 0; // Angle is in radians

                // Save the modified diagram to a new file.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with right-aligned text to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }