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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [imagePathForImageControl]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string imagePath = args.Length >= 3 ? args[2] : null;

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing image file when an ImageActiveXControl will be used.
        if (imagePath != null && !File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find ActiveX controls.
            foreach (Page page in diagram.Pages)
            {
                // Use zero‑based index to access shapes collection.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes without an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Retrieve the generic ActiveXControl.
                    ActiveXControl genericCtrl = shape.ActiveXControl;

                    // Determine the specific control type and cast accordingly.
                    switch (genericCtrl.Type)
                    {
                        case ControlType.CommandButton:
                            // Cast to CommandButtonActiveXControl to access its properties.
                            CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)genericCtrl;
                            // Modify the caption of the command button.
                            cmdBtn.Caption = "Clicked!";
                            // Note: IsEnabled property is a bool; omitted per API constraints.
                            break;

                        case ControlType.CheckBox:
                            // Cast to CheckBoxActiveXControl.
                            CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)genericCtrl;
                            // Set the checkbox state to Checked.
                            chkBox.Value = CheckValueType.Checked;
                            // Note: IsEnabled property is a bool; omitted per API constraints.
                            break;

                        case ControlType.Image:
                            // Cast to ImageActiveXControl.
                            ImageActiveXControl imgCtrl = (ImageActiveXControl)genericCtrl;
                            if (imagePath != null)
                            {
                                // Load image bytes and assign to the Picture property.
                                imgCtrl.Picture = File.ReadAllBytes(imagePath);
                            }
                            // Note: IsEnabled property is a bool; omitted per API constraints.
                            break;

                        case ControlType.SpinButton:
                            // Cast to SpinButtonActiveXControl.
                            SpinButtonActiveXControl spinCtrl = (SpinButtonActiveXControl)genericCtrl;
                            // Set the numeric position (value) of the spin button.
                            spinCtrl.Position = 42;
                            // Note: IsEnabled property is a bool; omitted per API constraints.
                            break;

                        // Add handling for other control types as needed.
                        default:
                            // For unhandled types, no specific action required.
                            break;
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}