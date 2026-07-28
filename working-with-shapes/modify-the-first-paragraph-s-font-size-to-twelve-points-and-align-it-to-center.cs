using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool modified = false;

                // Iterate through pages and shapes to locate the first shape containing text
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape has non‑empty text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                        {
                            // ----- Modify the first paragraph -----
                            if (shape.Paras.Count > 0)
                            {
                                // Align the first paragraph to center (use correct enum member)
                                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                            }

                            // ----- Modify the first character (font size) -----
                            if (shape.Chars.Count > 0)
                            {
                                // Set font size to 12 points (12/72 inches)
                                shape.Chars[0].Size.Value = 12.0 / 72.0;
                            }

                            modified = true;
                            break; // Only modify the first shape with text
                        }
                    }

                    if (modified)
                        break;
                }

                // Save the modified diagram using the appropriate SaveFileFormat enum
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}