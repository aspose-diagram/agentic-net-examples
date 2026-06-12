using System.IO;
using System;
using Aspose.Diagram;

class WatermarkRotationExample
{
    static void Main()
    {
        try
        {

            // Configuration: rotation angle in degrees (could be read from a config file or environment variable)
            double rotationDegrees = 45.0; // Example value; replace with your configuration source

            // Load existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to locate the watermark shape.
            // Adjust the condition (e.g., shape.NameU) to match how your watermark is identified.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Watermark") // Identify watermark shape by its name
                    {
                        // Set the text block rotation angle (degrees) using TextXForm.TxtAngle
                        shape.TextXForm.TxtAngle.Value = rotationDegrees;

                        // Optionally, also rotate the entire shape.
                        // Shape.SetAngle expects radians, so convert degrees to radians.
                        double rotationRadians = rotationDegrees * Math.PI / 180.0;
                        shape.SetAngle(rotationRadians);
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path and format)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
