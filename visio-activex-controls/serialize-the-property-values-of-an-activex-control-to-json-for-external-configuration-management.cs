using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public class ActiveXControlDto
{
    public long ShapeId { get; set; }
    public string ControlType { get; set; }
    public string Caption { get; set; }
    public string Text { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string Value { get; set; }
    public string CheckValue { get; set; }
    public int Position { get; set; }
    public string MousePointer { get; set; }
    public string IMEMode { get; set; }
}

public class Program
{
    public static void Main()
    {
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string outputJson = "activex_properties.json";

        try
        {
            Diagram diagram = new Diagram(diagramPath);
            List<ActiveXControlDto> controls = new List<ActiveXControlDto>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        var dto = new ActiveXControlDto
                        {
                            ShapeId = shape.ID,
                            ControlType = shape.ActiveXControl.Type.ToString(),
                            MousePointer = shape.ActiveXControl.MousePointer.ToString(),
                            IMEMode = shape.ActiveXControl.IMEMode.ToString()
                        };

                        switch (shape.ActiveXControl.Type)
                        {
                            case ControlType.CommandButton:
                                var btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                dto.Caption = btn.Caption;
                                dto.Width = btn.Width;
                                dto.Height = btn.Height;
                                break;

                            case ControlType.Image:
                                var img = (ImageActiveXControl)shape.ActiveXControl;
                                dto.Width = img.Width;
                                dto.Height = img.Height;
                                break;

                            case ControlType.CheckBox:
                                var chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                                dto.Caption = chk.Caption;
                                dto.CheckValue = chk.Value.ToString();
                                break;

                            case ControlType.TextBox:
                                var txt = (TextBoxActiveXControl)shape.ActiveXControl;
                                dto.Text = txt.Text;
                                break;

                            case ControlType.SpinButton:
                                var spin = (SpinButtonActiveXControl)shape.ActiveXControl;
                                dto.Position = spin.Position;
                                break;

                            default:
                                // No additional properties for unhandled types
                                break;
                        }

                        controls.Add(dto);
                    }
                }
            }

            string json = JsonSerializer.Serialize(controls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJson, json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}