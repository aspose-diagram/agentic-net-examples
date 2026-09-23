using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the scaled output file
                string outputPath = "output_scaled.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the base page size (in inches) that the original layout was designed for
                const double basePageWidth = 8.5;   // e.g., Letter width
                const double basePageHeight = 11.0; // e.g., Letter height

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Current page dimensions
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compute scaling factors relative to the base size
                    double scaleX = currentWidth / basePageWidth;
                    double scaleY = currentHeight / basePageHeight;

                    // Scale each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Scale position
                        shape.XForm.PinX.Value *= scaleX;
                        shape.XForm.PinY.Value *= scaleY;

                        // Scale size
                        shape.XForm.Width.Value *= scaleX;
                        shape.XForm.Height.Value *= scaleY;
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