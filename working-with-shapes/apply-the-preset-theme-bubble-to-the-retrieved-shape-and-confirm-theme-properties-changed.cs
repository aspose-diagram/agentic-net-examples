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
                // Path for the output file after applying the theme
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                    throw new Exception("The page contains no shapes.");

                // Retrieve the first shape (you can change the ID as needed)
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                    throw new Exception("Failed to retrieve the shape with ID 1.");

                // Apply the preset theme "Bubble" to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                // Optionally set a variant and quick style
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                // Since the theme properties are write‑only, we confirm by ensuring no exception was thrown
                Console.WriteLine("Preset theme 'Bubble' applied to shape ID {0}.", shape.ID);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved to '{0}'.", outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }