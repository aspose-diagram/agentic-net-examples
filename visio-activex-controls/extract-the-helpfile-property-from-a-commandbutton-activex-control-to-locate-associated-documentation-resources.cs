using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains an ActiveX control
                        if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            // Cast to the specific CommandButton control
                            CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Retrieve the HelpTopic value (HelpFile equivalent)
                            string helpTopic = null;
                            if (shape.Help != null && shape.Help.HelpTopic != null)
                            {
                                helpTopic = shape.Help.HelpTopic.Value;
                            }

                            // Output the extracted information
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"Caption: {commandButton.Caption}");
                            if (!string.IsNullOrEmpty(helpTopic))
                            {
                                Console.WriteLine($"HelpTopic (HelpFile): {helpTopic}");
                            }
                            else
                            {
                                Console.WriteLine("HelpTopic (HelpFile) not set.");
                            }
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