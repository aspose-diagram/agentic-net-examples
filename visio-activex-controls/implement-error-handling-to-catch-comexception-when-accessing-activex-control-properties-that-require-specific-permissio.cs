using System;
using System.Runtime.InteropServices;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Get the active page where the control will be placed
                Page page = diagram.ActivePage;

                // Add a CommandButton ActiveX control to the page
                // Parameters: control type, PinX, PinY, width, height (in inches)
                long shapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

                // Retrieve the shape that represents the ActiveX control
                Shape controlShape = page.Shapes.GetShape(shapeId);

                // Cast the generic ActiveXControl to the specific CommandButton type
                CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)controlShape.ActiveXControl;

                // Attempt to set a property that may require elevated permissions
                try
                {
                    // Setting the Caption property; this can throw COMException if permissions are insufficient
                    commandButton.Caption = "Click Me";
                    Console.WriteLine("Caption set successfully.");
                }
                catch (COMException comEx)
                {
                    // Handle the COMException gracefully
                    Console.WriteLine($"COMException caught while setting Caption: {comEx.Message}");
                    // Additional handling logic can be placed here (e.g., logging, fallback values)
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected exceptions
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                // Save the diagram to a VSDX file
                diagram.Save("ActiveXControlDiagram.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved as ActiveXControlDiagram.vsdx");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }