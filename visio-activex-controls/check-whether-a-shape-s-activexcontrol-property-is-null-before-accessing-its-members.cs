using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // Adjust the file path as needed
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an ActiveXControl before accessing its members
                        if (shape.ActiveXControl != null)
                        {
                            // Determine the specific control type
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            // Example handling for a CommandButton ActiveX control
                            if (ctrlType == ControlType.CommandButton)
                            {
                                CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;
                                Console.WriteLine($"Shape ID {shape.ID} has a CommandButton with caption: {button.Caption}");
                            }
                            // Example handling for a CheckBox ActiveX control
                            else if (ctrlType == ControlType.CheckBox)
                            {
                                CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                Console.WriteLine($"Shape ID {shape.ID} has a CheckBox with value: {checkBox.Value}");
                            }
                            // Add handling for other control types as needed
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type: {ctrlType}");
                            }
                        }
                    }
                }

                // Optionally save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }