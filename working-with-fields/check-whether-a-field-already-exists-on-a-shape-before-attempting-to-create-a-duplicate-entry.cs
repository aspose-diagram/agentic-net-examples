using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <shapeId> <fieldFormula>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Error: Invalid shape ID.");
            return;
        }

        string fieldFormula = args[2];

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the target shape by its ID across all pages.
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        Shape s = page.Shapes.GetShape(shapeId);
                        if (s != null)
                        {
                            targetShape = s;
                            break;
                        }
                    }
                    catch
                    {
                        // GetShape throws if the ID is not on this page – ignore and continue.
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found.");
                    return;
                }

                // -----------------------------------------------------------------
                // Check whether a field with the same formula already exists.
                // -----------------------------------------------------------------
                bool fieldExists = false;
                foreach (Field existingField in targetShape.Fields)
                {
                    // Guard against null members.
                    if (existingField != null &&
                        existingField.Value != null &&
                        existingField.Value.Ufev != null &&
                        existingField.Value.Ufev.F == fieldFormula)
                    {
                        fieldExists = true;
                        break;
                    }
                }

                if (fieldExists)
                {
                    Console.WriteLine("A field with the specified formula already exists on the shape. No duplicate was added.");
                }
                else
                {
                    // -------------------------------------------------------------
                    // Create a new field and add it to the shape.
                    // -------------------------------------------------------------
                    Field newField = new Field();

                    // Set the field type to Undefined (generic custom field).
                    newField.Type.Value = TypeFieldValue.Undefined;

                    // Assign the formula to the field. The unit is left undefined.
                    newField.Value.Ufev.F = fieldFormula;
                    // If needed, you can also set the unit explicitly:
                    // newField.Value.Ufev.Unit = MeasureConst.Undefined;

                    // Add the newly created field to the shape's field collection.
                    targetShape.Fields.Add(newField);
                    Console.WriteLine("Field added to the shape.");

                    // -------------------------------------------------------------
                    // Save the updated diagram to a new file.
                    // -------------------------------------------------------------
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(inputPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
