using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Use the first page (a new diagram contains one default page)
        Page page = diagram.Pages[0];

        // Start measuring time
        Stopwatch timer = Stopwatch.StartNew();

        // Add 100 rectangle shapes
        for (int i = 0; i < 100; i++)
        {
            // Example positioning – you can adjust as needed
            double pinX = 1.0 + i * 0.1; // X coordinate of the rectangle's center
            double pinY = 1.0;           // Y coordinate (constant for simplicity)
            double width = 0.5;          // Width in inches
            double height = 0.3;         // Height in inches

            // DrawRectangle adds a rectangle to the page
            page.DrawRectangle(pinX, pinY, width, height);
        }

        // Stop timing
        timer.Stop();

        // Log the elapsed time
        Console.WriteLine($"Time to add 100 rectangles: {timer.ElapsedMilliseconds} ms");

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
