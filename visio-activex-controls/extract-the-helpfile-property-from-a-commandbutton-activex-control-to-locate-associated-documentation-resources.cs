using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            // Cast to the specific CommandButton control
                            CommandButtonActiveXControl cmdButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Retrieve the Help topic (HelpFile) from the shape's Help element
                            // HelpTopic is a Str2Value; use .Value to get the string
                            string helpFile = shape.Help?.HelpTopic?.Value ?? string.Empty;

                            // Output the information
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"CommandButton Caption: {cmdButton.Caption}");
                            Console.WriteLine($"Help File: {helpFile}");
                            Console.WriteLine(new string('-', 40));
                        }
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
