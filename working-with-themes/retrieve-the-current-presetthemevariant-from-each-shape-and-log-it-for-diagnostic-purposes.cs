using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape.PresetThemeVariant is write‑only; it cannot be read.
                        // For diagnostic purposes we log the shape identifier and note that the variant cannot be retrieved.
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU} - PresetThemeVariant is write‑only and cannot be read.");
                    }
                }

                // Optional: Save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }