using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (default constructor creates a diagram with one page)
        Diagram diagram = new Diagram();

        // Get the first page where rectangles will be added
        Page page = diagram.Pages[0];

        // Start measuring the time taken to add 100 rectangles
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Add 100 rectangle shapes using the DrawRectangle method
        for (int i = 0; i < 100; i++)
        {
            // Example positioning; adjust as needed
            double pinX = 1.0 + i * 0.1; // X coordinate of the rectangle's pin
            double pinY = 1.0;          // Y coordinate of the rectangle's pin
            double width = 0.5;         // Width of the rectangle (in inches)
            double height = 0.3;        // Height of the rectangle (in inches)

            // Draw the rectangle on the page
            page.DrawRectangle(pinX, pinY, width, height);
        }

        // Stop the timer
        stopwatch.Stop();

        // Log the elapsed time in milliseconds
        Console.WriteLine($"Time to add 100 rectangles: {stopwatch.ElapsedMilliseconds} ms");

        // Optional: save the diagram to verify the shapes were added
        diagram.Save("RectanglesDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
