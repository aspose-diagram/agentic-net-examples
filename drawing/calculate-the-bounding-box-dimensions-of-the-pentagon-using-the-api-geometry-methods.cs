using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram
        Diagram diagram = null;
        try
        {
            diagram = new Diagram();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating diagram: {ex.Message}");
            return;
        }

        // Use the first (default) page; a new Diagram always contains one page
        Page page = null;
        try
        {
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram has no pages.");
                return;
            }
            page = diagram.Pages[0];
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error accessing page: {ex.Message}");
            return;
        }

        // Define pentagon vertices (regular pentagon centered at (0,0) with radius 2 inches)
        double radius = 2.0;
        double centerX = 0.0;
        double centerY = 0.0;
        double angleOffset = -Math.PI / 2; // start at top vertex

        double[] coords = new double[12]; // 6 points (including closing) * (x,y)
        for (int i = 0; i < 5; i++)
        {
            double angle = angleOffset + i * 2 * Math.PI / 5;
            double x = centerX + radius * Math.Cos(angle);
            double y = centerY + radius * Math.Sin(angle);
            coords[i * 2] = x;
            coords[i * 2 + 1] = y;
        }
        // Close the polygon by repeating the first point
        coords[10] = coords[0];
        coords[11] = coords[1];

        // Draw the pentagon on the page
        long pentagonId = 0;
        try
        {
            pentagonId = page.DrawPolyline(coords);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error drawing pentagon: {ex.Message}");
            return;
        }

        // Retrieve the shape object for the pentagon
        Shape pentagonShape = null;
        try
        {
            pentagonShape = page.Shapes.GetShape(pentagonId);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error retrieving pentagon shape: {ex.Message}");
            return;
        }

        // Calculate bounding box using geometry data
        double minX = double.MaxValue;
        double maxX = double.MinValue;
        double minY = double.MaxValue;
        double maxY = double.MinValue;

        try
        {
            foreach (Geom geom in pentagonShape.Geoms)
            {
                // Each Geom contains a collection of geometry commands (MoveTo, LineTo, etc.)
                foreach (object segment in geom.CoordinateCol)
                {
                    if (segment is MoveTo move)
                    {
                        double x = move.X.Value;
                        double y = move.Y.Value;
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                    else if (segment is LineTo line)
                    {
                        double x = line.X.Value;
                        double y = line.Y.Value;
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                    // Other segment types (ArcTo, etc.) are not used for this pentagon
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error calculating bounding box: {ex.Message}");
            return;
        }

        double width = maxX - minX;
        double height = maxY - minY;

        Console.WriteLine("Pentagon Bounding Box:");
        Console.WriteLine($"  MinX: {minX}");
        Console.WriteLine($"  MaxX: {maxX}");
        Console.WriteLine($"  MinY: {minY}");
        Console.WriteLine($"  MaxY: {maxY}");
        Console.WriteLine($"  Width: {width}");
        Console.WriteLine($"  Height: {height}");
    }
}