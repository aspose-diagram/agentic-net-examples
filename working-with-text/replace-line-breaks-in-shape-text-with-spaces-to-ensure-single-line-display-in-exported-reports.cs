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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through each text run in the shape.
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Only process actual text runs (Txt objects).
                        if (fmt is Txt txt)
                        {
                            // Replace any line‑break characters with a single space.
                            string original = txt.Text;
                            string replaced = original
                                .Replace("\r\n", " ")
                                .Replace("\r", " ")
                                .Replace("\n", " ");

                            // Update the text only if a change occurred.
                            if (!original.Equals(replaced))
                            {
                                txt.Text = replaced;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}