using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Determine the highest existing page ID (will be 0 for a fresh diagram)
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Add a new page with a unique ID
            Page newPage = new Page(maxPageId + 1);
            diagram.Pages.Add(newPage);

            // Define oval (ellipse) position and size in inches
            double pinX = 2.0;   // center X coordinate
            double pinY = 3.0;   // center Y coordinate
            double width = 4.0;  // horizontal diameter
            double height = 2.0; // vertical diameter

            // Insert the oval shape onto the new page
            long shapeId = newPage.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the shape to set additional properties (optional)
            Shape oval = newPage.Shapes.GetShape((int)shapeId);
            oval.Fill.FillForegnd.Value = "#00AAFF"; // light blue fill color

            // Save the diagram to VSDX format
            diagram.Save("OvalDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
