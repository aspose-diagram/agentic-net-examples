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

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Insert a SpinButton ActiveX control (used as a slider) onto the page
            // Parameters: ControlType, PinX, PinY, Width, Height (in inches)
            long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that hosts the ActiveX control
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific SpinButtonActiveXControl
            SpinButtonActiveXControl spinControl = (SpinButtonActiveXControl)controlShape.ActiveXControl;

            // Define the allowed range for the control
            spinControl.Min = 0;      // Minimum value
            spinControl.Max = 100;    // Maximum value

            // Set a test value within the defined range to satisfy validation
            spinControl.Position = 50;

            // Validate that the Position is within the defined Minimum and Maximum
            if (spinControl.Position < spinControl.Min || spinControl.Position > spinControl.Max)
            {
                throw new Exception($"SpinButton value {spinControl.Position} is outside the allowed range [{spinControl.Min}, {spinControl.Max}].");
            }
            else
            {
                Console.WriteLine($"SpinButton value {spinControl.Position} is within the allowed range.");
            }

            // Save the diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}