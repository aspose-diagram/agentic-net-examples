using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool paragraphModified = false;

                // Iterate through pages to find the first shape with a paragraph.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has at least one paragraph.
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Align the first paragraph to center.
                            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                            // Set font size of all characters in the shape to 12 points (12/72 inches).
                            if (shape.Chars != null && shape.Chars.Count > 0)
                            {
                                for (int i = 0; i < shape.Chars.Count; i++)
                                {
                                    Aspose.Diagram.Char ch = shape.Chars[i];
                                    ch.Size.Value = 12.0 / 72.0;
                                }
                            }

                            paragraphModified = true;
                            // Exit loops after processing the first paragraph.
                            break;
                        }
                    }

                    if (paragraphModified)
                        break;
                }

                if (!paragraphModified)
                {
                    Console.Error.WriteLine("No paragraph found in the diagram to modify.");
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}