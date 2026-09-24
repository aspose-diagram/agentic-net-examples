using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define position (PinX, PinY) and size (Width, Height) for the text shape
            double pinX = 2.0;   // X coordinate in inches
            double pinY = 3.0;   // Y coordinate in inches
            double width = 2.0;  // Width of the text box in inches
            double height = 1.0; // Height of the text box in inches

            // Add a standalone text shape with the specified content
            Shape textShape = page.AddText(pinX, pinY, width, height, "Hello Aspose!");

            // Save the diagram to a VSDX file
            diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }