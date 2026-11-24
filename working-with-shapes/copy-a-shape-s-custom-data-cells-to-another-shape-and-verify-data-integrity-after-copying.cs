using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, source shape ID, target shape ID
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <sourceShapeId> <targetShapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string sourceIdStr = args[2];
        string targetIdStr = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs
        if (!long.TryParse(sourceIdStr, out long sourceShapeId))
        {
            Console.Error.WriteLine($"Invalid source shape ID: {sourceIdStr}");
            return;
        }
        if (!long.TryParse(targetIdStr, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid target shape ID: {targetIdStr}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate source shape
            Shape sourceShape = null;
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Try to get the shape by ID from the current page
                if (sourceShape == null)
                {
                    try { sourceShape = page.Shapes.GetShape(sourceShapeId); } catch { }
                }
                if (targetShape == null)
                {
                    try { targetShape = page.Shapes.GetShape(targetShapeId); } catch { }
                }
                if (sourceShape != null && targetShape != null) break;
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Source shape with ID {sourceShapeId} not found.");
                return;
            }
            if (targetShape == null)
            {
                Console.Error.WriteLine($"Target shape with ID {targetShapeId} not found.");
                return;
            }

            // Clear existing custom properties on the target shape (optional, depending on requirements)
            targetShape.Props.Clear();

            // Copy each custom property from source to target
            foreach (Prop srcProp in sourceShape.Props)
            {
                // Create a new property and copy all relevant fields
                Prop newProp = new Prop();
                newProp.Name = srcProp.Name;                     // Property name
                newProp.Label.Value = srcProp.Label.Value;       // Label (display name)
                newProp.Prompt.Value = srcProp.Prompt.Value;     // Prompt (optional description)
                newProp.Type.Value = srcProp.Type.Value;         // Data type (e.g., String, Number)
                newProp.Value.Val = srcProp.Value.Val;           // Actual value

                // Add the new property to the target shape
                targetShape.Props.Add(newProp);
            }

            // Verify that the target shape now contains identical custom properties
            bool integrityOk = true;
            if (sourceShape.Props.Count != targetShape.Props.Count)
            {
                integrityOk = false;
                Console.Error.WriteLine("Property count mismatch after copying.");
            }
            else
            {
                for (int i = 0; i < sourceShape.Props.Count; i++)
                {
                    Prop src = sourceShape.Props[i];
                    Prop tgt = targetShape.Props[i];

                    if (src.Name != tgt.Name ||
                        src.Label.Value != tgt.Label.Value ||
                        src.Prompt.Value != tgt.Prompt.Value ||
                        src.Type.Value != tgt.Type.Value ||
                        src.Value.Val != tgt.Value.Val)
                    {
                        integrityOk = false;
                        Console.Error.WriteLine($"Mismatch found in property '{src.Name}'.");
                        break;
                    }
                }
            }

            if (integrityOk)
            {
                Console.WriteLine("Custom property copy verification succeeded.");
            }
            else
            {
                throw new Exception("Custom property copy verification failed.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}