using System;
using Aspose.Diagram;

namespace DiagramDimensionLogger
{
    // Helper class providing logging wrappers for dimension changes
    public static class ShapeDimensionLogger
    {
        // Logs old and new width values, then applies the new width
        public static void SetWidthWithLog(Shape shape, double newWidth)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            double oldWidth = shape.XForm.Width.Value;
            shape.SetWidth(newWidth);
            Console.WriteLine($"Shape ID {shape.ID}: Width changed from {oldWidth} to {newWidth}");
        }

        // Logs old and new height values, then applies the new height
        public static void SetHeightWithLog(Shape shape, double newHeight)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            double oldHeight = shape.XForm.Height.Value;
            shape.SetHeight(newHeight);
            Console.WriteLine($"Shape ID {shape.ID}: Height changed from {oldHeight} to {newHeight}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                var page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Example dimension changes with logging
                ShapeDimensionLogger.SetWidthWithLog(shape, 2.5);   // Set new width to 2.5 inches
                ShapeDimensionLogger.SetHeightWithLog(shape, 1.8); // Set new height to 1.8 inches

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}