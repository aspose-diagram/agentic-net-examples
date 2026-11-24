using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example modification: change the text of the first shape on the first page
                // This demonstrates a modification while preserving the original layout
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Shape shape in firstPage.Shapes)
                    {
                        // Ensure the shape is not marked as deleted
                        if (shape.Del == BOOL.False)
                        {
                            // Clear existing text runs
                            shape.Text.Value.Clear();

                            // Add new text run
                            shape.Text.Value.Add(new Txt("Modified"));
                        }

                        // Only modify the first shape
                        break;
                    }
                }

                // Save the modified diagram back to a Visio file, preserving layout
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}