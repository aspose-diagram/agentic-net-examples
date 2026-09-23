using System;
using System.IO;
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
            // Parameters: ControlType, PinX, PinY, Width, Height
            long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific SpinButton type
            SpinButtonActiveXControl spinButton = (SpinButtonActiveXControl)controlShape.ActiveXControl;

            // Initialize the control's range and current value
            spinButton.Min = 0;        // Minimum value (property name is Min)
            spinButton.Max = 100;      // Maximum value (property name is Max)
            spinButton.Position = 50;  // Current value

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