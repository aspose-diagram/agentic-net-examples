using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // ---------- Page 1 ----------
            Page page1 = new Page();
            diagram.Pages.Add(page1);
            page1.Name = "Page1";

            // Define triangle vertices (in inches) for page 1 as a flat double array
            double[] triangle1Points = new double[]
            {
                2, 2,   // Vertex A (PinX, PinY)
                4, 2,   // Vertex B
                3, 4,   // Vertex C
                2, 2    // Close the polygon by returning to A
            };

            // Draw the triangle on page 1
            long triangle1Id = page1.DrawPolyline(triangle1Points);
            Shape triangle1 = page1.Shapes.GetShape((int)triangle1Id);

            // Set fill color to red and solid fill pattern
            triangle1.Fill.FillPattern.Value = 1;               // Solid fill
            triangle1.Fill.FillForegnd.Value = "#FF0000";       // Red
            // Optional: set line color and weight
            triangle1.Line.LineColor.Value = "#000000";         // Black border
            triangle1.Line.LineWeight.Value = 0.02;            // Thin line

            // ---------- Page 2 ----------
            Page page2 = new Page();
            diagram.Pages.Add(page2);
            page2.Name = "Page2";

            // Define triangle vertices (in inches) for page 2 as a flat double array
            double[] triangle2Points = new double[]
            {
                2, 2,     // Vertex A
                5, 2,     // Vertex B (wider base)
                3.5, 5,   // Vertex C (higher)
                2, 2      // Close the polygon
            };

            // Draw the triangle on page 2
            long triangle2Id = page2.DrawPolyline(triangle2Points);
            Shape triangle2 = page2.Shapes.GetShape((int)triangle2Id);

            // Set fill color to blue and solid fill pattern
            triangle2.Fill.FillPattern.Value = 1;               // Solid fill
            triangle2.Fill.FillForegnd.Value = "#0000FF";       // Blue
            // Optional: set line color and weight
            triangle2.Line.LineColor.Value = "#000000";         // Black border
            triangle2.Line.LineWeight.Value = 0.02;            // Thin line

            // Save the diagram to a VSDX file
            diagram.Save("TwoPageTriangles.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}