using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine output path (original name with "_modified" suffix).
        string directory = Path.GetDirectoryName(inputPath);
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, filenameWithoutExt + "_modified.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        bool shapeModified = false;

                        // Iterate through each text fragment (FormatTxt) in the shape's text.
                        foreach (FormatTxt fmt in shape.Text.Value)
                        {
                            // Only process Txt objects (plain text runs).
                            Txt txt = fmt as Txt;
                            if (txt != null && txt.Text != null && txt.Text.Contains("TODO"))
                            {
                                // Replace all occurrences of "TODO" with an empty string.
                                txt.Text = txt.Text.Replace("TODO", string.Empty);
                                shapeModified = true;
                            }
                        }

                        // If any replacement occurred, log the shape ID.
                        if (shapeModified)
                        {
                            Console.WriteLine($"Modified shape ID: {shape.ID}");
                        }
                    }
                }

                // Save the modified diagram to the output file using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}