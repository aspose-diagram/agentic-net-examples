using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure the diagram has at least three pages (zero‑based index)
                    if (diagram.Pages.Count < 3)
                    {
                        Console.WriteLine("The diagram does not contain a third page.");
                        return;
                    }

                    // Retrieve the third page (page index 2)
                    Page page = diagram.Pages[2];

                    // Rotation angle of 90 degrees expressed in radians
                    double rotationRadians = Math.PI / 2.0;

                    // Iterate over all shapes on the third page and set their rotation
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set the shape's rotation angle (radians)
                        shape.XForm.Angle.Value = rotationRadians;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }