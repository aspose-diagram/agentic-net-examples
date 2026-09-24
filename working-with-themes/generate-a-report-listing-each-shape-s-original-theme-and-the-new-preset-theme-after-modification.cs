using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_modified.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Store shape identification info
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU;

                        // Original theme cannot be read (write‑only), so we note it as unknown
                        string originalTheme = "Unknown";

                        // Apply a new preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // New theme information
                        string newTheme = "Bubble (Variant1)";

                        // Output the report line for this shape
                        Console.WriteLine($"Shape ID: {shapeId}, Name: {shapeName}, Original Theme: {originalTheme}, New Theme: {newTheme}");
                    }
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