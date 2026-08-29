using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToSwfConverter
{
    static void Main()
    {
        try
        {
            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram("input.vsd");

            // Retrieve the shape you want to convert (example: first shape on the first page)
            Shape sourceShape = sourceDiagram.Pages[0].Shapes[0];

            // Create a new diagram that will contain only the selected shape
            Diagram singleShapeDiagram = new Diagram();

            // Add a new blank page to the new diagram
            Page newPage = new Page();
            singleShapeDiagram.Pages.Add(newPage);

            // Determine the master name of the source shape (required for AddShape)
            string masterName = sourceShape.Master?.Name ?? "Rectangle";

            // Add the shape to the new page using the overload that accepts a Shape instance
            newPage.AddShape(sourceShape, masterName);

            // Configure SWF save options (default settings are sufficient for basic conversion)
            SWFSaveOptions swfOptions = new SWFSaveOptions();

            // Save the diagram (which now contains only the selected shape) as SWF
            singleShapeDiagram.Save("shape.swf", swfOptions);
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the conversion process
            Console.WriteLine($"Error converting shape to SWF: {ex.Message}");
        }
    }
}
