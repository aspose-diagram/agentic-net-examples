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

                // Work with the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at (2,2) inches
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // Modify geometry: move to (5,5) and resize to 2" width x 1" height
                shape.XForm.PinX.Value = 5.0;
                shape.XForm.PinY.Value = 5.0;
                shape.XForm.Width.Value = 2.0;
                shape.XForm.Height.Value = 1.0;

                // Add a comment (annotation) to document the change rationale
                string commentText = "Moved shape to (5,5) and resized to 2\" x 1\" (inches).";
                page.AddComment(shape, commentText);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }