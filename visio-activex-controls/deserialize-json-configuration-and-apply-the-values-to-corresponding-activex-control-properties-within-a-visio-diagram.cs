using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing a control configuration from JSON
    private class ControlConfig
    {
        public long ShapeId { get; set; }
        public string? ControlType { get; set; }   // Made nullable to avoid CS8618 warning
        public string? Caption { get; set; }       // Made nullable to avoid CS8618 warning
        public double? Width { get; set; }
        public double? Height { get; set; }
        public string? Text { get; set; }          // Made nullable to avoid CS8618 warning
        public double? Position { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect at least two arguments: diagram path and JSON config path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonConfigPath> [outputDiagramPath]");
            return;
        }

        string diagramPath = args[0];
        string jsonConfigPath = args[1];
        string outputDiagramPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(jsonConfigPath))
        {
            Console.Error.WriteLine($"JSON configuration file not found: {jsonConfigPath}");
            return;
        }

        // Load and deserialize JSON configuration
        List<ControlConfig> configs;
        try
        {
            string json = File.ReadAllText(jsonConfigPath);
            configs = JsonSerializer.Deserialize<List<ControlConfig>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (configs == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON configuration: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply configurations
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                foreach (ControlConfig cfg in configs)
                {
                    // Locate the shape by ID across all pages
                    Shape? targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        Shape? shape = page.Shapes.GetShape(cfg.ShapeId);
                        if (shape != null)
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {cfg.ShapeId} not found.");
                        continue;
                    }

                    // Ensure the shape contains an ActiveX control
                    if (targetShape.ActiveXControl == null)
                    {
                        Console.Error.WriteLine($"Shape ID {cfg.ShapeId} does not contain an ActiveX control.");
                        continue;
                    }

                    // Determine control type (override if provided in config)
                    ControlType controlType = targetShape.ActiveXControl.Type;
                    if (!string.IsNullOrEmpty(cfg.ControlType) &&
                        Enum.TryParse<ControlType>(cfg.ControlType, out ControlType parsedType))
                    {
                        controlType = parsedType;
                    }

                    // Apply properties based on control type
                    switch (controlType)
                    {
                        case ControlType.CommandButton:
                            {
                                var btn = (CommandButtonActiveXControl)targetShape.ActiveXControl;
                                if (!string.IsNullOrEmpty(cfg.Caption))
                                    btn.Caption = cfg.Caption;
                                if (cfg.Width.HasValue)
                                    btn.Width = (int)cfg.Width.Value;   // Cast to int as required by API
                                if (cfg.Height.HasValue)
                                    btn.Height = (int)cfg.Height.Value; // Cast to int as required by API
                                break;
                            }
                        case ControlType.TextBox:
                            {
                                var txt = (TextBoxActiveXControl)targetShape.ActiveXControl;
                                if (!string.IsNullOrEmpty(cfg.Text))
                                    txt.Text = cfg.Text;
                                if (cfg.Width.HasValue)
                                    txt.Width = (int)cfg.Width.Value;   // Cast to int
                                if (cfg.Height.HasValue)
                                    txt.Height = (int)cfg.Height.Value; // Cast to int
                                break;
                            }
                        case ControlType.SpinButton:
                            {
                                var spin = (SpinButtonActiveXControl)targetShape.ActiveXControl;
                                if (cfg.Position.HasValue)
                                    spin.Position = (int)cfg.Position.Value; // Cast to int
                                if (cfg.Width.HasValue)
                                    spin.Width = (int)cfg.Width.Value;       // Cast to int
                                if (cfg.Height.HasValue)
                                    spin.Height = (int)cfg.Height.Value;     // Cast to int
                                break;
                            }
                        case ControlType.CheckBox:
                            {
                                var chk = (CheckBoxActiveXControl)targetShape.ActiveXControl;
                                if (!string.IsNullOrEmpty(cfg.Caption))
                                    chk.Caption = cfg.Caption;
                                if (cfg.Width.HasValue)
                                    chk.Width = (int)cfg.Width.Value;   // Cast to int
                                if (cfg.Height.HasValue)
                                    chk.Height = (int)cfg.Height.Value; // Cast to int
                                break;
                            }
                        case ControlType.Image:
                            {
                                var img = (ImageActiveXControl)targetShape.ActiveXControl;
                                // If Caption holds a file path, load image data
                                if (!string.IsNullOrEmpty(cfg.Caption) && File.Exists(cfg.Caption))
                                {
                                    img.Picture = File.ReadAllBytes(cfg.Caption);
                                }
                                if (cfg.Width.HasValue)
                                    img.Width = (int)cfg.Width.Value;   // Cast to int
                                if (cfg.Height.HasValue)
                                    img.Height = (int)cfg.Height.Value; // Cast to int
                                break;
                            }
                        default:
                            {
                                Console.Error.WriteLine($"Unsupported control type for shape ID {cfg.ShapeId}.");
                                break;
                            }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
