using System;
using System.Runtime.InteropServices;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path to the output Visio diagram
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            try
                            {
                                // Determine the type of the ActiveX control
                                ControlType ctrlType = shape.ActiveXControl.Type;

                                // Example handling for a CommandButton control
                                if (ctrlType == ControlType.CommandButton)
                                {
                                    CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;
                                    // Access a property that may require specific permissions
                                    string caption = button.Caption;
                                    Console.WriteLine($"CommandButton Caption: {caption}");
                                }
                                // Example handling for a CheckBox control
                                else if (ctrlType == ControlType.CheckBox)
                                {
                                    CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                    // Access the Value property (only Checked is valid)
                                    CheckValueType value = checkBox.Value;
                                    Console.WriteLine($"CheckBox Value: {value}");
                                }
                                // Add handling for other control types as needed
                            }
                            catch (COMException comEx)
                            {
                                // Handle permission-related COM exceptions
                                Console.WriteLine($"COMException accessing ActiveX control on shape ID {shape.ID}: {comEx.Message}");
                                // Optionally, log the error or take alternative actions
                            }
                            catch (Exception ex)
                            {
                                // Handle any other unexpected exceptions
                                Console.WriteLine($"Unexpected error on shape ID {shape.ID}: {ex.Message}");
                            }
                        }
                    }
                }

                // Save the diagram after processing
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }