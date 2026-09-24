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
                string outputPath = "output_fixed.vsdx";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Default fill color (hex string)
                    const string defaultFillColor = "#FFCC00"; // amber

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the shape has a fill foreground color defined
                            string fillColor = shape.Fill.FillForegnd.Value;

                            // If the fill color is null, empty, or whitespace, assign the default color
                            if (string.IsNullOrWhiteSpace(fillColor))
                            {
                                // Ensure the fill pattern is solid (1)
                                shape.Fill.FillPattern.Value = 1;
                                // Assign the default fill foreground color
                                shape.Fill.FillForegnd.Value = defaultFillColor;
                            }
                        }
                    }

                    // Save the corrected diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram processed and saved to: " + outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }