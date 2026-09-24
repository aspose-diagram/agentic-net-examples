using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Size threshold in inches (shapes larger than this will have gluing disabled)
                double sizeThreshold = 2.0;

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve shape width and height (in inches)
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Check if the shape exceeds the defined size threshold
                            if (width > sizeThreshold || height > sizeThreshold)
                            {
                                // Disable dynamic gluing for this shape
                                shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed. Modified diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }