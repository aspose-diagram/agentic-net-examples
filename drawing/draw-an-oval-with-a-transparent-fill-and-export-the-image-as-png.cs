using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Draw an oval (ellipse) on the page
            // Parameters: pinX, pinY (center), width, height (in inches)
            long shapeId = page.DrawEllipse(5.0, 5.0, 3.0, 2.0);

            // Retrieve the shape object using the returned ID
            Shape ovalShape = page.Shapes.GetShape((int)shapeId);

            // Set the fill pattern to 0 (no fill) to make the oval transparent
            ovalShape.Fill.FillPattern.Value = 0;

            // Export the diagram as a PNG image
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("oval.png", saveOptions);
        }
    }