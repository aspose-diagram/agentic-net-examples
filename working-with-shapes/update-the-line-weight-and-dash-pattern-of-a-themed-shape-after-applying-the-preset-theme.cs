using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify (for example, the first shape on the page)
                // Ensure the shape collection is not empty
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Apply a preset theme to the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Update line weight (in inches) and dash pattern
                shape.Line.LineWeight.Value = 0.02;                     // 0.02 inches thick
                shape.Line.LinePattern.Value = LinePatternValue.Dash; // Dashed line

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }