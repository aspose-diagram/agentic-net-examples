using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Original rectangle parameters
            double pinX = 5.0;   // X coordinate of the rectangle's pin (center of rotation)
            double pinY = 5.0;   // Y coordinate of the rectangle's pin
            double width = 2.0; // Original width
            double height = 1.0; // Original height

            // Draw the original rectangle (optional, for reference)
            page.DrawRectangle(pinX, pinY, width, height);

            // Scale factor to double the size while keeping aspect ratio
            double scaleFactor = 2.0;

            // Calculate new dimensions
            double newWidth = width * scaleFactor;
            double newHeight = height * scaleFactor;

            // Draw the scaled rectangle with doubled width and height
            page.DrawRectangle(pinX, pinY, newWidth, newHeight);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
