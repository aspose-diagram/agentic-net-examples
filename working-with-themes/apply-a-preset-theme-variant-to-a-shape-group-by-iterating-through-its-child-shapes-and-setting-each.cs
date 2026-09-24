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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Find group shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is a group
                        if (shape.Type == TypeValue.Group)
                        {
                            // Apply theme variant to each child shape within the group
                            foreach (Shape child in shape.Shapes)
                            {
                                // Set the preset theme (required before setting variant)
                                child.PresetTheme = PresetThemeValue.Bubble;
                                // Apply a specific theme variant
                                child.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }