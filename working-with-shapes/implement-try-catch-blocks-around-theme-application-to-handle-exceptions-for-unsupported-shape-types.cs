using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply a preset theme to each page and its shapes
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Apply theme to the page
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to apply theme to page '{page.Name}': {ex.Message}");
                    }

                    // Iterate through shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        try
                        {
                            // Attempt to apply theme to the shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                        catch (Exception ex)
                        {
                            // Handle shapes that do not support theme application
                            Console.WriteLine($"Shape ID {shape.ID} ('{shape.Name}') does not support theme: {ex.Message}");
                        }
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