using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the active page (a new diagram contains a default page)
            Page page = diagram.ActivePage;

            // Add a CommandButton ActiveX control to the page
            // Parameters: control type, PinX, PinY, width, height (all in inches)
            long shapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that represents the ActiveX control
            Shape shape = page.Shapes.GetShape(shapeId);

            // Cast the generic ActiveXControl to the specific CommandButtonActiveXControl
            CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;

            // Modify properties of the specific control
            commandButton.Caption = "Click Me";
            commandButton.Width = 120;   // width in points
            commandButton.Height = 30;   // height in points

            // Save the diagram to a VSDX file
            diagram.Save("ActiveXDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
