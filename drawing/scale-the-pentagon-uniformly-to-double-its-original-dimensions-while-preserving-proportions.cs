using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file containing a pentagon shape.
                string inputPath = "input.vsdx";
                // Path where the scaled diagram will be saved.
                string outputPath = "output_scaled.vsdx";

                // Load the existing diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses the "Pentagon" master.
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            // Double the width and height while keeping the center (PinX/PinY) unchanged.
                            shape.XForm.Width.Value *= 2;
                            shape.XForm.Height.Value *= 2;
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }