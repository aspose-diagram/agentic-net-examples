using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class AutoSpaceLogger
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // Replace with your actual file path
            string inputPath = @"C:\Diagrams\sample.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or any specific page you want to work with)
            Page page = diagram.Pages[0];

            // Prepare auto‑spacing options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                // Example distances in inches; adjust as needed
                DistanceInHorizontal = 0.5f,
                DistanceInVertical = 0.5f
            };

            // Perform auto‑spacing on all shapes of the page
            page.AutoSpaceShapes(page.Shapes, options);

            // After auto‑spacing, log each shape's new position
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has position data (some shapes may be groups)
                if (shape.XForm != null && shape.XForm.PinX != null && shape.XForm.PinY != null)
                {
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, New Position -> X: {pinX:F2}, Y: {pinY:F2}");
                }
                else
                {
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name} does not have position data.");
                }
            }

            // Optionally save the modified diagram
            // Replace with your desired output path
            string outputPath = @"C:\Diagrams\sample_autospaced.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
