using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Retrieve the concrete control type via the Type property
                            ControlType controlType = shape.ActiveXControl.Type;

                            // Output the control type
                            Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type: {controlType}");

                            // Example of handling specific control types
                            switch (controlType)
                            {
                                case ControlType.CommandButton:
                                    // Cast to the specific control class if needed
                                    CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;
                                    Console.WriteLine($"  Caption: {button.Caption}");
                                    break;
                                case ControlType.CheckBox:
                                    CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                    Console.WriteLine($"  Value: {checkBox.Value}");
                                    break;
                                case ControlType.TextBox:
                                    TextBoxActiveXControl textBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                    Console.WriteLine($"  Text: {textBox.Text}");
                                    break;
                                // Add additional cases as required
                                default:
                                    Console.WriteLine("  No additional handling for this control type.");
                                    break;
                            }
                        }
                    }
                }

                // Dispose the diagram when done
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }