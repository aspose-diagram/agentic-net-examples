using System;
using System.IO;
using Aspose.Diagram;

class VisioShapeExtractor
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path and output CSV file path
            string visioFilePath = "input.vsdx";
            string csvOutputPath = "shapes.csv";

            // Load the Visio diagram using the appropriate constructor
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Prepare a StreamWriter for CSV output
                using (StreamWriter writer = new StreamWriter(csvOutputPath))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Left,Top,Right,Bottom");

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve shape ID
                            long shapeId = shape.ID;

                            // Retrieve positioning information from XForm
                            // PinX and PinY represent the center of the shape
                            // Width and Height represent the size of the shape
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Calculate bounding rectangle coordinates
                            double left = pinX - width / 2.0;
                            double right = pinX + width / 2.0;
                            double top = pinY + height / 2.0;
                            double bottom = pinY - height / 2.0;

                            // Write shape data to CSV
                            writer.WriteLine($"{shapeId},{left},{top},{right},{bottom}");
                        }
                    }
                }
            }

            Console.WriteLine("Shape extraction completed. CSV saved to: " + csvOutputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
