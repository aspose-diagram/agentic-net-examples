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
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains an ActiveX control
                        if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            // Cast to the specific CommandButton control
                            CommandButtonActiveXControl cmdButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Retrieve the Help topic (HelpFile equivalent) from the shape's Help element
                            string helpTopic = shape.Help?.HelpTopic?.Value;

                            // Output the information
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"CommandButton Caption: {cmdButton.Caption}");
                            Console.WriteLine($"Help Topic: {(string.IsNullOrEmpty(helpTopic) ? "None" : helpTopic)}");
                            Console.WriteLine(new string('-', 40));
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