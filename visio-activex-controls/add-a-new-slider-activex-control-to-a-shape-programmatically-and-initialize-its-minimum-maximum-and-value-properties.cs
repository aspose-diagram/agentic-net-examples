using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram inside a try/catch to capture any Aspose errors
        try
        {
            Diagram diagram = new Diagram();

            // Get the first page (index 0) of the diagram
            Page page = diagram.Pages[0];

            // Add a SpinButton ActiveX control (used as a slider) to the page
            // Parameters: ControlType, PinX, PinY, Width, Height (all in inches)
            long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific SpinButton type
            SpinButtonActiveXControl spinButton = (SpinButtonActiveXControl)controlShape.ActiveXControl;

            // Initialize the control's range and current position using the correct property names
            spinButton.Min = 0;        // Minimum value
            spinButton.Max = 100;      // Maximum value
            spinButton.Position = 50;  // Current value (position)

            // Save the diagram to a VSDX file
            diagram.Save("SliderDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}