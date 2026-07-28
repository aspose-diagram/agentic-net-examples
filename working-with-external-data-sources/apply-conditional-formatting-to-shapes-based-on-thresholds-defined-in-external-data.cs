using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        // Placeholder for the provided load rule
        static Diagram LoadDiagram(string path)
        {
            // The actual implementation is supplied by the rule set.
            return new Diagram(path);
        }

        // Placeholder for the provided save rule
        static void SaveDiagram(Diagram diagram, string path)
        {
            // The actual implementation is supplied by the rule set.
            diagram.Save(path, SaveFileFormat.Vsdx);
        }

        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram using the rule‑provided method.
                Diagram diagram = LoadDiagram("input.vsdx");

                // Define thresholds (could be read from an external file).
                // For illustration we use hard‑coded values.
                var thresholds = new Dictionary<string, double>
                {
                    { "High",   80.0 },
                    { "Medium", 50.0 }
                };

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Assume the shape's Data1 cell contains the numeric value to evaluate.
                        if (double.TryParse(shape.Data1, out double shapeValue))
                        {
                            // Apply conditional formatting based on the thresholds.
                            if (shapeValue >= thresholds["High"])
                            {
                                // High values – apply a red style.
                                shape.SetPresetThemeStyleMatrics(
                                    PresetStyleMatricsValue.Style3,
                                    PresetColorMatricsValue.Color3);
                            }
                            else if (shapeValue >= thresholds["Medium"])
                            {
                                // Medium values – apply an orange style.
                                shape.SetPresetThemeStyleMatrics(
                                    PresetStyleMatricsValue.Style2,
                                    PresetColorMatricsValue.Color2);
                            }
                            else
                            {
                                // Low values – apply a green style.
                                shape.SetPresetThemeStyleMatrics(
                                    PresetStyleMatricsValue.Style1,
                                    PresetColorMatricsValue.Color1);
                            }

                            // Refresh the shape to ensure the visual changes take effect.
                            shape.RefreshData();
                        }
                    }
                }

                // Save the modified diagram using the rule‑provided method.
                SaveDiagram(diagram, "output.vsdx");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }