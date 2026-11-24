using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "CircularLayout.vsdx";

        try
        {
            // Create a new diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Add a new blank page to the diagram
                Page page = new Page();
                page.Name = "CircularPage";
                diagram.Pages.Add(page);

                // Circle layout parameters
                double centerX = 5.0;
                double centerY = 5.0;
                double radius = 3.0;

                // Define shape creators for five distinct shapes
                Action<int, double, double> addRectangle = (index, x, y) =>
                {
                    long id = page.DrawRectangle(x - 0.5, y - 0.5, x + 0.5, y + 0.5);
                    Shape shape = page.Shapes.GetShape((int)id);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Rect {index + 1}"));
                };

                Action<int, double, double> addEllipse = (index, x, y) =>
                {
                    long id = page.DrawEllipse(x, y, 1.0, 1.0);
                    Shape shape = page.Shapes.GetShape((int)id);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Ellipse {index + 1}"));
                };

                Action<int, double, double> addDiamond = (index, x, y) =>
                {
                    // Diamond defined by four points (polyline closed)
                    double half = 0.5;
                    double[] points = new double[]
                    {
                        x, y - half,   // top
                        x + half, y,   // right
                        x, y + half,   // bottom
                        x - half, y,   // left
                        x, y - half    // close back to top
                    };
                    long id = page.DrawPolyline(points);
                    Shape shape = page.Shapes.GetShape((int)id);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Diamond {index + 1}"));
                };

                Action<int, double, double> addTriangle = (index, x, y) =>
                {
                    // Upward triangle
                    double half = 0.5;
                    double[] points = new double[]
                    {
                        x, y - half,       // top
                        x + half, y + half, // bottom right
                        x - half, y + half, // bottom left
                        x, y - half        // close
                    };
                    long id = page.DrawPolyline(points);
                    Shape shape = page.Shapes.GetShape((int)id);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Tri {index + 1}"));
                };

                Action<int, double, double> addHexagon = (index, x, y) =>
                {
                    double r = 0.5;
                    double[] points = new double[12];
                    for (int i = 0; i < 6; i++)
                    {
                        double angle = Math.PI / 3 * i;
                        points[i * 2] = x + r * Math.Cos(angle);
                        points[i * 2 + 1] = y + r * Math.Sin(angle);
                    }
                    // Close the polygon by repeating the first point
                    points = new double[]
                    {
                        points[0], points[1],
                        points[2], points[3],
                        points[4], points[5],
                        points[6], points[7],
                        points[8], points[9],
                        points[10], points[11],
                        points[0], points[1]
                    };
                    long id = page.DrawPolyline(points);
                    Shape shape = page.Shapes.GetShape((int)id);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Hex {index + 1}"));
                };

                // Add five shapes positioned around a circle
                for (int i = 0; i < 5; i++)
                {
                    double angle = 2 * Math.PI * i / 5;
                    double x = centerX + radius * Math.Cos(angle);
                    double y = centerY + radius * Math.Sin(angle);

                    // Choose a different shape type for each index
                    switch (i)
                    {
                        case 0:
                            addRectangle(i, x, y);
                            break;
                        case 1:
                            addEllipse(i, x, y);
                            break;
                        case 2:
                            addDiamond(i, x, y);
                            break;
                        case 3:
                            addTriangle(i, x, y);
                            break;
                        case 4:
                            addHexagon(i, x, y);
                            break;
                    }
                }

                // Save the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}