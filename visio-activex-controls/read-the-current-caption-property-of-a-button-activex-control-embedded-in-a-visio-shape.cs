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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to find the button ActiveX control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains an ActiveX control and that it is a CommandButton
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                    {
                        // Cast to the specific CommandButtonActiveXControl type
                        CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                        // Read the Caption property
                        string caption = button.Caption;

                        // Output the caption value
                        Console.WriteLine($"Button Caption: {caption}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
