using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Original theme cannot be read (write‑only), so we record it as unknown
                        string originalTheme = "Unknown";

                        // Apply a new preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Log the change
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, Original Theme: {originalTheme}, New Theme: Bubble Variant1");
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