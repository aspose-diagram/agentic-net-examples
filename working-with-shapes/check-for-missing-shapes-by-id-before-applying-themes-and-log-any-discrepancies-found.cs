using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after applying the theme
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the list of shape IDs that are expected to exist
                long[] expectedShapeIds = new long[] { 1, 2, 3, 10, 20 };

                // Check each expected ID across all pages
                foreach (long shapeId in expectedShapeIds)
                {
                    bool found = false;

                    foreach (Page page in diagram.Pages)
                    {
                        // Attempt to retrieve the shape by ID
                        Shape shape = null;
                        try
                        {
                            shape = page.Shapes.GetShape(shapeId);
                        }
                        catch
                        {
                            // GetShape throws if the ID is not present on this page
                            shape = null;
                        }

                        if (shape != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"[Warning] Shape with ID {shapeId} was not found in the diagram.");
                    }
                }

                // Apply a preset theme to each page after verification
                foreach (Page page in diagram.Pages)
                {
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }