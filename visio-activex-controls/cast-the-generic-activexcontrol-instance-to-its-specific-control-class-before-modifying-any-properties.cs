using System.IO;
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
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the first shape that contains an ActiveX control
            Shape targetShape = null;
            foreach (Shape shape in diagram.ActivePage.Shapes)
            {
                if (shape.ActiveXControl != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No ActiveX control found in the diagram.");
                return;
            }

            // Cast the generic ActiveXControl to its specific type based on the ControlType enum
            switch (targetShape.ActiveXControl.Type)
            {
                case ControlType.CommandButton:
                    // Cast to CommandButtonActiveXControl and modify its properties
                    CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)targetShape.ActiveXControl;
                    cmdBtn.Caption = "Clicked!";
                    break;

                case ControlType.CheckBox:
                    // Cast to CheckBoxActiveXControl and set its checked state
                    CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)targetShape.ActiveXControl;
                    chkBox.Value = CheckValueType.Checked;
                    break;

                case ControlType.TextBox:
                    // Cast to TextBoxActiveXControl and set its text content
                    TextBoxActiveXControl txtBox = (TextBoxActiveXControl)targetShape.ActiveXControl;
                    txtBox.Text = "Hello World";
                    break;

                default:
                    Console.WriteLine($"Control type '{targetShape.ActiveXControl.Type}' is not handled.");
                    break;
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
