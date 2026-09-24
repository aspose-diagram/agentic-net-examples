using System.IO;
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
            Diagram diagram = new Diagram("input.vsdx");

            // Apply a preset theme to the first page (optional, can also be applied to individual shapes)
            Page page = diagram.Pages[0];
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found.");
                return;
            }

            // Assign the same theme to the shape (optional, demonstrates theme after assignment)
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Apply 3‑D rotation to create a perspective view
            // X‑axis rotation
            targetShape.ThreeDFormat.RotationXAngle.Value = 30; // degrees
            // Y‑axis rotation
            targetShape.ThreeDFormat.RotationYAngle.Value = 15; // degrees
            // Z‑axis rotation
            targetShape.ThreeDFormat.RotationZAngle.Value = 0; // degrees

            // Set rotation type (oblique from bottom left gives a perspective effect)
            targetShape.ThreeDFormat.RotationType.Value = RotationTypeValue.ObliqueFromBottomLeft;

            // Keep text flat while rotating the shape
            targetShape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with 3‑D rotation applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
