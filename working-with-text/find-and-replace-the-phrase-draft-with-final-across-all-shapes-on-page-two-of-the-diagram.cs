using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Define output path with a suffix
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram has at least two pages (zero‑based index)
                if (diagram.Pages.Count <= 1)
                {
                    Console.Error.WriteLine("Error: Diagram does not contain a second page.");
                    return;
                }

                // Access the second page (index 1)
                Page page = diagram.Pages[1];

                // Iterate over all shapes on the second page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text property with content
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Iterate over each text segment within the shape
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast the segment to Txt to access the actual text string
                        Txt txt = fmt as Txt;
                        if (txt != null && txt.Text != null && txt.Text.Contains("Draft"))
                        {
                            // Replace 'Draft' with 'Final' in the text
                            txt.Text = txt.Text.Replace("Draft", "Final");
                        }
                    }
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}