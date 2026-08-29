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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate that every shape has a non‑null master reference
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null)
                        {
                            // Throw an exception if a shape lacks a master
                            throw new Exception($"Shape ID {shape.ID} on page \"{page.Name}\" has a null Master reference.");
                        }
                    }
                }

                // ----- Bulk modifications can be performed here -----
                // Example placeholder: change fill color of all shapes to light gray
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is not deleted
                        if (shape.Del == BOOL.False)
                        {
                            shape.Fill.FillForegnd.Value = "#D3D3D3";
                        }
                    }
                }
                // ----------------------------------------------------

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }