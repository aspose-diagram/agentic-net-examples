using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Apply a preset theme to the first page to ensure consistent styling
                Page page = diagram.Pages[0];
                page.PresetTheme = PresetThemeValue.Bubble; // Example theme

                // Find the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Del == BOOL.False)
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found on the page.");
                    return;
                }

                // Configure shadow effect for the selected shape
                // Enable a simple shadow
                targetShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                // Set shadow color (black)
                targetShape.Fill.ShdwForegnd.Value = "#000000";
                // Set shadow transparency (30% transparent)
                targetShape.Fill.ShdwForegndTrans.Value = 0.3;
                // Set shadow offset (horizontal and vertical)
                targetShape.Fill.ShapeShdwOffsetX.Value = 0.1; // inches
                targetShape.Fill.ShapeShdwOffsetY.Value = 0.1; // inches

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with updated shadow effect.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }