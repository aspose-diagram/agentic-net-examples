using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace ActiveXControlSerialization
{
    // DTO representing the serializable properties of an ActiveX control
    public class ActiveXControlDto
    {
        public long ShapeId { get; set; }
        public string ControlType { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLocked { get; set; }
        public bool IsTransparent { get; set; }
        public string MousePointer { get; set; }
        public string IMEMode { get; set; }
        public int BackOleColor { get; set; }
        public int ForeOleColor { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output JSON file path
                string outputPath = "activex_controls.json";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    var controls = new List<ActiveXControlDto>();

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape contains an ActiveX control
                            if (shape.ActiveXControl != null)
                            {
                                ActiveXControl ctrl = shape.ActiveXControl;

                                var dto = new ActiveXControlDto
                                {
                                    ShapeId = shape.ID,
                                    ControlType = ctrl.Type.ToString(),
                                    Width = ctrl.Width,
                                    Height = ctrl.Height,
                                    IsEnabled = ctrl.IsEnabled,
                                    IsLocked = ctrl.IsLocked,
                                    IsTransparent = ctrl.IsTransparent,
                                    MousePointer = ctrl.MousePointer.ToString(),
                                    IMEMode = ctrl.IMEMode.ToString(),
                                    BackOleColor = ctrl.BackOleColor,
                                    ForeOleColor = ctrl.ForeOleColor
                                };

                                controls.Add(dto);
                            }
                        }
                    }

                    // Serialize the list to JSON with indentation
                    var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(controls, jsonOptions);

                    // Write JSON to the output file
                    File.WriteAllText(outputPath, json);
                    Console.WriteLine($"Serialized {controls.Count} ActiveX control(s) to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}