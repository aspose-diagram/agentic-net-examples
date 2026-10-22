using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, target shape name, source shape name, output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <targetShapeName> <sourceShapeName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string targetShapeName = args[1];
        string sourceShapeName = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume operations on the first page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Locate the target shape (the shape whose field formula will be updated)
            Shape targetShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shp = page.Shapes[i];
                if (shp.NameU != null && shp.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Target shape \"{targetShapeName}\" not found on the first page.");
                return;
            }

            // Locate the source shape (the shape whose data will be referenced)
            Shape sourceShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shp = page.Shapes[i];
                if (shp.NameU != null && shp.NameU.Equals(sourceShapeName, StringComparison.OrdinalIgnoreCase))
                {
                    sourceShape = shp;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Source shape \"{sourceShapeName}\" not found on the first page.");
                return;
            }

            // Ensure the target shape has at least one field to modify
            if (targetShape.Fields == null || targetShape.Fields.Count == 0)
            {
                Console.Error.WriteLine($"Target shape \"{targetShapeName}\" does not contain any fields to update.");
                return;
            }

            // Update the first field's formula to reference the source shape's Data1 property
            // Visio formula syntax: Sheet.<ShapeID>!Data1
            Field field = targetShape.Fields[0];
            field.Value.Ufev.F = $"Sheet.{sourceShape.ID}!Data1";
            // Set unit to undefined (optional, but aligns with typical formula setup)
            field.Value.Ufev.Unit = MeasureConst.Undefined;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}