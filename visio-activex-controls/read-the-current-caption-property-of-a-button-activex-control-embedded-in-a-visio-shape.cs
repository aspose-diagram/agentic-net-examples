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

            // Path to the Visio file containing the ActiveX button
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find the button control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control and if it is a CommandButton
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                    {
                        // Cast to the specific button control type
                        CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                        // Output the current Caption property
                        Console.WriteLine($"Shape ID {shape.ID} - Button Caption: {button.Caption}");
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
