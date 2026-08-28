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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Locate a shape that contains an ActiveX control
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape with an ActiveX control was found in the diagram.");
                return;
            }

            // Retrieve the ActiveX control instance
            var activeX = targetShape.ActiveXControl;

            // Process the control based on its specific type
            switch (activeX.Type)
            {
                case ControlType.CommandButton:
                    var commandBtn = (CommandButtonActiveXControl)activeX;
                    Console.WriteLine($"CommandButton Caption: {commandBtn.Caption}");
                    break;

                case ControlType.Image:
                    var imageCtrl = (ImageActiveXControl)activeX;
                    Console.WriteLine($"Image Control Size: Width={imageCtrl.Width}, Height={imageCtrl.Height}");
                    break;

                case ControlType.CheckBox:
                    var checkBox = (CheckBoxActiveXControl)activeX;
                    Console.WriteLine($"CheckBox Value: {checkBox.Value}");
                    break;

                default:
                    Console.WriteLine($"ActiveX control type: {activeX.Type}");
                    break;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
