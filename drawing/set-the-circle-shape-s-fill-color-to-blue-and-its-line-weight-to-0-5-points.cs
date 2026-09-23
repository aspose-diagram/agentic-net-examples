using System.IO;
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

            // Iterate through all pages and shapes to find the circle shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify the circle shape (commonly named "Ellipse" master or contains "Circle" in its name)
                    bool isCircleMaster = shape.Master != null && shape.Master.Name == "Ellipse";
                    bool nameContainsCircle = !string.IsNullOrEmpty(shape.NameU) && shape.NameU.IndexOf("Circle", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (isCircleMaster || nameContainsCircle)
                    {
                        // Set fill color to blue (hex format)
                        shape.Fill.FillForegnd.Value = "#0000FF";

                        // Set line weight to 0.5 points (convert points to inches: 1 point = 1/72 inch)
                        shape.Line.LineWeight.Value = 0.5 / 72.0;

                        // If there are multiple circle shapes, continue updating each one
                    }
                }
            }

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
