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

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify (example: shape with ID 1)
                // Adjust the ID as needed for your specific diagram
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape exists
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {shapeId} not found.");
                }

                // Set custom text pin coordinates (in inches)
                // These coordinates define where the text block is positioned within the shape
                double customPinX = 2.0; // example X coordinate
                double customPinY = 1.5; // example Y coordinate

                shape.TextXForm.TxtPinX.Value = customPinX;
                shape.TextXForm.TxtPinY.Value = customPinY;

                // Optionally, update the shape's text to see the effect
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Pinned Text"));

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