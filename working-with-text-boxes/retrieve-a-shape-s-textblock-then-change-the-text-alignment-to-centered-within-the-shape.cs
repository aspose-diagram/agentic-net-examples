using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default suffix)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_centered.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // ----- Modify vertical alignment to middle -----
                    // TextBlock.VerticalAlign controls vertical alignment of the text block
                    shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                    // ----- Modify horizontal alignment to center for each paragraph -----
                    // Horizontal alignment is set per paragraph via HorzAlign
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                    }

                    // Refresh shape data after modifications
                    shape.RefreshData();
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with centered text alignment to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}