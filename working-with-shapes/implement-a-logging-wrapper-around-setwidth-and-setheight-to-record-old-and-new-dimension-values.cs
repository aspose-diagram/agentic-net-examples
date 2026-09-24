using System;
using Aspose.Diagram;

namespace DiagramDimensionLogger
{
    // Extension methods to wrap SetWidth and SetHeight with logging
    public static class ShapeDimensionLogger
    {
        // Logs old and new width, then sets the new width
        public static void SetWidthLogged(this Shape shape, double newWidth)
        {
            // Retrieve current width (in inches)
            double oldWidth = shape.XForm.Width.Value;
            Console.WriteLine($"[Log] Shape ID {shape.ID}: Width changing from {oldWidth} to {newWidth}");
            // Apply the new width
            shape.SetWidth(newWidth);
        }

        // Logs old and new height, then sets the new height
        public static void SetHeightLogged(this Shape shape, double newHeight)
        {
            // Retrieve current height (in inches)
            double oldHeight = shape.XForm.Height.Value;
            Console.WriteLine($"[Log] Shape ID {shape.ID}: Height changing from {oldHeight} to {newHeight}");
            // Apply the new height
            shape.SetHeight(newHeight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the actual file path
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Example: modify the first shape on the page
                // Retrieve the shape by its ID
                // Replace 1 with the actual shape ID you want to modify
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Log and set new dimensions using the wrapper methods
                shape.SetWidthLogged(2.5);   // Set width to 2.5 inches
                shape.SetHeightLogged(1.8);  // Set height to 1.8 inches

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}