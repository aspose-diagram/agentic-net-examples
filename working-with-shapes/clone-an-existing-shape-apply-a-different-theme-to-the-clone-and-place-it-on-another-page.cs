using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages; add a new page if necessary.
                if (diagram.Pages.Count < 2)
                {
                    // Create a new blank page and add it to the diagram.
                    Page newPage = new Page();
                    diagram.Pages.Add(newPage);
                }

                // Get the source page (first page) and the first shape on that page.
                Page sourcePage = diagram.Pages[0];
                if (sourcePage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("No shapes found on the source page.");
                    return;
                }

                Shape originalShape = sourcePage.Shapes[0];

                // Create a new shape instance and copy the original shape's data.
                Shape clonedShape = new Shape();
                clonedShape.Copy(originalShape);

                // Apply a different preset theme to the cloned shape.
                clonedShape.PresetTheme = PresetThemeValue.Bubble;
                clonedShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Optionally adjust the position of the cloned shape to avoid overlap.
                clonedShape.XForm.PinX.Value += 2.0;
                clonedShape.XForm.PinY.Value += 2.0;

                // Place the cloned shape on the second page.
                Page targetPage = diagram.Pages[1];
                targetPage.Shapes.Add(clonedShape);

                // Save the modified diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}