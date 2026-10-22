using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [shapeId]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        long targetShapeId = 0; // 0 means process the first shape found.

        if (args.Length >= 3 && !long.TryParse(args[2], out targetShapeId))
        {
            Console.Error.WriteLine("Invalid shapeId argument.");
            return;
        }

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the target shape.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // If a specific shape ID was provided, try to get that shape.
                if (targetShapeId != 0)
                {
                    // Shape IDs are stored as long but GetShape expects an int.
                    targetShape = page.Shapes.GetShape((int)targetShapeId);
                    if (targetShape != null)
                        break;
                }
                else
                {
                    // No ID supplied – pick the first non‑deleted shape on the first page.
                    foreach (Shape shp in page.Shapes)
                    {
                        if (shp.Del == BOOL.False)
                        {
                            targetShape = shp;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("Shape not found in the diagram.");
                return;
            }

            // ------------------------------
            // Remove all existing fields.
            // ------------------------------
            List<Field> fieldsToRemove = new List<Field>();
            foreach (Field fld in targetShape.Fields)
            {
                fieldsToRemove.Add(fld);
            }

            foreach (Field fld in fieldsToRemove)
            {
                targetShape.Fields.Remove(fld);
            }

            // ------------------------------
            // Add fresh set of updated fields.
            // ------------------------------

            // Example: add a custom field displaying a static text.
            Field newField1 = new Field();
            newField1.Value.Val = "Custom Text 1";
            newField1.Value.Ufev.Unit = MeasureConst.Undefined;
            newField1.Value.Ufev.F = ""; // No formula.
            targetShape.Fields.Add(newField1);

            // Example: add a second field with a formula.
            Field newField2 = new Field();
            newField2.Value.Val = ""; // Value will be calculated from the formula.
            newField2.Value.Ufev.Unit = MeasureConst.Undefined;
            newField2.Value.Ufev.F = "Width*Height"; // Simple formula example.
            targetShape.Fields.Add(newField2);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}