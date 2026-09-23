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

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains an ActiveX control of type CommandButton
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                    {
                        // Cast to the specific CommandButton control (optional, shown for completeness)
                        CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                        // Extract the HelpTopic value (the help file or topic associated with the control)
                        string helpFile = shape.Help.HelpTopic.Value;

                        Console.WriteLine($"Shape ID {shape.ID} - HelpFile: {helpFile}");
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
