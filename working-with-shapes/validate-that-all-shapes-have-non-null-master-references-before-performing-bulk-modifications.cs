using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate that every shape has a non‑null Master reference
                bool allMastersValid = true;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null)
                        {
                            allMastersValid = false;
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has a null Master reference.");
                        }
                    }
                }

                // Abort if any shape is missing a Master
                if (!allMastersValid)
                {
                    throw new Exception("Validation failed: one or more shapes have null Master references.");
                }

                // ----- Bulk modifications can be performed safely after validation -----
                // Example modification: set the foreground fill color of every shape to red
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        shape.Fill.FillForegnd.Value = "#FF0000";
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