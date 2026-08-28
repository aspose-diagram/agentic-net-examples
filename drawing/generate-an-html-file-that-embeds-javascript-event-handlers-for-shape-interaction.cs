using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape at position (5,5) inches
            long shapeId = page.AddShape(5.0, 5.0, "Rectangle");

            // Retrieve the shape instance using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Assign a double‑click event that triggers a JavaScript alert when the shape is clicked in the HTML view
            shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"alert('Shape double‑clicked')\")";

            // Configure HTML export options (default settings are sufficient for this example)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the diagram to an HTML file; the event formula will be embedded as JavaScript
            diagram.Save("ShapeInteraction.html", htmlOptions);

            Console.WriteLine("HTML file with JavaScript event handlers generated successfully.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
