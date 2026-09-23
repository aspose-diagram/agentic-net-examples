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

                // Ensure there is at least one page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate
                long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set a double‑click event that triggers a JavaScript alert when the HTML is viewed
                // The CALLTHIS function can invoke a JavaScript URL in the exported HTML
                shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"javascript:alert('Shape double‑clicked')\")";

                // Export the diagram to an HTML file
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("Diagram exported to output.html with JavaScript event handler.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }