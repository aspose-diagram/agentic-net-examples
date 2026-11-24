using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Generate timestamp prefix
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE (foreign) shapes by checking ForeignData
                    if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                    {
                        // Retrieve existing display text (if any)
                        string existingText = string.Concat(shape.Text.Value.Select(t => t.Value));

                        // Build new text with timestamp prefix
                        string newText = $"{timestamp} {existingText}".Trim();

                        // Clear existing text and set new text
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(newText));
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}