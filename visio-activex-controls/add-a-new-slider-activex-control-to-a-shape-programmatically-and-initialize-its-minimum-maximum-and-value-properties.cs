using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a SpinButton ActiveX control (used as a numeric range control)
            // Parameters: control type, PinX, PinY, width, height (all in inches)
            long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific SpinButtonActiveXControl
            SpinButtonActiveXControl spinButton = (SpinButtonActiveXControl)controlShape.ActiveXControl;

            // Initialize the control's range and current value
            spinButton.Min = 0;          // Minimum value
            spinButton.Max = 100;        // Maximum value
            spinButton.Position = 50;    // Current value (Position property)

            // Save the diagram to a VSDX file
            diagram.Save("SliderControlDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}