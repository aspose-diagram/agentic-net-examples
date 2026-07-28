using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Indices of the source page (where the original shape resides) 
            // and the target page (where the copy will be placed)
            int sourcePageIndex = 0;
            int targetPageIndex = 1;

            // ID of the shape to duplicate on the source page
            long shapeIdToCopy = 1; // replace with the actual shape ID

            // Retrieve the source page and the shape to be copied
            Page sourcePage = diagram.Pages[sourcePageIndex];
            Shape sourceShape = sourcePage.Shapes.GetShape(shapeIdToCopy);

            // Create a new Shape instance and copy all properties from the source shape
            Shape copiedShape = new Shape();
            copiedShape.Copy(sourceShape);

            // Determine the master name of the source shape (required by AddShape)
            string masterName = sourceShape.MasterShape?.Name ?? string.Empty;

            // Add the copied shape to the target page; AddShape returns a new unique ID
            long newShapeId = diagram.AddShape(copiedShape, masterName, targetPageIndex);

            // Optional: bring the new shape to the front of the Z‑order on the target page
            diagram.Pages[targetPageIndex].BringToFront(newShapeId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
