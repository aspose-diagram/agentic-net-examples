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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDocxPath> [fontName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fontName = args.Length > 2 ? args[2] : "Arial";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set the font for every character run in the shape.
                        for (int c = 0; c < shape.Chars.Count; c++)
                        {
                            shape.Chars[c].FontName.Value = fontName;
                        }

                        // Set line spacing for every paragraph in the shape.
                        for (int p = 0; p < shape.Paras.Count; p++)
                        {
                            // Example line spacing value (1.5 lines). Adjust as needed.
                            shape.Paras[p].SpLine.Value = 1.5;
                        }
                    }
                }

                // TODO: Save the diagram as DOCX when the appropriate SaveFileFormat is available.
                // Example (when supported):
                // diagram.Save(outputPath, SaveFileFormat.Docx);
                //
                // Since the current API reference does not list a DOCX format, replace the above
                // with the correct format once it becomes available.
                Console.WriteLine("Diagram processing completed. Saving as DOCX is not implemented in this version.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}