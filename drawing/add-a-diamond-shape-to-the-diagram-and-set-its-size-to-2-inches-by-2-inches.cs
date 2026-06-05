using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a blank page to the diagram
            Page page = new Page();
            // Determine a unique page ID
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }
            page.ID = maxPageId + 1;
            page.Name = "Page-1";
            diagram.Pages.Add(page);

            // Draw a diamond shape using a polyline.
            // Coordinates define a closed diamond centered at (5,5) with width and height of 2 inches.
            long shapeId = page.DrawPolyline(new double[]
            {
                5, 6,   // top vertex
                6, 5,   // right vertex
                5, 4,   // bottom vertex
                4, 5,   // left vertex
                5, 6    // close back to top
            });

            // Retrieve the shape object to set its size explicitly
            Shape diamond = page.Shapes.GetShape((int)shapeId);
            diamond.XForm.Width.Value = 2;   // 2 inches width
            diamond.XForm.Height.Value = 2;  // 2 inches height
            diamond.XForm.PinX.Value = 5;    // center X
            diamond.XForm.PinY.Value = 5;    // center Y

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }