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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages to find group shapes.
            foreach (Page page in diagram.Pages)
            {
                // Collect group shapes on the current page.
                List<Shape> groupShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.Type == TypeValue.Group)
                        groupShapes.Add(shape);
                }

                // Apply the preset theme variant to each group and its children.
                foreach (Shape group in groupShapes)
                {
                    ApplyPresetThemeVariantToGroup(group);
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Recursively applies a preset theme variant to a group shape and all of its child shapes.
    /// </summary>
    /// <param name="groupShape">The group shape to process.</param>
    private static void ApplyPresetThemeVariantToGroup(Shape groupShape)
    {
        // Ensure the shape is a group before processing.
        if (groupShape == null || groupShape.Type != TypeValue.Group)
            return;

        // Iterate over direct child shapes of the group.
        foreach (Shape child in groupShape.Shapes)
        {
            // Skip deleted shapes.
            if (child.Del == BOOL.True)
                continue;

            // If the child is itself a group, recurse.
            if (child.Type == TypeValue.Group)
            {
                ApplyPresetThemeVariantToGroup(child);
            }
            else
            {
                // Apply the desired preset theme variant.
                // Example uses Variant1; adjust as needed.
                child.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }
        }
    }
}