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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to locate ActiveX controls.
            foreach (Page page in diagram.Pages)
            {
                // Shapes collection is zero‑based; iterate by index.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes without an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Retrieve the generic ActiveXControl instance.
                    ActiveXControl genericCtrl = shape.ActiveXControl;

                    // Determine the specific control type and cast accordingly.
                    switch (genericCtrl.Type)
                    {
                        case ControlType.CommandButton:
                            // Cast to CommandButtonActiveXControl to access button‑specific members.
                            CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)genericCtrl;
                            // Modify properties: set caption.
                            cmdBtn.Caption = "Click Me";
                            // Note: IsEnabled property type may be bool; omit if not applicable.
                            break;

                        case ControlType.CheckBox:
                            // Cast to CheckBoxActiveXControl.
                            CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)genericCtrl;
                            // Set the checkbox to a checked state.
                            chkBox.Value = CheckValueType.Checked;
                            break;

                        case ControlType.SpinButton:
                            // Cast to SpinButtonActiveXControl.
                            SpinButtonActiveXControl spinBtn = (SpinButtonActiveXControl)genericCtrl;
                            // Set the numeric position.
                            spinBtn.Position = 5;
                            break;

                        case ControlType.Image:
                            // Cast to ImageActiveXControl.
                            ImageActiveXControl imgCtrl = (ImageActiveXControl)genericCtrl;
                            // Assign an image from disk (ensure the file exists).
                            string imagePath = "sample.png";
                            if (File.Exists(imagePath))
                            {
                                imgCtrl.Picture = File.ReadAllBytes(imagePath);
                            }
                            break;

                        // Add handling for other control types as needed.
                        default:
                            // Log unhandled control types.
                            Console.WriteLine($"Unhandled ActiveX control type: {genericCtrl.Type}");
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
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}