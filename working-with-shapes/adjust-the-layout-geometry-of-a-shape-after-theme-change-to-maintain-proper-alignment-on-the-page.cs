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
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure there is at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        throw new Exception("The diagram contains no pages.");
                    }

                    // Get the first page
                    Page page = diagram.Pages[0];

                    // Find the target shape (example: shape with universal name "Rectangle")
                    Shape targetShape = null;
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        throw new Exception("Target shape not found on the page.");
                    }

                    // Apply a preset theme to the shape
                    targetShape.PresetTheme = PresetThemeValue.Bubble;
                    targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    // After theme change, re-align the shape to the center of the page
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    double shapeWidth = targetShape.XForm.Width.Value;
                    double shapeHeight = targetShape.XForm.Height.Value;

                    // Set PinX and PinY so the shape is centered
                    targetShape.XForm.PinX.Value = pageWidth / 2.0;
                    targetShape.XForm.PinY.Value = pageHeight / 2.0;

                    // Optionally, preserve original size (if theme altered it)
                    targetShape.XForm.Width.Value = shapeWidth;
                    targetShape.XForm.Height.Value = shapeHeight;

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processed and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }