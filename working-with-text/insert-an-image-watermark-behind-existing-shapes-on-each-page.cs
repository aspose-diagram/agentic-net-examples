using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class WatermarkExample
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Path to the watermark image (PNG, JPG, etc.)
            string watermarkPath = "watermark.png";

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Open a fresh stream for the image on each iteration
                using (FileStream imgStream = new FileStream(watermarkPath, FileMode.Open, FileAccess.Read))
                {
                    // Define position and size for the watermark.
                    // PinX and PinY represent the center of the shape (in inches).
                    // Width and Height define the size of the shape (in inches).
                    // Adjust these values as needed to fit the page.
                    double pinX = 5.0;    // example center X
                    double pinY = 5.0;    // example center Y
                    double width = 10.0; // example width
                    double height = 7.5; // example height

                    // Add the image as a shape to the current page
                    long shapeId = page.AddShape(pinX, pinY, width, height, imgStream);

                    // Send the newly added shape to the back so it appears behind existing shapes
                    page.SendToBack(shapeId);
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
