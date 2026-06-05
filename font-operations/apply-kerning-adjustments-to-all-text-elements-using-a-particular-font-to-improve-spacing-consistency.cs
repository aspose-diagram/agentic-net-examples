using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target font name for which kerning will be adjusted
                string targetFontName = "Calibri";
                // Desired kerning value (Letterspace) in 1/20th point increments.
                // 0 means default spacing; positive values increase spacing, negative decrease.
                double desiredLetterspace = 0;

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
                                // Check if the character run uses the target font
                                if (ch.FontName != null && ch.FontName.Value == targetFontName)
                                {
                                    // Apply the kerning adjustment by setting Letterspace
                                    ch.Letterspace.Value = desiredLetterspace;
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