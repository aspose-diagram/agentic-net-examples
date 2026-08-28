using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

namespace VisioActiveXUpdater
{
    // DTO representing a control configuration entry in the JSON file
    public class ControlConfig
    {
        public long ShapeId { get; set; }               // ID of the shape containing the ActiveX control
        public string ControlType { get; set; }         // e.g., "CommandButton", "Image", "CheckBox"
        public string Caption { get; set; }             // For CommandButton
        public double Width { get; set; }               // Desired width (in inches)
        public double Height { get; set; }              // Desired height (in inches)
        public string ImagePath { get; set; }           // Path to image file for ImageActiveXControl
        public string CheckValue { get; set; }          // "Checked" or "Unchecked" for CheckBoxActiveXControl
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Expected arguments: [0] Visio file path, [1] JSON config path, [2] output Visio file path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: VisioActiveXUpdater <inputVisio> <configJson> <outputVisio>");
                return;
            }

            string visioPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load JSON configuration
            List<ControlConfig> configs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                configs = JsonSerializer.Deserialize<List<ControlConfig>>(jsonContent);
                if (configs == null)
                {
                    Console.WriteLine("Failed to deserialize JSON configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON configuration: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Visio file: {ex.Message}");
                return;
            }

            // Apply each configuration entry to the corresponding shape
            foreach (var cfg in configs)
            {
                // Locate the shape by ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // GetShape throws if the ID does not exist on this page; catch and continue
                        targetShape = page.Shapes.GetShape(cfg.ShapeId);
                        if (targetShape != null)
                            break;
                    }
                    catch
                    {
                        // Ignore and continue searching other pages
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine($"Shape with ID {cfg.ShapeId} not found.");
                    continue;
                }

                // Ensure the shape actually hosts an ActiveX control
                if (targetShape.ActiveXControl == null)
                {
                    Console.WriteLine($"Shape ID {cfg.ShapeId} does not contain an ActiveX control.");
                    continue;
                }

                // Determine control type and apply properties
                ControlType actualType = targetShape.ActiveXControl.Type;

                // Use the string from JSON to match the enum (case‑insensitive)
                if (!Enum.TryParse<ControlType>(cfg.ControlType, true, out ControlType expectedType))
                {
                    Console.WriteLine($"Invalid ControlType '{cfg.ControlType}' in configuration.");
                    continue;
                }

                if (actualType != expectedType)
                {
                    Console.WriteLine($"Shape ID {cfg.ShapeId} control type mismatch (expected {expectedType}, found {actualType}).");
                    continue;
                }

                // Apply properties based on specific control class
                switch (actualType)
                {
                    case ControlType.CommandButton:
                        {
                            var btn = (CommandButtonActiveXControl)targetShape.ActiveXControl;
                            if (!string.IsNullOrEmpty(cfg.Caption))
                                btn.Caption = cfg.Caption;
                            btn.Width = cfg.Width;
                            btn.Height = cfg.Height;
                            break;
                        }
                    case ControlType.Image:
                        {
                            var imgCtrl = (ImageActiveXControl)targetShape.ActiveXControl;
                            if (!string.IsNullOrEmpty(cfg.ImagePath) && File.Exists(cfg.ImagePath))
                            {
                                imgCtrl.Picture = File.ReadAllBytes(cfg.ImagePath);
                            }
                            else
                            {
                                Console.WriteLine($"Image file '{cfg.ImagePath}' not found for shape ID {cfg.ShapeId}.");
                            }
                            imgCtrl.Width = cfg.Width;
                            imgCtrl.Height = cfg.Height;
                            break;
                        }
                    case ControlType.CheckBox:
                        {
                            var chkBox = (CheckBoxActiveXControl)targetShape.ActiveXControl;
                            // Set checked/unchecked state
                            if (string.Equals(cfg.CheckValue, "Checked", StringComparison.OrdinalIgnoreCase))
                            {
                                chkBox.Value = CheckValueType.Checked;
                            }
                            else if (string.Equals(cfg.CheckValue, "Unchecked", StringComparison.OrdinalIgnoreCase))
                            {
                                // Unchecked is represented by the integer value 0
                                chkBox.Value = (CheckValueType)0;
                            }
                            else
                            {
                                Console.WriteLine($"Invalid CheckValue '{cfg.CheckValue}' for shape ID {cfg.ShapeId}.");
                            }
                            chkBox.Width = cfg.Width;
                            chkBox.Height = cfg.Height;
                            break;
                        }
                    default:
                        Console.WriteLine($"Control type {actualType} is not handled by this utility.");
                        break;
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
    }
}