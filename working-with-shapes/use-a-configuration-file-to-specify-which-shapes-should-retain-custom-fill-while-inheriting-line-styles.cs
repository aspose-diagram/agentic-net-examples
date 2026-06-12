using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

public class Config
{
    // List of shape IDs that should keep their custom fill.
    public List<long>? ShapeIds { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Paths – adjust as needed or pass via command‑line arguments.
            string diagramPath = "input.vsdx";
            string configPath = "config.json";
            string outputPath = "output.vsdx";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Load configuration (JSON) that contains shape IDs to retain custom fill.
            HashSet<long> retainFillIds = new HashSet<long>();
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                Config? cfg = JsonSerializer.Deserialize<Config>(json);
                if (cfg?.ShapeIds != null)
                {
                    foreach (long id in cfg.ShapeIds)
                    {
                        retainFillIds.Add(id);
                    }
                }
            }

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // ALWAYS inherit line style from the parent/style.
                    shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                    shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                    shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;

                    // For shapes NOT listed in the config, also inherit fill.
                    if (!retainFillIds.Contains(shape.ID))
                    {
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                    }
                    // Shapes listed in the config keep their existing Fill values.
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}