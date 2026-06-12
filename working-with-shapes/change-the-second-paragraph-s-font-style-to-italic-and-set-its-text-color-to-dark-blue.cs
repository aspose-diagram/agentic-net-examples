using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Ensure the shape has at least two paragraphs
                        if (shape.Paras != null && shape.Paras.Count > 1)
                        {
                            // Iterate over character runs (Char collection)
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Paragraph index is zero‑based; 1 corresponds to the second paragraph
                                if (ch.IX == 1)
                                {
                                    // Apply italic style while preserving existing styles
                                    ch.Style.Value |= StyleValue.Italic;

                                    // Set text color to dark blue (hex #00008B)
                                    ch.Color.Value = "#00008B";
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }