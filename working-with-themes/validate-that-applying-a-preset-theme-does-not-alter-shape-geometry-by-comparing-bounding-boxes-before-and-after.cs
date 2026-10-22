using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Store original geometry for each shape (excluding deleted shapes)
            var originalGeometry = new Dictionary<long, (double PinX, double PinY, double Width, double Height)>();

            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                originalGeometry[shape.ID] = (
                    shape.XForm.PinX.Value,
                    shape.XForm.PinY.Value,
                    shape.XForm.Width.Value,
                    shape.XForm.Height.Value
                );
            }

            // Apply a preset theme to the page (write‑only properties)
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // After applying the theme, compare geometry
            const double tolerance = 1e-4;
            bool geometryChanged = false;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                if (!originalGeometry.TryGetValue(shape.ID, out var before))
                {
                    // New shape appeared after theme application (unlikely)
                    Console.WriteLine($"New shape detected with ID {shape.ID} after applying theme.");
                    geometryChanged = true;
                    continue;
                }

                double afterPinX = shape.XForm.PinX.Value;
                double afterPinY = shape.XForm.PinY.Value;
                double afterWidth = shape.XForm.Width.Value;
                double afterHeight = shape.XForm.Height.Value;

                if (Math.Abs(before.PinX - afterPinX) > tolerance ||
                    Math.Abs(before.PinY - afterPinY) > tolerance ||
                    Math.Abs(before.Width - afterWidth) > tolerance ||
                    Math.Abs(before.Height - afterHeight) > tolerance)
                {
                    Console.WriteLine($"Geometry changed for shape ID {shape.ID}:");
                    Console.WriteLine($"  Before -> PinX:{before.PinX}, PinY:{before.PinY}, Width:{before.Width}, Height:{before.Height}");
                    Console.WriteLine($"  After  -> PinX:{afterPinX}, PinY:{afterPinY}, Width:{afterWidth}, Height:{afterHeight}");
                    geometryChanged = true;
                }
            }

            if (geometryChanged)
            {
                throw new Exception("One or more shapes changed geometry after applying the preset theme.");
            }
            else
            {
                Console.WriteLine("Validation succeeded: No shape geometry changed after applying the preset theme.");
            }

            // Optionally, save the diagram to verify that saving works
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "ThemeApplied.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}