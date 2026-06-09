using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioSaveExample
{
    static void Main()
    {
        try
        {

            // Original Visio file path
            string filePath = @"C:\Diagrams\MyDiagram.vsdx";

            // Load the diagram from the original file
            Diagram diagram = new Diagram(filePath);

            // -------------------------------------------------
            // Perform geometry modifications on the diagram here
            // Example: move the first shape on the active page
            // -------------------------------------------------
            if (diagram.ActivePage != null && diagram.ActivePage.Shapes.Count > 0)
            {
                Shape shape = diagram.ActivePage.Shapes[0];
                // Set new position (PinX, PinY) and size (Width, Height)
                shape.XForm.PinX.Value = 5.0;   // new X coordinate
                shape.XForm.PinY.Value = 5.0;   // new Y coordinate
                shape.XForm.Width.Value = 2.0; // new width
                shape.XForm.Height.Value = 1.0; // new height
            }

            // Save the modified diagram back to the original file,
            // preserving all changes (including geometry).
            diagram.Save(filePath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
