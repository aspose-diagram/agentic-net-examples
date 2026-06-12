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

                // Conversion factor: 2 points = 2/72 inches
                double incrementInInches = 2.0 / 72.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify title shapes by their universal name (case‑insensitive)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                        {
                            // Increase font size for each character run in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Ensure the size cell has a value before modifying
                                if (ch.Size != null && ch.Size.Value != null)
                                {
                                    ch.Size.Value += incrementInInches;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram (preserving the original format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }