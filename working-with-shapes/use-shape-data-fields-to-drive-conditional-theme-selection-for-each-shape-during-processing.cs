using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the custom data field (Data1) – it is a plain string
                        string dataValue = shape.Data1 ?? string.Empty;

                        // Apply a preset theme based on the Data1 value
                        // Example mapping:
                        //   "Red"   -> Theme Variant1
                        //   "Blue"  -> Theme Variant2
                        //   "Green" -> Theme Variant3
                        //   any other value -> no theme change
                        switch (dataValue.Trim().ToUpperInvariant())
                        {
                            case "RED":
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                break;
                            case "BLUE":
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                                break;
                            case "GREEN":
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                                break;
                            default:
                                // No theme change for unrecognized values
                                break;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }