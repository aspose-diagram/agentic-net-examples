using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                // Shape IDs are returned as long; cast to int for GetShape
                long shapeId = page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Ensure the shape has some text (optional)
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Sample Text"));

                // Set text orientation angle (in radians). 0 degrees = 0 radians.
                double angleDegrees = 0.0;
                shape.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * angleDegrees;

                // Position text at the top of the shape.
                // TxtLocPinY = 0 aligns the text block's local pin to the top.
                // TxtPinY = shape height places the text block at the top edge of the shape.
                shape.TextXForm.TxtLocPinY.Value = 0.0;
                shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Text repositioned and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }