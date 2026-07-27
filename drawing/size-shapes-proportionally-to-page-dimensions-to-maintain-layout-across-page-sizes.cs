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
                string outputPath = "output_scaled.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Reference page size (Letter size in inches)
                    const double referenceWidth = 8.5;
                    const double referenceHeight = 11.0;

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve current page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Compute scaling factors relative to the reference size
                        double scaleX = pageWidth / referenceWidth;
                        double scaleY = pageHeight / referenceHeight;

                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Scale position (PinX, PinY)
                            shape.XForm.PinX.Value *= scaleX;
                            shape.XForm.PinY.Value *= scaleY;

                            // Scale size (Width, Height)
                            shape.XForm.Width.Value *= scaleX;
                            shape.XForm.Height.Value *= scaleY;
                        }
                    }

                    // Save the modified diagram back to Visio format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram scaling completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }