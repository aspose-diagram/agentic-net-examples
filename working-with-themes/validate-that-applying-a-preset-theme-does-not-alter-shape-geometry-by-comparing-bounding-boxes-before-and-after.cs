using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Simple struct to hold geometry data of a shape
    struct ShapeGeometry
    {
        public double PinX;
        public double PinY;
        public double Width;
        public double Height;
        public double Angle; // Radians

        public ShapeGeometry(double pinX, double pinY, double width, double height, double angle)
        {
            PinX = pinX;
            PinY = pinY;
            Width = width;
            Height = height;
            Angle = angle;
        }

        // Compare two geometry structs; returns true if all values are equal within a small tolerance
        public bool Equals(ShapeGeometry other, double tolerance = 1e-6)
        {
            return Math.Abs(PinX - other.PinX) < tolerance &&
                   Math.Abs(PinY - other.PinY) < tolerance &&
                   Math.Abs(Width - other.Width) < tolerance &&
                   Math.Abs(Height - other.Height) < tolerance &&
                   Math.Abs(Angle - other.Angle) < tolerance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (provide via command line or use a default placeholder)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for this validation
                Page page = diagram.Pages[0];

                // Store original geometry for each shape (keyed by shape ID)
                Dictionary<long, ShapeGeometry> originalGeometries = new Dictionary<long, ShapeGeometry>();

                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double angle = shape.XForm.Angle.Value; // Radians

                    originalGeometries[shape.ID] = new ShapeGeometry(pinX, pinY, width, height, angle);
                }

                // Apply a preset theme to the page (theme should not affect geometry)
                page.PresetTheme = PresetThemeValue.Bubble;
                // Optionally set a variant; not required for geometry check
                // page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // After applying the theme, compare geometry
                bool anyMismatch = false;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (!originalGeometries.TryGetValue(shape.ID, out ShapeGeometry before))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} was not captured before theme application.");
                        continue;
                    }

                    ShapeGeometry after = new ShapeGeometry(
                        shape.XForm.PinX.Value,
                        shape.XForm.PinY.Value,
                        shape.XForm.Width.Value,
                        shape.XForm.Height.Value,
                        shape.XForm.Angle.Value);

                    if (!before.Equals(after))
                    {
                        anyMismatch = true;
                        Console.WriteLine($"Geometry changed for Shape ID {shape.ID}:");
                        Console.WriteLine($"  Before -> PinX:{before.PinX}, PinY:{before.PinY}, Width:{before.Width}, Height:{before.Height}, Angle:{before.Angle}");
                        Console.WriteLine($"  After  -> PinX:{after.PinX}, PinY:{after.PinY}, Width:{after.Width}, Height:{after.Height}, Angle:{after.Angle}");
                    }
                }

                if (anyMismatch)
                {
                    throw new Exception("One or more shapes changed geometry after applying the preset theme.");
                }
                else
                {
                    Console.WriteLine("All shapes retained their geometry after applying the preset theme.");
                }

                // Optionally save the diagram to verify the theme was applied
                string outputPath = "output_with_theme.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }