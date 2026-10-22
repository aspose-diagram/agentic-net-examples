using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path (optional)
        string outputPath = args.Length > 1 ? args[1] : "output_audit.vsdx";

        // Optional image file for ImageActiveXControl updates
        string imagePath = args.Length > 2 ? args[2] : null;
        byte[] newImageData = null;
        if (!string.IsNullOrEmpty(imagePath))
        {
            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Image file not found: {imagePath}");
                return;
            }
            newImageData = File.ReadAllBytes(imagePath);
        }

        // Audit log file path
        string auditLogPath = "audit.log";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // Determine the control type
                    ControlType ctrlType = shape.ActiveXControl.Type;

                    // Process CommandButtonActiveXControl
                    if (ctrlType == ControlType.CommandButton)
                    {
                        var btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        string prevCaption = btn.Caption;
                        string newCaption = "Updated Caption";

                        // Apply change
                        btn.Caption = newCaption;

                        // Log the change
                        string logEntry = $"Shape ID {shape.ID}: CommandButton Caption changed from '{prevCaption}' to '{newCaption}'.";
                        Console.WriteLine(logEntry);
                        File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                    }
                    // Process CheckBoxActiveXControl
                    else if (ctrlType == ControlType.CheckBox)
                    {
                        var chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                        CheckValueType prevValue = chk.Value;

                        // Toggle the checkbox state
                        CheckValueType newValue = prevValue == CheckValueType.Checked ? (CheckValueType)0 : CheckValueType.Checked;
                        chk.Value = newValue;

                        // Log the change
                        string logEntry = $"Shape ID {shape.ID}: CheckBox Value changed from '{prevValue}' to '{newValue}'.";
                        Console.WriteLine(logEntry);
                        File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                    }
                    // Process ImageActiveXControl (if new image data is provided)
                    else if (ctrlType == ControlType.Image && newImageData != null)
                    {
                        var imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                        byte[] prevPicture = imgCtrl.Picture;

                        // Update picture data
                        imgCtrl.Picture = newImageData;

                        // Log the change (show byte lengths)
                        string logEntry = $"Shape ID {shape.ID}: Image picture data length changed from {prevPicture?.Length ?? 0} to {newImageData.Length}.";
                        Console.WriteLine(logEntry);
                        File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                    }
                    // Process TextBoxActiveXControl
                    else if (ctrlType == ControlType.TextBox)
                    {
                        var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                        string prevText = txtBox.Text;
                        string newText = "Updated Text";

                        // Apply change
                        txtBox.Text = newText;

                        // Log the change
                        string logEntry = $"Shape ID {shape.ID}: TextBox Text changed from '{prevText}' to '{newText}'.";
                        Console.WriteLine(logEntry);
                        File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                    }
                    // Additional control types can be handled here as needed
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}