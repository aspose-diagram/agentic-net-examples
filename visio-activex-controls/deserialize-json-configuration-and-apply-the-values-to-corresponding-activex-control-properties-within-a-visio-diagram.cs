using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace VisioActiveXUpdater
{
    // DTO representing the JSON configuration for an ActiveX control
    public class ControlConfig
    {
        public long ShapeId { get; set; }               // Unique shape ID containing the ActiveX control
        public string? Caption { get; set; }            // For CommandButton
        public double? Width { get; set; }              // Width in inches
        public double? Height { get; set; }             // Height in inches
        public string? Text { get; set; }               // For TextBox
        public string? ImagePath { get; set; }          // For Image control (file path)
        public bool? Checked { get; set; }              // For CheckBox (true = Checked)
        public int? Position { get; set; }              // For SpinButton (numeric position)
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            // Expected arguments: diagramPath jsonConfigPath outputPath
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Usage: VisioActiveXUpdater <diagramPath> <jsonConfigPath> <outputPath>");
                return;
            }

            string diagramPath = args[0];
            string jsonConfigPath = args[1];
            string outputPath = args[2];

            // Guard: ensure diagram file exists
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            // Guard: ensure JSON config file exists
            if (!File.Exists(jsonConfigPath))
            {
                Console.Error.WriteLine($"File not found: {jsonConfigPath}");
                return;
            }

            Diagram diagram;
            try
            {
                // Load the Visio diagram (Aspose operation)
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Deserialize JSON configuration
            string json = File.ReadAllText(jsonConfigPath);
            List<ControlConfig>? configs = JsonSerializer.Deserialize<List<ControlConfig>>(json);
            if (configs == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON configuration.");
                return;
            }

            // Apply each configuration entry to the corresponding ActiveX control
            foreach (ControlConfig cfg in configs)
            {
                Shape? shape = GetShapeById(diagram, cfg.ShapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {cfg.ShapeId} not found.");
                    continue;
                }

                if (shape.ActiveXControl == null)
                {
                    Console.WriteLine($"Shape ID {cfg.ShapeId} does not contain an ActiveX control.");
                    continue;
                }

                // Determine control type and cast accordingly
                ControlType ctrlType = shape.ActiveXControl.Type;

                if (ctrlType == ControlType.CommandButton)
                {
                    var btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                    if (cfg.Caption != null) btn.Caption = cfg.Caption;
                    if (cfg.Width.HasValue) btn.Width = cfg.Width.Value;
                    if (cfg.Height.HasValue) btn.Height = cfg.Height.Value;
                }
                else if (ctrlType == ControlType.TextBox)
                {
                    var txt = (TextBoxActiveXControl)shape.ActiveXControl;
                    if (cfg.Text != null) txt.Text = cfg.Text;
                }
                else if (ctrlType == ControlType.Image)
                {
                    var img = (ImageActiveXControl)shape.ActiveXControl;
                    if (cfg.ImagePath != null && File.Exists(cfg.ImagePath))
                        img.Picture = File.ReadAllBytes(cfg.ImagePath);
                }
                else if (ctrlType == ControlType.CheckBox)
                {
                    var chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                    if (cfg.Checked.HasValue)
                        chk.Value = cfg.Checked.Value ? CheckValueType.Checked : (CheckValueType)0; // unchecked
                }
                else if (ctrlType == ControlType.SpinButton)
                {
                    var spin = (SpinButtonActiveXControl)shape.ActiveXControl;
                    if (cfg.Position.HasValue) spin.Position = cfg.Position.Value;
                }
                else
                {
                    Console.WriteLine($"Unsupported ActiveX control type {ctrlType} on shape ID {cfg.ShapeId}.");
                }
            }

            try
            {
                // Save the updated diagram (Aspose operation)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }

        // Helper to locate a shape by its unique ID across all pages
        private static Shape? GetShapeById(Diagram diagram, long shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                // Shapes.GetShape expects a long ID; catch if not found on this page
                try
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                        return shape;
                }
                catch
                {
                    // Shape not on this page; continue searching
                }
            }
            return null;
        }
    }
}