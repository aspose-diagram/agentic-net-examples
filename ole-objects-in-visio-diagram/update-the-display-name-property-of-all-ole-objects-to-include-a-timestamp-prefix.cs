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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a timestamp prefix (e.g., 20231127_154530).
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE objects via the presence of ForeignData with embedded ObjectData.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Retrieve existing display text if any.
                        string existingText = string.Empty;
                        if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Count > 0)
                        {
                            // Use the first Txt element as the current caption.
                            existingText = shape.Text.Value[0].Value;
                        }

                        // Build the new caption with the timestamp prefix.
                        string newCaption = string.IsNullOrEmpty(existingText)
                            ? $"{timestamp}_{shape.NameU}"
                            : $"{timestamp}_{existingText}";

                        // Clear existing text and set the new caption.
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(newCaption));
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}