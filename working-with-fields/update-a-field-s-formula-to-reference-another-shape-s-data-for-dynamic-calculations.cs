using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Locate the source shape (the shape whose data will be referenced)
            // -----------------------------------------------------------------
            Shape sourceShape = null;
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shp = page.Shapes[i];
                    // Identify by the shape's universal name (NameU). Adjust as needed.
                    if (shp.NameU != null && shp.NameU.Equals("SourceShape", StringComparison.OrdinalIgnoreCase))
                    {
                        sourceShape = shp;
                        break;
                    }
                }
                if (sourceShape != null) break;
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("Source shape named 'SourceShape' was not found.");
                return;
            }

            // ---------------------------------------------------------------
            // Locate the target shape (the shape containing the field to edit)
            // ---------------------------------------------------------------
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shp = page.Shapes[i];
                    if (shp.NameU != null && shp.NameU.Equals("TargetShape", StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shp;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("Target shape named 'TargetShape' was not found.");
                return;
            }

            // ---------------------------------------------------------------
            // Ensure the target shape has at least one field to modify
            // ---------------------------------------------------------------
            if (targetShape.Fields == null || targetShape.Fields.Count == 0)
            {
                Console.Error.WriteLine("Target shape does not contain any fields to update.");
                return;
            }

            // ---------------------------------------------------------------
            // Update the first field's formula to reference a property of the source shape
            // ---------------------------------------------------------------
            // Example assumes the source shape has a custom property named "MyData"
            // The formula uses the Visio syntax: Sheet.<ShapeID>!Prop.<PropertyName>
            Field fieldToUpdate = targetShape.Fields[0];
            string referencedPropertyName = "MyData"; // adjust to actual property name on source shape
            fieldToUpdate.Value.Ufev.F = $"Sheet.{sourceShape.ID}!Prop.{referencedPropertyName}";

            // ---------------------------------------------------------------
            // Save the modified diagram
            // ---------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}