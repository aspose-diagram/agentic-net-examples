using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we are working with the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape by its Visio ID (example ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape contains an ActiveX control
            if (shape.ActiveXControl != null)
            {
                // Determine the specific control type and cast accordingly
                switch (shape.ActiveXControl.Type)
                {
                    case ControlType.CommandButton:
                        // Cast to CommandButtonActiveXControl
                        CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        // Modify properties specific to command button
                        cmdBtn.Caption = "Submit";
                        // Width and Height are in inches; adjust as needed
                        cmdBtn.Width = 2.0;
                        cmdBtn.Height = 0.5;
                        break;

                    case ControlType.Image:
                        // Cast to ImageActiveXControl
                        ImageActiveXControl imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                        // Load image bytes and assign to the control
                        byte[] imageBytes = File.ReadAllBytes("logo.png");
                        imgCtrl.Picture = imageBytes;
                        // Optionally adjust size
                        imgCtrl.Width = 1.5;
                        imgCtrl.Height = 1.0;
                        break;

                    case ControlType.CheckBox:
                        // Cast to CheckBoxActiveXControl
                        CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                        // Set the checkbox state to Checked
                        chkBox.Value = CheckValueType.Checked;
                        // Unchecked state can be represented by casting 0
                        // chkBox.Value = (CheckValueType)0;
                        break;

                    // Add handling for other control types as needed
                    default:
                        Console.WriteLine($"Control type {shape.ActiveXControl.Type} is not handled.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("The selected shape does not contain an ActiveX control.");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
