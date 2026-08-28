using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_scaled.vsdx";
                // Scaling factor to apply (e.g., 1.5 for 150% scaling)
                double scalingFactor = 1.5;

                // Load the diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Select the page to modify – here we use the first page
                    if (diagram.Pages.Count == 0)
                    {
                        Console.WriteLine("The diagram contains no pages.");
                        return;
                    }

                    Page page = diagram.Pages[0];

                    // Iterate over all shapes on the selected page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Scale position (PinX, PinY)
                        shape.XForm.PinX.Value *= scalingFactor;
                        shape.XForm.PinY.Value *= scalingFactor;

                        // Scale size (Width, Height)
                        shape.XForm.Width.Value *= scalingFactor;
                        shape.XForm.Height.Value *= scalingFactor;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}' with scaling factor {scalingFactor} applied.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }