using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a SpinButton ActiveX control (used as a slider) to the page
            // Parameters: ControlType, PinX, PinY, Width, Height (all in inches)
            long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific SpinButtonActiveXControl
            SpinButtonActiveXControl spinControl = (SpinButtonActiveXControl)controlShape.ActiveXControl;

            // Initialize the control's range and current value
            spinControl.Min = 0;          // Minimum value
            spinControl.Max = 100;        // Maximum value
            spinControl.Position = 50;    // Current value (position)

            // Save the diagram to a VSDX file
            diagram.Save("SliderControlDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}