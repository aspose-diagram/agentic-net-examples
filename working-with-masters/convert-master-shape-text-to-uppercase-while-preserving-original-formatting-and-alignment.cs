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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
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
                    // Process only shapes that are based on a master shape.
                    if (shape.Master != null)
                    {
                        // Ensure the shape has a Text collection.
                        if (shape.Text != null && shape.Text.Value != null)
                        {
                            // Iterate over the text elements (Txt objects) in the shape.
                            for (int i = 0; i < shape.Text.Value.Count; i++)
                            {
                                object item = shape.Text.Value[i];
                                if (item is Txt txt && txt.Text != null)
                                {
                                    // Convert the text to uppercase while leaving formatting untouched.
                                    txt.Text = txt.Text.ToUpperInvariant();
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}