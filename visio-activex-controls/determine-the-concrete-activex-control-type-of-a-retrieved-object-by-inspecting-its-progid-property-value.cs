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
            // Replace "input.vsdx" with the path to your diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we are interested in the first page
            Page page = diagram.Pages[0];

            // Example shape ID – replace with the actual ID of the shape that contains the ActiveX control
            long shapeId = 1; // long is required for shape IDs

            // Retrieve the shape by its unique Visio ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape actually hosts an ActiveX control
            if (shape.ActiveXControl == null)
            {
                Console.WriteLine("The specified shape does not contain an ActiveX control.");
                return;
            }

            // Determine the concrete ActiveX control type using the Type property (ControlType enum)
            ControlType controlType = shape.ActiveXControl.Type;

            // Output the identified control type
            Console.WriteLine($"ActiveX control type: {controlType}");

            // Optional: based on the control type, you can cast to the specific control class
            switch (controlType)
            {
                case ControlType.CommandButton:
                    CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                    Console.WriteLine($"Caption: {btn.Caption}");
                    break;

                case ControlType.CheckBox:
                    CheckBoxActiveXControl chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                    Console.WriteLine($"Checked state: {chk.Value}");
                    break;

                case ControlType.TextBox:
                    TextBoxActiveXControl txt = (TextBoxActiveXControl)shape.ActiveXControl;
                    Console.WriteLine($"Text: {txt.Text}");
                    break;

                // Add additional cases for other control types as needed
                default:
                    Console.WriteLine("Control type is not specifically handled in this example.");
                    break;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
