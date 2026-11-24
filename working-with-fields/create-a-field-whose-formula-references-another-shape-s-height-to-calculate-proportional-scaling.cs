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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape sourceShape = null;
                Shape targetShape = null;

                // Locate shapes by their universal names
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == "SourceShape")
                            sourceShape = shape;
                        if (shape.NameU == "TargetShape")
                            targetShape = shape;
                    }
                }

                // Verify that both shapes were found
                if (sourceShape == null)
                {
                    Console.Error.WriteLine("Source shape 'SourceShape' not found.");
                    return;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Target shape 'TargetShape' not found.");
                    return;
                }

                // Create a new text insertion field on the target shape
                Field newField = new Field();

                // Set the field type to Undefined (generic formula field)
                newField.Type.Value = TypeFieldValue.Undefined;

                // Define the formula that references the source shape's Height
                // This formula calculates half of the source shape's height
                newField.Value.Ufev.F = "SourceShape!Height * 0.5";

                // Set the unit to undefined (default)
                newField.Value.Ufev.Unit = MeasureConst.Undefined;

                // Add the field to the target shape's Fields collection
                targetShape.Fields.Add(newField);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}