using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Example external data: shape universal name -> numeric value
            var externalValues = new Dictionary<string, double>
            {
                { "Shape1", 120.0 },
                { "Shape2", 80.0 },
                { "Shape3", 150.0 }
            };

            // Threshold for conditional formatting
            double threshold = 100.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply formatting only if we have a value for the shape
                    if (externalValues.TryGetValue(shape.NameU, out double value))
                    {
                        if (value > threshold)
                        {
                            // High value: use Style1 with Color1
                            shape.SetPresetThemeStyleMatrics(
                                PresetStyleMatricsValue.Style1,
                                PresetColorMatricsValue.Color1);
                        }
                        else
                        {
                            // Low value: use Style2 with Color2
                            shape.SetPresetThemeStyleMatrics(
                                PresetStyleMatricsValue.Style2,
                                PresetColorMatricsValue.Color2);
                        }

                        // Refresh shape to ensure changes are applied
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
