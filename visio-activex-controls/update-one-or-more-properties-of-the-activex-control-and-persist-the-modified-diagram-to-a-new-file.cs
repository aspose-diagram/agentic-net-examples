using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not supplied)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Process only shapes that contain an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Identify the type of ActiveX control
                            ControlType ctrlType = shape.ActiveXControl.Type;

                            if (ctrlType == ControlType.TextBox)
                            {
                                // Update the text of a TextBox control
                                TextBoxActiveXControl txtCtrl = (TextBoxActiveXControl)shape.ActiveXControl;
                                txtCtrl.Text = "Updated text via Aspose.Diagram";
                            }
                            else if (ctrlType == ControlType.CommandButton)
                            {
                                // Update the caption of a CommandButton control
                                CommandButtonActiveXControl btnCtrl = (CommandButtonActiveXControl)shape.ActiveXControl;
                                btnCtrl.Caption = "New Caption";
                            }
                            else if (ctrlType == ControlType.CheckBox)
                            {
                                // Set the CheckBox to the checked state
                                CheckBoxActiveXControl chkCtrl = (CheckBoxActiveXControl)shape.ActiveXControl;
                                chkCtrl.Value = CheckValueType.Checked;
                            }
                            // Additional ActiveX control types can be handled here as needed
                        }
                    }
                }

                // Save the modified diagram to the specified output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}