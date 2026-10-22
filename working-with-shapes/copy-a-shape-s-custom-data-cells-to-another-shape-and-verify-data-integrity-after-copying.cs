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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape to copy from
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }

            // Select the first shape as the source shape
            Shape sourceShape = page.Shapes[0];

            // Verify the source shape has a master (required for adding a new shape)
            if (sourceShape.Master == null)
            {
                Console.Error.WriteLine("Source shape does not have an associated master.");
                return;
            }

            // Add a new shape on the same page using the same master as the source shape
            // Position it slightly offset from the source shape
            double offsetX = 2.0; // inches
            double newPinX = sourceShape.XForm.PinX.Value + offsetX;
            double newPinY = sourceShape.XForm.PinY.Value;
            long newShapeId = page.AddShape(newPinX, newPinY, sourceShape.Master.Name, false);

            // Retrieve the newly added target shape
            Shape targetShape = page.Shapes.GetShape(newShapeId);

            // Copy custom properties (Props) from source to target
            foreach (Prop srcProp in sourceShape.Props)
            {
                // Create a new property and copy all relevant fields
                Prop newProp = new Prop();
                newProp.Name = srcProp.Name;
                newProp.Label.Value = srcProp.Label.Value;
                newProp.Prompt.Value = srcProp.Prompt.Value;
                newProp.Type.Value = srcProp.Type.Value;
                newProp.Value.Val = srcProp.Value.Val;

                // Add the new property to the target shape
                targetShape.Props.Add(newProp);
            }

            // Verify that all custom properties were copied correctly
            foreach (Prop srcProp in sourceShape.Props)
            {
                bool found = false;
                foreach (Prop tgtProp in targetShape.Props)
                {
                    if (tgtProp.Name == srcProp.Name)
                    {
                        // Compare each field for equality
                        if (tgtProp.Label.Value != srcProp.Label.Value ||
                            tgtProp.Prompt.Value != srcProp.Prompt.Value ||
                            tgtProp.Type.Value != srcProp.Type.Value ||
                            tgtProp.Value.Val != srcProp.Value.Val)
                        {
                            throw new Exception($"Property mismatch for '{srcProp.Name}'.");
                        }
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    throw new Exception($"Property '{srcProp.Name}' was not found in the target shape.");
                }
            }

            Console.WriteLine("All custom properties were copied and verified successfully.");

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}