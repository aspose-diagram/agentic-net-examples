using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // Font name to target (case-insensitive)
                string targetFontName = "Calibri";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Iterate through character formatting runs
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Check if the character run uses the target font
                                if (string.Equals(ch.FontName.Value, targetFontName, StringComparison.OrdinalIgnoreCase))
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