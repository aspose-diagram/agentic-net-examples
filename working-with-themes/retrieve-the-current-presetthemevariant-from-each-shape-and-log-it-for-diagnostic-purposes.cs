using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // PresetThemeVariant is write‑only; it cannot be read back.
                        // Log the shape ID and indicate that the variant cannot be retrieved.
                        Console.WriteLine($"Shape ID: {shape.ID} - PresetThemeVariant is write‑only and cannot be read.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }