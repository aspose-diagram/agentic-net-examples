using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Only process shapes that contain an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Determine the specific control type
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            // Example handling for a CommandButton control
                            if (ctrlType == ControlType.CommandButton)
                            {
                                // Cast to the concrete control class
                                CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                                // Log and change the Caption property
                                LogChange("Caption", button.Caption, "Submit");
                                button.Caption = "Submit";

                                // Log and change the Width property (value in points)
                                LogChange("Width", button.Width.ToString(), "120");
                                button.Width = 120;

                                // Log and change the Height property (value in points)
                                LogChange("Height", button.Height.ToString(), "30");
                                button.Height = 30;
                            }
                            // Example handling for a TextBox control
                            else if (ctrlType == ControlType.TextBox)
                            {
                                // Cast to the concrete control class
                                TextBoxActiveXControl textBox = (TextBoxActiveXControl)shape.ActiveXControl;

                                // Log and change the Text property
                                LogChange("Text", textBox.Text, "Hello World");
                                textBox.Text = "Hello World";
                            }
                            // Add handling for other control types as needed
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to log property changes
        static void LogChange(string propertyName, string oldValue, string newValue)
        {
            Console.WriteLine($"Property '{propertyName}' changed from '{oldValue}' to '{newValue}'.");
        }
    }