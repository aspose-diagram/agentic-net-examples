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
                string outputPath = "output_uppercase.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Each master can contain multiple shapes (the master definition)
                    foreach (Shape shape in master.Shapes)
                    {
                        // Ensure the shape has text content
                        if (shape.Text != null && shape.Text.Value != null)
                        {
                            // Iterate over the text runs (Txt objects) while preserving formatting (Cp objects)
                            foreach (var item in shape.Text.Value)
                            {
                                if (item is Txt txt && !string.IsNullOrEmpty(txt.Text))
                                {
                                    // Convert the text to uppercase, preserving existing formatting
                                    txt.Text = txt.Text.ToUpperInvariant();
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram preserving original formatting and alignment
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }