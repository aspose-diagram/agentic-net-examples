using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace VisioActiveXConfigurator
{
    // DTO for JSON configuration
    public class ConfigRoot
    {
        public List<ControlConfig> Controls { get; set; } = new();
    }

    public class ControlConfig
    {
        public long ShapeId { get; set; }

        // Common properties
        public string? Caption { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }

        // TextBox specific
        public string? Text { get; set; }

        // CheckBox specific
        public CheckValueType? CheckValue { get; set; }

        // SpinButton specific
        public int? Position { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: VisioActiveXConfigurator <inputVisioPath> <configJsonPath> <outputVisioPath>");
                return;
            }

            string visioPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Read and deserialize JSON configuration
            string jsonContent = File.ReadAllText(jsonPath);
            ConfigRoot config = JsonSerializer.Deserialize<ConfigRoot>(jsonContent);
            if (config == null || config.Controls == null)
            {
                Console.WriteLine("Invalid or empty configuration.");
                return;
            }

            // Apply configuration to each specified ActiveX control
            foreach (ControlConfig ctrlCfg in config.Controls)
            {
                Shape shape = FindShapeById(diagram, ctrlCfg.ShapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {ctrlCfg.ShapeId} not found.");
                    continue;
                }

                if (shape.ActiveXControl == null)
                {
                    Console.WriteLine($"Shape ID {ctrlCfg.ShapeId} does not contain an ActiveX control.");
                    continue;
                }

                var axControl = shape.ActiveXControl;

                // Apply properties based on control type
                switch (axControl.Type)
                {
                    case ControlType.CommandButton:
                        var btn = (CommandButtonActiveXControl)axControl;
                        if (ctrlCfg.Caption != null) btn.Caption = ctrlCfg.Caption;
                        if (ctrlCfg.Width.HasValue) btn.Width = ctrlCfg.Width.Value;
                        if (ctrlCfg.Height.HasValue) btn.Height = ctrlCfg.Height.Value;
                        break;

                    case ControlType.TextBox:
                        var txtBox = (TextBoxActiveXControl)axControl;
                        if (ctrlCfg.Text != null) txtBox.Text = ctrlCfg.Text;
                        if (ctrlCfg.Width.HasValue) txtBox.Width = ctrlCfg.Width.Value;
                        if (ctrlCfg.Height.HasValue) txtBox.Height = ctrlCfg.Height.Value;
                        break;

                    case ControlType.CheckBox:
                        var chkBox = (CheckBoxActiveXControl)axControl;
                        if (ctrlCfg.CheckValue.HasValue) chkBox.Value = ctrlCfg.CheckValue.Value;
                        if (ctrlCfg.Width.HasValue) chkBox.Width = ctrlCfg.Width.Value;
                        if (ctrlCfg.Height.HasValue) chkBox.Height = ctrlCfg.Height.Value;
                        break;

                    case ControlType.SpinButton:
                        var spin = (SpinButtonActiveXControl)axControl;
                        if (ctrlCfg.Position.HasValue) spin.Position = ctrlCfg.Position.Value;
                        if (ctrlCfg.Width.HasValue) spin.Width = ctrlCfg.Width.Value;
                        if (ctrlCfg.Height.HasValue) spin.Height = ctrlCfg.Height.Value;
                        break;

                    case ControlType.Image:
                        // ImageActiveXControl uses the Picture property (byte[]). Example placeholder:
                        // if (ctrlCfg.ImagePath != null) imgCtrl.Picture = File.ReadAllBytes(ctrlCfg.ImagePath);
                        // No direct properties defined in the current config schema.
                        break;

                    default:
                        Console.WriteLine($"Unsupported ActiveX control type on shape ID {ctrlCfg.ShapeId}.");
                        break;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }

        // Helper to locate a shape by its unique ID across all pages
        private static Shape FindShapeById(Diagram diagram, long shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                // Shapes.GetShape accepts a long ID
                try
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                        return shape;
                }
                catch
                {
                    // Ignore and continue searching
                }
            }
            return null;
        }
    }
}