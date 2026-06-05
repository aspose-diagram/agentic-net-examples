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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Desired line thickness (in inches)
            double lineThickness = 0.02; // adjust as needed

            // Locate all pentagon shapes and apply the dashed line style
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        // Set dash pattern
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                        // Set line weight (thickness)
                        shape.Line.LineWeight.Value = lineThickness;
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
