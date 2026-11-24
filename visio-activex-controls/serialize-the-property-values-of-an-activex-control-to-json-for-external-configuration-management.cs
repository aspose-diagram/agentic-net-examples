using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output JSON file path
        string outputPath = "activex_controls.json";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // List to hold serialized control information
            List<ActiveXControlInfo> controlsInfo = new List<ActiveXControlInfo>();

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Base information common to all controls
                        var baseInfo = new ActiveXControlInfo
                        {
                            ShapeId = shape.ID,
                            ControlType = shape.ActiveXControl.Type.ToString(),
                            Width = shape.ActiveXControl.Width,
                            Height = shape.ActiveXControl.Height
                        };

                        // Retrieve type‑specific properties
                        switch (shape.ActiveXControl.Type)
                        {
                            case ControlType.CommandButton:
                                var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                baseInfo.Caption = cmdBtn.Caption;
                                break;

                            case ControlType.TextBox:
                                var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                baseInfo.Text = txtBox.Text;
                                break;

                            case ControlType.CheckBox:
                                var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                // Checked state is represented by CheckValueType.Checked or 0 for unchecked
                                baseInfo.IsChecked = chkBox.Value == CheckValueType.Checked;
                                break;

                            case ControlType.Image:
                                var imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                // Image data is stored as a byte array; we capture its size
                                baseInfo.ImageSizeBytes = imgCtrl.Picture?.Length ?? 0;
                                break;

                            case ControlType.SpinButton:
                                var spinBtn = (SpinButtonActiveXControl)shape.ActiveXControl;
                                baseInfo.Position = spinBtn.Position;
                                break;

                            // Add other control types as needed
                            default:
                                // For unsupported types, no additional properties are captured
                                break;
                        }

                        controlsInfo.Add(baseInfo);
                    }
                }
            }

            // Serialize the collected information to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(controlsInfo, jsonOptions);
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"ActiveX control data serialized to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // DTO class representing the serializable properties of an ActiveX control
    private class ActiveXControlInfo
    {
        public long ShapeId { get; set; }
        public string ControlType { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }

        // Optional properties for specific control types
        public string? Caption { get; set; }
        public string? Text { get; set; }
        public bool? IsChecked { get; set; }
        public int? ImageSizeBytes { get; set; }
        public int? Position { get; set; }
    }
}