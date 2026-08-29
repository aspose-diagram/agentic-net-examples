using System;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Attempt to read the PresetThemeVariant via reflection
                        PropertyInfo variantProp = shape.GetType().GetProperty("PresetThemeVariant");
                        string variantInfo;

                        if (variantProp != null && variantProp.CanRead)
                        {
                            // If the property were readable, get its value
                            object value = variantProp.GetValue(shape);
                            variantInfo = value?.ToString() ?? "null";
                        }
                        else
                        {
                            // Property is write‑only; cannot retrieve the value directly
                            variantInfo = "Write‑only (cannot read)";
                        }

                        // Log diagnostic information
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, PresetThemeVariant: {variantInfo}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }