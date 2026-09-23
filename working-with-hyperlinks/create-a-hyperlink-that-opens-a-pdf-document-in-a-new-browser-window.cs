using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a page to the diagram (the first page is created by default)
            Page page = diagram.Pages[0];

            // Draw a simple rectangle shape that will hold the hyperlink
            // Parameters: pinX, pinY, width, height (all in inches)
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;
            long shapeId = page.DrawRectangle(pinX, pinY, width, height);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Add a hyperlink to the shape
            Hyperlink link = new Hyperlink();
            // URL of the PDF document to open
            link.Address.Value = "https://example.com/sample.pdf";
            // Optional description (tooltip)
            link.Description.Value = "Open PDF in a new browser window";
            // If the Hyperlink class supports opening in a new window, set it.
            // The Visio cell is named "NewWindow". Use BOOL.True if available.
            // Uncomment the following line if the property exists in your version:
            // link.NewWindow.Value = BOOL.True;

            // Attach the hyperlink to the shape
            shape.Hyperlinks.Add(link);

            // Save the diagram to a VSDX file
            string outputPath = "HyperlinkDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }