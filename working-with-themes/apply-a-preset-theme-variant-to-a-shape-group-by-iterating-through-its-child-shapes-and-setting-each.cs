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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify group shapes.
                    if (shape.Type == TypeValue.Group)
                    {
                        // Iterate through child shapes of the group.
                        foreach (Shape child in shape.Shapes)
                        {
                            // Apply a preset theme to the child shape.
                            child.PresetTheme = PresetThemeValue.Bubble;

                            // Apply a preset theme variant to the child shape.
                            child.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        }
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}