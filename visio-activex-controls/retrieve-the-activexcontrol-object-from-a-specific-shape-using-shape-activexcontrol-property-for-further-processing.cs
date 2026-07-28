using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Specify the shape ID that contains the ActiveX control
                // Replace with the actual shape ID you want to process
                long shapeId = 1;

                // Retrieve the shape from the active page
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Ensure the shape has an ActiveX control attached
                if (shape.ActiveXControl == null)
                {
                    Console.WriteLine("The specified shape does not contain an ActiveX control.");
                    return;
                }

                // Determine the type of the ActiveX control and cast accordingly
                ControlType ctrlType = shape.ActiveXControl.Type;
                switch (ctrlType)
                {
                    case ControlType.CommandButton:
                        // Cast to CommandButtonActiveXControl
                        CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        Console.WriteLine("ActiveX Control Type: CommandButton");
                        Console.WriteLine($"Caption: {cmdBtn.Caption}");
                        // Example of modifying a property
                        cmdBtn.Caption = "Updated Caption";
                        break;

                    case ControlType.Image:
                        ImageActiveXControl imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                        Console.WriteLine("ActiveX Control Type: Image");
                        // Example: display the size of the image control
                        Console.WriteLine($"Width: {imgCtrl.Width}, Height: {imgCtrl.Height}");
                        break;

                    case ControlType.CheckBox:
                        CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                        Console.WriteLine("ActiveX Control Type: CheckBox");
                        // Example: display the current value
                        Console.WriteLine($"Value: {chkBox.Value}");
                        break;

                    // Add handling for other control types as needed
                    default:
                        Console.WriteLine($"ActiveX Control Type: {ctrlType} (no specific handling implemented).");
                        break;
                }

                // Further processing can be added here, such as saving the diagram after modifications
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }