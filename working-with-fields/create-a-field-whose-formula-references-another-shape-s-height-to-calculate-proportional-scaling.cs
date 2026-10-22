using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Locate the source shape whose Height will be referenced.
                // Assumes the shape has a unique NameU of "SourceShape".
                // -----------------------------------------------------------------
                Shape sourceShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == "SourceShape")
                        {
                            sourceShape = shape;
                            break;
                        }
                    }
                    if (sourceShape != null) break;
                }

                if (sourceShape == null)
                {
                    Console.Error.WriteLine("Source shape 'SourceShape' not found.");
                    return;
                }

                // -----------------------------------------------------------------
                // Locate the target shape where the custom field will be added.
                // Assumes the shape has a unique NameU of "TargetShape".
                // -----------------------------------------------------------------
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == "TargetShape")
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Target shape 'TargetShape' not found.");
                    return;
                }

                // -----------------------------------------------------------------
                // Create a new text insertion field on the target shape.
                // The field's formula references the Height of the source shape
                // and scales it by 0.5 (i.e., half the source height).
                // -----------------------------------------------------------------
                Field field = new Field();

                // Set the field type to Undefined (generic formula field)
                field.Type.Value = TypeFieldValue.Undefined;

                // Ensure the unit is undefined (default)
                field.Value.Ufev.Unit = MeasureConst.Undefined;

                // Build the formula string referencing the source shape's Height
                field.Value.Ufev.F = "Height of SourceShape * 0.5";

                // Add the field to the target shape's Fields collection
                targetShape.Fields.Add(field);

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}