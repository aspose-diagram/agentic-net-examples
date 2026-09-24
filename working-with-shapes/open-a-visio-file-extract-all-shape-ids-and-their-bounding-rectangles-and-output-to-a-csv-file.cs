using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (modify as needed)
            string inputPath = "input.vsdx";

            // Output CSV file path
            string csvPath = "shapes.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create CSV file and write header
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("ShapeID,Left,Top,Right,Bottom");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve position and size (in inches)
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
                        writer.WriteLine($"{shape.ID},{left},{top},{right},{bottom}");
                    }
                }
            }

            Console.WriteLine($"Shape data exported to '{csvPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
