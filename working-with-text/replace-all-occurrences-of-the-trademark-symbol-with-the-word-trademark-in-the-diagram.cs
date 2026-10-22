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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate over each text element within the shape.
                        foreach (FormatTxt fmt in shape.Text.Value)
                        {
                            // Cast to Txt to access the actual text string.
                            Txt txt = fmt as Txt;
                            if (txt != null && txt.Text != null && txt.Text.Contains("™"))
                            {
                                // Replace the trademark symbol with the word 'Trademark'.
                                txt.Text = txt.Text.Replace("™", "Trademark");
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}