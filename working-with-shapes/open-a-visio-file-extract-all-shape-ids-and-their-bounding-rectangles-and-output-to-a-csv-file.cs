using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to process
            string inputPath = "input.vsdx";

            // Path for the CSV output
            string outputCsv = "shapes.csv";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Create a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,Left,Top,Right,Bottom");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Shape identifier
                        long id = shape.ID;

                        // Retrieve position and size (center based)
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate bounding rectangle coordinates
                        double left = pinX - width / 2.0;
                        double right = pinX + width / 2.0;
                        double top = pinY + height / 2.0;
                        double bottom = pinY - height / 2.0;

                        // Write a line to the CSV file
                        writer.WriteLine($"{id},{left},{top},{right},{bottom}");
                    }
                }
            }

            Console.WriteLine($"Shape data exported to '{outputCsv}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
