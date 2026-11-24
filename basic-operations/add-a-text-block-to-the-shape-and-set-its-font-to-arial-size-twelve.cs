using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the output file path (saved alongside the input file).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_with_text.vsdx");

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Clear any existing text in the shape.
                shape.Text.Value.Clear();

                // Add a new text block to the shape.
                shape.Text.Value.Add(new Txt("Sample Text"));

                // Verify that a character run was created.
                if (shape.Chars.Count > 0)
                {
                    // Set the font name to Arial.
                    shape.Chars[0].FontName.Value = "Arial";

                    // Set the font size to 12 points (converted to inches: points / 72).
                    shape.Chars[0].Size.Value = 12.0 / 72.0;
                }
                else
                {
                    Console.Error.WriteLine("No character runs were created after adding text.");
                }

                // Save the modified diagram to the output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}