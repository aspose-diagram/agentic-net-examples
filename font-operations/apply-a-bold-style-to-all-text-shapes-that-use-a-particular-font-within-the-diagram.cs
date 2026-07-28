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

                // Font name to target (case‑insensitive comparison)
                string targetFontName = "Calibri";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

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

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }