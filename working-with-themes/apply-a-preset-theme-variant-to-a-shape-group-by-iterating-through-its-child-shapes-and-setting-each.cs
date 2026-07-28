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
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify group shapes
                        if (shape.Type == TypeValue.Group)
                        {
                            // Apply preset theme and variant to the group shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                            // Apply the same theme settings to each child shape within the group
                            foreach (Shape child in shape.Shapes)
                            {
                                child.PresetTheme = PresetThemeValue.Bubble;
                                child.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }