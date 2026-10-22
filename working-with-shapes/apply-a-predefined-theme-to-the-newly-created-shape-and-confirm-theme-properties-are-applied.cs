using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page where the new shape will be added.
            Page page = diagram.Pages[0];

            // Define position for the new shape (in inches).
            double pinX = 2.0;
            double pinY = 2.0;

            // Use a built‑in master name. "Rectangle" is commonly available.
            string masterName = "Rectangle";

            // Add the shape to the page. The fourth argument isCalculate is a bool.
            long shapeId = page.AddShape(pinX, pinY, masterName, false);

            // Retrieve the newly added shape instance.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            // Apply a predefined theme to the shape.
            // All three properties are write‑only; they cannot be read back.
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Confirmation message – we cannot read back the values, but we know the assignment succeeded.
            Console.WriteLine($"Applied theme 'Bubble' with variant 'Variant1' and quick style 'VariantStyle1' to shape ID {shapeId}.");

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}