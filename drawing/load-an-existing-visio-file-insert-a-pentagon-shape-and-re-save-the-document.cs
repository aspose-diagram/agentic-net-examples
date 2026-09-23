using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source and the resulting Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page of the diagram
            Page page = diagram.Pages[0];

            // Define the pentagon geometry (center at (5,5), radius 2)
            double centerX = 5.0;
            double centerY = 5.0;
            double radius = 2.0;
            double angleStep = 2 * Math.PI / 5; // 5 sides

            // Prepare a flat double array: x1, y1, x2, y2, ..., xn, yn, x1, y1 (close polygon)
            double[] points = new double[12]; // 5 points * 2 + 2 to repeat the first point
            for (int i = 0; i < 5; i++)
            {
                double angle = i * angleStep - Math.PI / 2; // start at the top vertex
                points[i * 2] = centerX + radius * Math.Cos(angle);
                points[i * 2 + 1] = centerY + radius * Math.Sin(angle);
            }
            // Close the shape by repeating the first vertex
            points[10] = points[0];
            points[11] = points[1];

            // Insert the pentagon shape onto the page
            long pentagonId = page.DrawPolyline(points);

            // Retrieve the shape object (cast long ID to int as required)
            Shape pentagon = page.Shapes.GetShape((int)pentagonId);

            // Optional styling: red fill and black outline
            pentagon.Fill.FillForegnd.Value = "#FF0000";
            pentagon.Line.LineColor.Value = "#000000";

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
