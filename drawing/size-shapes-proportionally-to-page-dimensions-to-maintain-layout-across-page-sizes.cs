using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define the target page size (in inches). Adjust as needed.
                    double targetPageWidth = 11.0;   // Example: landscape width
                    double targetPageHeight = 8.5;   // Example: landscape height

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Original page dimensions
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate scaling factors for X and Y axes
                        double scaleX = targetPageWidth / originalWidth;
                        double scaleY = targetPageHeight / originalHeight;

                        // Apply the new page dimensions
                        page.PageSheet.PageProps.PageWidth.Value = targetPageWidth;
                        page.PageSheet.PageProps.PageHeight.Value = targetPageHeight;

                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Scale position (PinX, PinY)
                            shape.XForm.PinX.Value = shape.XForm.PinX.Value * scaleX;
                            shape.XForm.PinY.Value = shape.XForm.PinY.Value * scaleY;

                            // Scale size (Width, Height)
                            shape.XForm.Width.Value = shape.XForm.Width.Value * scaleX;
                            shape.XForm.Height.Value = shape.XForm.Height.Value * scaleY;
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