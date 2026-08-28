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

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define dark gray color in HEX
                const string darkGrayHex = "#A9A9A9";

                // Convert 1.5 points to inches (1 point = 1/72 inch)
                double lineWeightInInches = 1.5 / 72.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Set line color to dark gray
                            shape.Line.LineColor.Value = darkGrayHex;

                            // Set line thickness to 1.5 points (in inches)
                            shape.Line.LineWeight.Value = lineWeightInInches;
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