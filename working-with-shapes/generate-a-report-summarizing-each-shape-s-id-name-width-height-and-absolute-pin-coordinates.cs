using System;
using System.Text;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeReportGenerator
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare a StringBuilder to collect report lines
            StringBuilder reportBuilder = new StringBuilder();
            // Header line
            reportBuilder.AppendLine("ShapeID,Name,Width,Height,PinX,PinY");

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve required properties
                    long shapeId = shape.ID;
                    string shapeName = shape.Name ?? string.Empty;

                    // XForm contains positioning information
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    // Append a CSV line for the current shape
                    reportBuilder.AppendLine($"{shapeId},{shapeName},{width},{height},{pinX},{pinY}");
                }
            }

            // Write the report to a CSV file
            File.WriteAllText("ShapeReport.csv", reportBuilder.ToString());

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
