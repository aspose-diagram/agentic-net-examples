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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that are based on a master shape.
                    if (shape.Master != null)
                    {
                        // The Text.Value collection holds a mix of Txt objects (actual text)
                        // and formatting objects (Char, etc.). We replace the text in each Txt
                        // while leaving formatting objects untouched.
                        for (int i = 0; i < shape.Text.Value.Count; i++)
                        {
                            object item = shape.Text.Value[i];
                            if (item is Txt txt && txt.Text != null)
                            {
                                txt.Text = txt.Text.ToUpperInvariant();
                            }
                        }
                    }
                }
            }

            // Save the modified diagram preserving the original format (VSDX used here).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}