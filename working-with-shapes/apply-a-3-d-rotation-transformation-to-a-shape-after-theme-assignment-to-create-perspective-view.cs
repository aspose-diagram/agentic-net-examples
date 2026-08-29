using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (if any)
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Apply a preset theme to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                // Apply 3‑D rotation transformation
                shape.ThreeDFormat.RotationXAngle.Value = 30; // Rotate 30° around X‑axis
                shape.ThreeDFormat.RotationYAngle.Value = 20; // Rotate 20° around Y‑axis
                shape.ThreeDFormat.RotationZAngle.Value = 10; // Rotate 10° around Z‑axis
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.ObliqueFromBottomLeft;
                shape.ThreeDFormat.Perspective.Value = 30; // Perspective depth
                shape.ThreeDFormat.DistanceFromGround.Value = 0;
                shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with 3‑D rotation to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }