using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    // Represents a single control configuration entry from the JSON file.
    private class ControlConfig
    {
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public string ControlType { get; set; }
        public JsonElement Properties { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, JSON config file, output Visio file.
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <configJsonPath> <outputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON configuration file not found: {jsonPath}");
            return;
        }

        // Load the diagram inside a using block.
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Deserialize JSON configuration.
                List<ControlConfig> configs;
                try
                {
                    string jsonContent = File.ReadAllText(jsonPath);
                    configs = JsonSerializer.Deserialize<List<ControlConfig>>(jsonContent);
                    if (configs == null)
                    {
                        Console.Error.WriteLine("JSON deserialization resulted in null.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to parse JSON configuration: {ex.Message}");
                    return;
                }

                // Apply each configuration entry to the corresponding ActiveX control.
                foreach (var cfg in configs)
                {
                    // Validate page index.
                    if (cfg.PageIndex < 0 || cfg.PageIndex >= diagram.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid PageIndex {cfg.PageIndex} in configuration.");
                        continue;
                    }

                    Page page = diagram.Pages[cfg.PageIndex];

                    // Retrieve the shape by its ID.
                    Shape shape = page.Shapes.GetShape(cfg.ShapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {cfg.ShapeId} not found on page {cfg.PageIndex}.");
                        continue;
                    }

                    // Ensure the shape contains an ActiveX control.
                    if (shape.ActiveXControl == null)
                    {
                        Console.Error.WriteLine($"Shape ID {cfg.ShapeId} does not contain an ActiveX control.");
                        continue;
                    }

                    // Determine the target control type.
                    string targetTypeName = string.IsNullOrWhiteSpace(cfg.ControlType)
                        ? shape.ActiveXControl.Type.ToString()
                        : cfg.ControlType;

                    try
                    {
                        switch (targetTypeName)
                        {
                            case "CommandButton":
                                var cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                                ApplyCommonProperties(cmdBtn, cfg.Properties);
                                if (cfg.Properties.TryGetProperty("Caption", out JsonElement capElem) && capElem.ValueKind == JsonValueKind.String)
                                    cmdBtn.Caption = capElem.GetString();
                                break;

                            case "TextBox":
                                var txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                                ApplyCommonProperties(txtBox, cfg.Properties);
                                if (cfg.Properties.TryGetProperty("Text", out JsonElement txtElem) && txtElem.ValueKind == JsonValueKind.String)
                                    txtBox.Text = txtElem.GetString();
                                break;

                            case "CheckBox":
                                var chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                                ApplyCommonProperties(chkBox, cfg.Properties);
                                if (cfg.Properties.TryGetProperty("Caption", out JsonElement chkCap) && chkCap.ValueKind == JsonValueKind.String)
                                    chkBox.Caption = chkCap.GetString();
                                if (cfg.Properties.TryGetProperty("IsChecked", out JsonElement chkElem) && chkElem.ValueKind == JsonValueKind.True || chkElem.ValueKind == JsonValueKind.False)
                                {
                                    bool isChecked = chkElem.GetBoolean();
                                    chkBox.Value = isChecked ? CheckValueType.Checked : (CheckValueType)0;
                                }
                                break;

                            case "SpinButton":
                                var spinBtn = (SpinButtonActiveXControl)shape.ActiveXControl;
                                ApplyCommonProperties(spinBtn, cfg.Properties);
                                if (cfg.Properties.TryGetProperty("Position", out JsonElement posElem) && posElem.ValueKind == JsonValueKind.Number && posElem.TryGetInt32(out int posVal))
                                    spinBtn.Position = posVal;
                                break;

                            default:
                                Console.Error.WriteLine($"Unsupported or unrecognized ControlType '{targetTypeName}' for shape ID {cfg.ShapeId}.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error applying configuration to shape ID {cfg.ShapeId}: {ex.Message}");
                    }
                }

                // Save the modified diagram.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load Visio diagram: {ex.Message}");
        }
    }

    // Applies properties common to all ActiveX controls: Width, Height, X, Y.
    private static void ApplyCommonProperties(object control, JsonElement properties)
    {
        // Width
        if (properties.TryGetProperty("Width", out JsonElement wElem) && wElem.ValueKind == JsonValueKind.Number && wElem.TryGetDouble(out double wVal))
        {
            SetProperty(control, "Width", wVal);
        }

        // Height
        if (properties.TryGetProperty("Height", out JsonElement hElem) && hElem.ValueKind == JsonValueKind.Number && hElem.TryGetDouble(out double hVal))
        {
            SetProperty(control, "Height", hVal);
        }

        // X (horizontal position)
        if (properties.TryGetProperty("X", out JsonElement xElem) && xElem.ValueKind == JsonValueKind.Number && xElem.TryGetDouble(out double xVal))
        {
            SetProperty(control, "X", xVal);
        }

        // Y (vertical position)
        if (properties.TryGetProperty("Y", out JsonElement yElem) && yElem.ValueKind == JsonValueKind.Number && yElem.TryGetDouble(out double yVal))
        {
            SetProperty(control, "Y", yVal);
        }
    }

    // Helper to set a property via reflection (used for common numeric properties).
    private static void SetProperty(object target, string propertyName, double value)
    {
        var propInfo = target.GetType().GetProperty(propertyName);
        if (propInfo != null && propInfo.CanWrite)
        {
            propInfo.SetValue(target, value);
        }
    }
}