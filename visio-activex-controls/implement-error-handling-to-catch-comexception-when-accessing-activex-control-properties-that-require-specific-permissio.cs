using System.IO;
using System;
using System.Runtime.InteropServices;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the active page
            Page page = diagram.ActivePage;

            // Add a CommandButton ActiveX control to the page
            long shapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that contains the ActiveX control
            Shape shape = page.Shapes.GetShape(shapeId);

            // Cast the generic ActiveXControl to the specific CommandButton type
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

            // Attempt to set a property that may require special permissions
            try
            {
                button.Caption = "Click Me";
                Console.WriteLine("Caption set successfully.");
            }
            catch (COMException comEx)
            {
                // Handle permission-related COM exceptions
                Console.WriteLine($"COMException caught: {comEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
