using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control.
                        if (shape.ActiveXControl != null)
                        {
                            // Determine the control type.
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            // Handle CommandButtonActiveXControl.
                            if (ctrlType == ControlType.CommandButton)
                            {
                                CommandButtonActiveXControl cmdBtn = shape.ActiveXControl as CommandButtonActiveXControl;
                                if (cmdBtn != null)
                                {
                                    // Update the caption.
                                    cmdBtn.Caption = "Updated Caption";

                                    // Enable the control (property expects a plain bool).
                                    cmdBtn.IsEnabled = true;
                                }
                            }
                            // Handle TextBoxActiveXControl.
                            else if (ctrlType == ControlType.TextBox)
                            {
                                TextBoxActiveXControl txtBox = shape.ActiveXControl as TextBoxActiveXControl;
                                if (txtBox != null)
                                {
                                    // Set the displayed text.
                                    txtBox.Text = "New text content";

                                    // Enable the control.
                                    txtBox.IsEnabled = true;
                                }
                            }
                            // Handle CheckBoxActiveXControl.
                            else if (ctrlType == ControlType.CheckBox)
                            {
                                CheckBoxActiveXControl chkBox = shape.ActiveXControl as CheckBoxActiveXControl;
                                if (chkBox != null)
                                {
                                    // Enable the checkbox.
                                    chkBox.IsEnabled = true;

                                    // Set the caption.
                                    chkBox.Caption = "Accept Terms";
                                }
                            }
                            // Additional control types can be handled similarly.
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}