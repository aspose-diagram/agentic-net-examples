using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace ActiveXControlSerialization
{
    // DTO for JSON representation of an ActiveX control
    public class ActiveXControlDto
    {
        public long ShapeId { get; set; }
        public string ControlType { get; set; } = string.Empty;
        public string? Caption { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string? Text { get; set; }
        public string? Value { get; set; }
        public int? Position { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // List to hold serialized control data
                List<ActiveXControlDto> controls = new List<ActiveXControlDto>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl == null)
                            continue;

                        // Prepare DTO
                        ActiveXControlDto dto = new ActiveXControlDto
                        {
                            ShapeId = shape.ID,
                            ControlType = shape.ActiveXControl.Type.ToString()
                        };

                        // Cast based on control type and extract properties
                        switch (shape.ActiveXControl.Type)
                        {
                            case ControlType.CommandButton:
                                var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                dto.Caption = cmdBtn.Caption;
                                dto.Width = cmdBtn.Width;
                                dto.Height = cmdBtn.Height;
                                break;

                            case ControlType.Image:
                                var imgCtrl = (ImageActiveXControl)shape.ActiveXControl;
                                // Convert image bytes to Base64 string for JSON
                                if (imgCtrl.Picture != null && imgCtrl.Picture.Length > 0)
                                    dto.ImageBase64 = Convert.ToBase64String(imgCtrl.Picture);
                                break;

                            case ControlType.CheckBox:
                                var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                // Represent checked state as string
                                dto.Value = chkBox.Value == CheckValueType.Checked ? "Checked" : "Unchecked";
                                break;

                            case ControlType.TextBox:
                                var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                dto.Text = txtBox.Text;
                                break;

                            case ControlType.SpinButton:
                                var spinBtn = (SpinButtonActiveXControl)shape.ActiveXControl;
                                dto.Position = spinBtn.Position;
                                break;

                            // Add handling for other control types as needed
                            default:
                                // For unhandled types, store generic information
                                break;
                        }

                        controls.Add(dto);
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(controls, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText("controls.json", json);

                Console.WriteLine("ActiveX control properties have been serialized to controls.json");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}