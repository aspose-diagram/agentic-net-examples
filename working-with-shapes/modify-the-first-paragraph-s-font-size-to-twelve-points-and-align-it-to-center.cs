using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool paragraphModified = false;

                // Iterate pages and shapes to find the first shape that contains at least one paragraph.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is not deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check that the shape has paragraphs.
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Access the first paragraph.
                            Para para = shape.Paras[0];

                            // Set horizontal alignment to center.
                            // The correct enum member for center alignment is HorzAlignValue.Center.
                            para.HorzAlign.Value = HorzAlignValue.Center;

                            // Convert 12 points to inches (1 point = 1/72 inch).
                            double sizeInInches = 12.0 / 72.0;

                            // Apply the font size to all characters of the shape.
                            if (shape.Chars != null)
                            {
                                for (int i = 0; i < shape.Chars.Count; i++)
                                {
                                    shape.Chars[i].Size.Value = sizeInInches;
                                }
                            }

                            paragraphModified = true;
                            // Break out after modifying the first paragraph.
                            break;
                        }
                    }

                    if (paragraphModified)
                        break;
                }

                if (!paragraphModified)
                {
                    Console.Error.WriteLine("No paragraph found in the diagram to modify.");
                    return;
                }

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}