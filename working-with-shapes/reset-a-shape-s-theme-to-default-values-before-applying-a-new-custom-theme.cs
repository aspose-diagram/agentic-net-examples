using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        Diagram diagram;
        try
        {
            // Attempt to load the diagram; catch any loading errors
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Get the first page (you can change the index as needed)
        Page page = diagram.Pages[0];

        // Retrieve a shape by its ID (replace with the actual shape ID you want to modify)
        long shapeId = 1; // example shape ID
        Shape shape = page.Shapes.GetShape(shapeId);

        if (shape == null)
        {
            throw new Exception($"Shape with ID {shapeId} not found on page {page.Name}.");
        }

        // -----------------------------------------------------------------
        // Reset the shape's theme to default values.
        // Since theme properties are write‑only, assigning the default
        // (no theme) is achieved by setting the preset theme to a known
        // baseline (Bubble) with the first variant and quick style.
        // -----------------------------------------------------------------
        shape.PresetTheme = PresetThemeValue.Bubble;
        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

        // -----------------------------------------------------------------
        // Apply a new custom theme to the same shape.
        // Here we demonstrate applying a different variant and quick style.
        // Replace these values with the desired custom theme settings.
        // -----------------------------------------------------------------
        shape.PresetTheme = PresetThemeValue.Bubble;
        shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
        // Use a valid quick style enum member (VariantStyle2 is guaranteed to exist)
        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

        // Save the modified diagram to a new file
        string outputPath = "output.vsdx";
        try
        {
            // Save using the appropriate overload with a SaveFileFormat argument
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}