using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare timestamp string
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Iterate through all pages and shapes to prepend timestamp to existing text
            foreach (Page page in diagram.Pages)
            {
                // Iterate over shapes collection
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Retrieve existing text; handle possible nulls safely
                    string existingText = shape.Text?.Value?.ToString() ?? string.Empty;

                    // Build new text with timestamp
                    string newText = $"{timestamp} {existingText}";

                    // Clear current text collection
                    shape.Text.Value.Clear();

                    // Add the new text as a Txt object
                    shape.Text.Value.Add(new Txt(newText));
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the save format (required by some versions)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the updated diagram as PDF
            diagram.Save(outputPdfPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}