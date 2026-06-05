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

                // Font name to target (case-sensitive as stored in the diagram)
                string targetFont = "Arial";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Iterate through each character formatting run in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Check if the character's font matches the target font
                                if (ch.FontName != null && ch.FontName.Value == targetFont)
                                {
                                    // Apply bold style while preserving existing styles
                                    ch.Style.Value |= StyleValue.Bold;
                                }
                            }
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