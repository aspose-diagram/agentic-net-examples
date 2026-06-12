using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we are working with the first page
                Page page = diagram.Pages[0];

                // List of shape IDs that are expected to exist before applying themes
                long[] expectedShapeIds = new long[] { 1, 2, 3, 4, 5 };

                // Track whether any missing shapes were found
                bool missingFound = false;

                foreach (long shapeId in expectedShapeIds)
                {
                    try
                    {
                        // Attempt to retrieve the shape by ID
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // If retrieval succeeds, apply a preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }
                    catch (Exception ex)
                    {
                        // If an exception occurs, the shape ID is missing
                        missingFound = true;
                        Console.WriteLine($"Missing shape with ID {shapeId}: {ex.Message}");
                    }
                }

                if (missingFound)
                {
                    Console.WriteLine("One or more shapes were missing. Theme applied only to existing shapes.");
                }
                else
                {
                    Console.WriteLine("All expected shapes were found. Theme applied to all shapes.");
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }