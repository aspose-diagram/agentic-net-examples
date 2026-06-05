using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (do not use ActivePage)
                Page page = diagram.Pages[0];

                // Add a rectangle shape using the built‑in "Rectangle" master
                // PinX and PinY specify the center position of the shape on the page
                double pinX = 2.0; // inches from the left edge
                double pinY = 2.0; // inches from the top edge
                long shapeId = page.AddShape(pinX, pinY, "Rectangle");

                // Retrieve the newly added shape
                Shape rectangle = page.Shapes.GetShape(shapeId);

                // Set a solid red border
                rectangle.Line.LineColor.Value = "#FF0000";

                // Save the modified diagram back to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }