using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define shape geometry (center at (5,5), size 2x1 inches)
            double shapePinX = 5.0;
            double shapePinY = 5.0;
            double shapeWidth = 2.0;
            double shapeHeight = 1.0;

            // Add a rectangle shape using DrawRectangle (returns a shape ID)
            long rectShapeId = page.DrawRectangle(shapePinX, shapePinY, shapeWidth, shapeHeight);

            // Retrieve the shape instance (GetShape expects an int)
            Shape rectShape = page.Shapes.GetShape((int)rectShapeId);

            // Clear any existing text and add new text
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Centered Text"));

            // Anchor the text to the exact center of the shape's geometry
            // TxtPinX/Y are the absolute position of the text block; set them to the shape's PinX/Y
            rectShape.TextXForm.TxtPinX.Value = rectShape.XForm.PinX.Value;
            rectShape.TextXForm.TxtPinY.Value = rectShape.XForm.PinY.Value;

            // Optionally, set the local pin of the text block to its center (0.5,0.5) for perfect centering
            rectShape.TextXForm.TxtLocPinX.Value = 0.5;
            rectShape.TextXForm.TxtLocPinY.Value = 0.5;

            // Save the diagram to a VSDX file
            string outputPath = "CenteredTextShape.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }