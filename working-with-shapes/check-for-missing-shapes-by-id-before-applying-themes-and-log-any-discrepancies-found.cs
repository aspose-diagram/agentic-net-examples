using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Assume we are working with the first page
                Page page = diagram.Pages[0];

                // List of shape IDs that are expected to exist before applying a theme
                long[] expectedShapeIds = { 1, 2, 5, 10 };

                bool anyMissing = false;

                foreach (long shapeId in expectedShapeIds)
                {
                    Shape shape = null;
                    try
                    {
                        // Retrieve the shape by ID; GetShape returns null if not found
                        shape = page.Shapes.GetShape(shapeId);
                    }
                    catch (Exception ex)
                    {
                        // In case GetShape throws an exception for an invalid ID
                        Console.WriteLine($"Error retrieving shape ID {shapeId}: {ex.Message}");
                    }

                    if (shape == null)
                    {
                        anyMissing = true;
                        Console.WriteLine($"Missing shape with ID: {shapeId}");
                    }
                }

                if (!anyMissing)
                {
                    Console.WriteLine("All expected shapes are present. Applying theme...");
                    // Apply a preset theme to the page
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }
                else
                {
                    Console.WriteLine("Theme application skipped due to missing shapes.");
                }

                // Save the diagram (if needed)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }