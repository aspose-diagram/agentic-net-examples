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

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the solid fill color to replace background images
                const string solidColorHex = "#FFFFFF"; // white; change to any hex color

                // Iterate through all pages (including background pages)
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Apply solid fill
                            shape.Fill.FillPattern.Value = 1; // solid fill
                            shape.Fill.FillForegnd.Value = solidColorHex;

                            // Remove outline by setting line pattern to none and weight to zero
                            shape.Line.LinePattern.Value = (LinePatternValue)0; // no line pattern
                            shape.Line.LineWeight.Value = 0; // zero thickness
                        }
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