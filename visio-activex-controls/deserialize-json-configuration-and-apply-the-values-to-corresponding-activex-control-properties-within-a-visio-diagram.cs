using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

namespace VisioActiveXConfigurator
{
    public class Config
    {
        public List<ControlConfig> Controls { get; set; } = new();
    }

    public class ControlConfig
    {
        public string Type { get; set; } = string.Empty;
        public string? Caption { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string? Value { get; set; }
        public string? Text { get; set; }
        public int? Position { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: VisioActiveXConfigurator <diagramPath> <jsonConfigPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            string jsonPath = args[1];
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"File not found: {jsonPath}");
                return;
            }

            string outputPath = args[2];

            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            string jsonContent;
            try
            {
                jsonContent = File.ReadAllText(jsonPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading JSON file: {ex.Message}");
                return;
            }

            Config? config = JsonSerializer.Deserialize<Config>(jsonContent);
            if (config == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON configuration.");
                return;
            }

            foreach (ControlConfig ctrlCfg in config.Controls)
            {
                if (!Enum.TryParse<ControlType>(ctrlCfg.Type, out ControlType targetType))
                {
                    Console.Error.WriteLine($"Unrecognized control type: {ctrlCfg.Type}");
                    continue;
                }

                bool applied = false;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ActiveXControl == null)
                            continue;

                        if (shape.ActiveXControl.Type != targetType)
                            continue;

                        switch (targetType)
                        {
                            case ControlType.CommandButton:
                                var btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                if (ctrlCfg.Caption != null)
                                    btn.Caption = ctrlCfg.Caption;
                                if (ctrlCfg.Width.HasValue)
                                    btn.Width = ctrlCfg.Width.Value;
                                if (ctrlCfg.Height.HasValue)
                                    btn.Height = ctrlCfg.Height.Value;
                                break;

                            case ControlType.CheckBox:
                                var chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                                if (ctrlCfg.Value != null && Enum.TryParse<CheckValueType>(ctrlCfg.Value, out CheckValueType chkVal))
                                    chk.Value = chkVal;
                                if (ctrlCfg.Width.HasValue)
                                    chk.Width = ctrlCfg.Width.Value;
                                if (ctrlCfg.Height.HasValue)
                                    chk.Height = ctrlCfg.Height.Value;
                                break;

                            case ControlType.TextBox:
                                var txt = (TextBoxActiveXControl)shape.ActiveXControl;
                                if (ctrlCfg.Text != null)
                                    txt.Text = ctrlCfg.Text;
                                if (ctrlCfg.Width.HasValue)
                                    txt.Width = ctrlCfg.Width.Value;
                                if (ctrlCfg.Height.HasValue)
                                    txt.Height = ctrlCfg.Height.Value;
                                break;

                            case ControlType.SpinButton:
                                var spn = (SpinButtonActiveXControl)shape.ActiveXControl;
                                if (ctrlCfg.Position.HasValue)
                                    spn.Position = ctrlCfg.Position.Value;
                                if (ctrlCfg.Width.HasValue)
                                    spn.Width = ctrlCfg.Width.Value;
                                if (ctrlCfg.Height.HasValue)
                                    spn.Height = ctrlCfg.Height.Value;
                                break;

                            default:
                                Console.Error.WriteLine($"Control type not handled: {targetType}");
                                break;
                        }

                        applied = true;
                        break; // stop searching after first match for this control config
                    }

                    if (applied)
                        break;
                }

                if (!applied)
                {
                    Console.Error.WriteLine($"No matching ActiveX control found for type: {ctrlCfg.Type}");
                }
            }

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
    }
}