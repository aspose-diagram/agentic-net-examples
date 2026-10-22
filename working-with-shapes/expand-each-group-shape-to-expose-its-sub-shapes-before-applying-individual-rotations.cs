using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block (required pattern).
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // ---------- First pass: collect group shapes ----------
                    List<Shape> groupsToExpand = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify group shapes by their Type.
                        if (shape.Type == TypeValue.Group)
                        {
                            groupsToExpand.Add(shape);
                        }
                    }

                    // ---------- Second pass: ungroup each collected group ----------
                    foreach (Shape groupShape in groupsToExpand)
                    {
                        try
                        {
                            // Ungroup the shape; its child shapes become top‑level shapes on the page.
                            groupShape.Ungroup();
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to ungroup shape ID {groupShape.ID}: {ex.Message}");
                        }
                    }

                    // ---------- Third pass: rotate every remaining shape ----------
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        try
                        {
                            // Rotate the shape to 45 degrees (example angle).
                            // Using SetAngle method as documented.
                            shape.SetAngle(45.0);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to rotate shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}