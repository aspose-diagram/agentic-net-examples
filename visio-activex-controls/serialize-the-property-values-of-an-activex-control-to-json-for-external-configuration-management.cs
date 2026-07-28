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
        public string ControlType { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLocked { get; set; }
        public bool IsTransparent { get; set; }
        public string MousePointer { get; set; }
        public string IMEMode { get; set; }
        public string BackOleColor { get; set; }
        public string ForeOleColor { get; set; }

        // Specific properties for certain control types
        public string Caption { get; set; }          // CommandButton
        public string Text { get; set; }             // TextBox
        public string CheckValue { get; set; }       // CheckBox
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to hold serialized control data
                List<ActiveXControlDto> controlsData = new List<ActiveXControlDto>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            var control = shape.ActiveXControl;
                            var dto = new ActiveXControlDto
                            {
                                ControlType = control.Type.ToString(),
                                Width = control.Width,
                                Height = control.Height,
                                IsEnabled = control.IsEnabled,
                                IsLocked = control.IsLocked,
                                IsTransparent = control.IsTransparent,
                                MousePointer = control.MousePointer.ToString(),
                                IMEMode = control.IMEMode.ToString(),
                                BackOleColor = control.BackOleColor.ToString(),
                                ForeOleColor = control.ForeOleColor.ToString()
                            };

                            // Cast to specific control types to capture extra properties
                            if (control.Type == ControlType.CommandButton)
                            {
                                var btn = (CommandButtonActiveXControl)control;
                                dto.Caption = btn.Caption;
                            }
                            else if (control.Type == ControlType.TextBox)
                            {
                                var txt = (TextBoxActiveXControl)control;
                                dto.Text = txt.Text;
                            }
                            else if (control.Type == ControlType.CheckBox)
                            {
                                var chk = (CheckBoxActiveXControl)control;
                                // Only Checked is defined; unchecked is represented by zero
                                dto.CheckValue = chk.Value == CheckValueType.Checked ? "Checked" : "Unchecked";
                            }

                            controlsData.Add(dto);
                        }
                    }
                }

                // Serialize the list to JSON
                string json = JsonSerializer.Serialize(controlsData, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to a file
                string outputPath = "controls.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Serialized {controlsData.Count} ActiveX control(s) to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}