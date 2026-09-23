using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after kerning adjustments
                string outputPath = "output_kerning.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target font name for which kerning will be adjusted
                string targetFontName = "Calibri";

                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Iterate over each character formatting entry
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Check if the character uses the target font
                                if (ch.FontName != null && ch.FontName.Value == targetFontName)
                                {
                                    // NOTE: Aspose.Diagram does not expose a direct Kerning property.
                                    // If a kerning cell were available, it could be set here, e.g.:
                                    // ch.Kerning.Value = desiredKerningValue;
                                    // Since such a property is not defined, this placeholder demonstrates where
                                    // kerning adjustments would be applied.

                                    // Example placeholder: set a custom style flag (replace with actual kerning logic if available)
                                    // ch.Style.Value |= StyleValue.Bold; // This line is just illustrative.
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Kerning adjustments (if any) have been applied and the diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }