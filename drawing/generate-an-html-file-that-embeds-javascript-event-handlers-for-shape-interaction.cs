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

                // Access the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2, 2) inches
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set a double‑click event that triggers JavaScript alert when exported to HTML
                // The CALLTHIS function with a "javascript:" URI is used for HTML export
                shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"javascript:alert('Shape clicked')\")";

                // Export the shape (including the event) to an HTML file
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                shape.ToHTML("ShapeWithEvent.html", htmlOptions);

                Console.WriteLine("HTML file with embedded JavaScript event has been created.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }