using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Font to target and desired kerning (letterspacing) value
                string targetFontName = "Calibri";
                double desiredLetterspace = 0.5; // value in points (adjust as needed)

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == Aspose.Diagram.BOOL.True)
                            continue;

                        // Ensure the shape contains text
                        if (shape.Text == null)
                            continue;

                        // Iterate through character formatting runs
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Check if the character run uses the target font
                            if (ch.FontName != null && ch.FontName.Value == targetFontName)
                            {
                                // Apply the kerning (letterspacing) adjustment
                                ch.Letterspace.Value = desiredLetterspace;
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