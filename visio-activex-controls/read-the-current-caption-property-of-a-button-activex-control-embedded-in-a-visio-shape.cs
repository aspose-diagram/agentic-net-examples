using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find CommandButton ActiveX controls
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains an ActiveX control
                        if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            // Cast to the specific CommandButton control type
                            CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Read the Caption property
                            string caption = button.Caption;

                            // Output the result
                            Console.WriteLine($"Shape ID {shape.ID} - Button Caption: {caption}");
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