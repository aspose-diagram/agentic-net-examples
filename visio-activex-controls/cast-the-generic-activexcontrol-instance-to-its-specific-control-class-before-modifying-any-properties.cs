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

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape actually contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Identify the specific control type via the Type property
                        ControlType ctrlType = shape.ActiveXControl.Type;

                        // Cast to the concrete control class before accessing its members
                        switch (ctrlType)
                        {
                            case ControlType.SpinButton:
                                // Cast to SpinButtonActiveXControl
                                SpinButtonActiveXControl spinCtrl = (SpinButtonActiveXControl)shape.ActiveXControl;
                                // Example modification: set width (in inches)
                                spinCtrl.Width = 2.0;
                                break;

                            case ControlType.CheckBox:
                                // Cast to CheckBoxActiveXControl
                                CheckBoxActiveXControl checkCtrl = (CheckBoxActiveXControl)shape.ActiveXControl;
                                // Example modification: disable the checkbox
                                checkCtrl.IsEnabled = false;
                                break;

                            case ControlType.TextBox:
                                // Cast to TextBoxActiveXControl
                                TextBoxActiveXControl textCtrl = (TextBoxActiveXControl)shape.ActiveXControl;
                                // Example modification: change background color (OLE color)
                                textCtrl.BackOleColor = 0x00FF00; // Green
                                break;

                            default:
                                // For any other or unknown controls, attempt to cast to UnknownControl
                                UnknownControl unknownCtrl = shape.ActiveXControl as UnknownControl;
                                if (unknownCtrl != null)
                                {
                                    // Example modification: lock the control
                                    unknownCtrl.IsLocked = true;
                                }
                                break;
                        }
                    }
                }
            }

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
