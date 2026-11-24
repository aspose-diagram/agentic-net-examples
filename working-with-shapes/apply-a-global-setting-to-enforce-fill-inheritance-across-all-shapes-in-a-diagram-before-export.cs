using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder, suffix "_InheritFill.vsdx").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_InheritFill.vsdx");

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Fill and FillForegnd objects are not null.
                        if (shape.Fill != null && shape.Fill.FillForegnd != null && shape.Fill.FillForegnd.Ufe != null)
                        {
                            // Enforce fill inheritance for the shape.
                            shape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                        }
                    }
                }

                // Save the modified diagram to the output file using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved with enforced fill inheritance to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}