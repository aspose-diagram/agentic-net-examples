using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Use the first page (created by default)
            Page page = diagram.Pages[0];

            // ---------- Insert Image ----------
            // Image position (center) and size in inches
            double imgPinX = 5.0;      // X coordinate of image center
            double imgPinY = 5.0;      // Y coordinate of image center
            double imgWidth = 3.0;    // Image width
            double imgHeight = 2.0;   // Image height

            // Load image file into a stream
            using (FileStream imgStream = File.OpenRead("sample.png"))
            {
                // Add the image shape to the page
                // Overload: AddShape(pinX, pinY, width, height, Stream)
                page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
            }

            // ---------- Insert Triangle Above Image ----------
            // Triangle size in inches
            double triWidth = 2.0;
            double triHeight = 1.0;

            // Position triangle directly above the image with a small gap (0.2 inches)
            double triPinX = imgPinX;
            double triPinY = imgPinY - (imgHeight / 2) - (triHeight / 2) - 0.2;

            // Add a triangle shape using the built‑in master named "Triangle"
            // Overload: AddShape(pinX, pinY, width, height, masterName)
            page.AddShape(triPinX, triPinY, triWidth, triHeight, "Triangle");

            // Save the diagram to a VDX file
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
