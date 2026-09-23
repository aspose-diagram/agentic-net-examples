using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx"; // replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Identify the shape that contains the ActiveX control
                // Replace with the actual shape ID you want to process
                long shapeId = 5;

                // Retrieve the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Ensure the shape has an ActiveX control attached
                if (shape.ActiveXControl == null)
                {
                    Console.WriteLine("The selected shape does not contain an ActiveX control.");
                    return;
                }

                // Determine the control type and cast accordingly
                ControlType ctrlType = shape.ActiveXControl.Type;
                switch (ctrlType)
                {
                    case ControlType.CommandButton:
                        var commandBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        Console.WriteLine($"CommandButton Caption: {commandBtn.Caption}");
                        break;

                    case ControlType.Image:
                        var imageCtrl = (ImageActiveXControl)shape.ActiveXControl;
                        int byteCount = imageCtrl.Picture?.Length ?? 0;
                        Console.WriteLine($"Image control contains {byteCount} bytes of image data.");
                        break;

                    case ControlType.CheckBox:
                        var checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                        Console.WriteLine($"CheckBox Value: {checkBox.Value}");
                        break;

                    default:
                        Console.WriteLine($"ActiveX control type '{ctrlType}' is not specifically handled.");
                        break;
                }

                // Additional processing of the retrieved ActiveX control can be added here

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }