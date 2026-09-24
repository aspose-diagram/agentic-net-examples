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

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram using the standard constructor
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page of the diagram
                Page page = diagram.Pages[0];

                // Apply a preset theme to the page to change its visual style
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Retrieve the page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Locate a shape that needs geometry adjustment (example: first Rectangle master)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape != null)
                {
                    // Center the shape on the page after the theme change
                    targetShape.XForm.PinX.Value = pageWidth / 2.0;
                    targetShape.XForm.PinY.Value = pageHeight / 2.0;

                    // Optionally, keep the original size (no changes needed for width/height)
                }
                else
                {
                    Console.WriteLine("Target shape not found.");
                }

                // Save the modified diagram using a valid overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
