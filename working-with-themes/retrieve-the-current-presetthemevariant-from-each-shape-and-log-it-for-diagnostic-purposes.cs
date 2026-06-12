using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape ID and Name for identification
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU;

                        // PresetThemeVariant is write‑only; it cannot be read back.
                        // Log that the value is not retrievable.
                        Console.WriteLine($"Shape ID: {shapeId}, Name: {shapeName} - PresetThemeVariant: <unavailable (write‑only)>");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }