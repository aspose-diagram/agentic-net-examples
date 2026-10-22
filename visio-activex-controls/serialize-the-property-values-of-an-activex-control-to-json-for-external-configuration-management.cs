using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    // DTO representing serialized ActiveX control properties
    public class ActiveXControlDto
    {
        public string ControlType { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> AdditionalProperties { get; set; } = new Dictionary<string, object>();
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output JSON path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputJsonPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonOutputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // List to hold serialized control data
            List<ActiveXControlDto> controlsData = new List<ActiveXControlDto>();

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape hosts an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        var control = shape.ActiveXControl;

                        var dto = new ActiveXControlDto
                        {
                            ControlType = control.Type.ToString(),
                            Width = control.Width,
                            Height = control.Height,
                            // Directly assign the native bool value (removed invalid BOOL comparison)
                            IsEnabled = control.IsEnabled
                        };

                        // Capture Caption where the property exists
                        if (control is CommandButtonActiveXControl btn)
                        {
                            dto.Caption = btn.Caption;
                        }
                        else if (control is CheckBoxActiveXControl chk)
                        {
                            dto.Caption = chk.Caption;
                        }
                        else if (control is LabelActiveXControl lbl)
                        {
                            dto.Caption = lbl.Caption;
                        }
                        // ImageActiveXControl does not have a Caption property – removed invalid access

                        // Capture type‑specific additional properties
                        var additional = dto.AdditionalProperties;

                        // CheckBox specific
                        if (control is CheckBoxActiveXControl checkBox)
                        {
                            // Store enum value as integer
                            additional["Value"] = (int)checkBox.Value;
                        }

                        // TextBox specific
                        if (control is TextBoxActiveXControl textBox)
                        {
                            additional["Text"] = textBox.Text;
                        }

                        // Image specific
                        if (control is ImageActiveXControl imageCtrl)
                        {
                            additional["PictureLength"] = imageCtrl.Picture?.Length ?? 0;
                        }

                        // Add the DTO to the collection
                        controlsData.Add(dto);
                    }
                }
            }

            // Serialize the collection to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(controlsData, jsonOptions);

            // Write JSON to the specified output file
            File.WriteAllText(jsonOutputPath, json);
            Console.WriteLine($"ActiveX control properties have been serialized to: {jsonOutputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
