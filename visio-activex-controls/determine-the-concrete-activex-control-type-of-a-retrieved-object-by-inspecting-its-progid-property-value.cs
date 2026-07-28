using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Define the page index and shape ID you want to inspect
            int pageIndex = 0;          // first page (zero‑based)
            long shapeId = 1;           // replace with the actual shape ID

            // Retrieve the page from the diagram
            Page page = diagram.Pages[pageIndex];

            // Retrieve the shape by its unique ID (must be a long)
            Shape shape = page.Shapes.GetShape(shapeId);

            // Check if the shape contains an ActiveX control
            if (shape.ActiveXControl != null)
            {
                // Obtain the concrete control type via the Type property
                ControlType controlType = shape.ActiveXControl.Type;

                // Output the determined control type
                Console.WriteLine($"Shape ID {shapeId} contains an ActiveX control of type: {controlType}");
            }
            else
            {
                Console.WriteLine($"Shape ID {shapeId} does not contain an ActiveX control.");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
