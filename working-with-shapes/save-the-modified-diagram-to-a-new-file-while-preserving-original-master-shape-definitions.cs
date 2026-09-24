using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – replace with your actual file location.
        string inputPath = "input.vsdx";

        // Verify that the source file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output file path for the modified diagram.
        string outputPath = "output_modified.vsdx";

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count > 0)
            {
                // Access the first page.
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape.
                if (page.Shapes.Count > 0)
                {
                    // Retrieve the first shape on the page.
                    Shape shape = page.Shapes[0];

                    // Clear any existing text runs.
                    shape.Text.Value.Clear();

                    // Add new text to the shape.
                    shape.Text.Value.Add(new Txt("Modified Text"));
                }
                else
                {
                    Console.WriteLine("No shapes found on the first page; nothing to modify.");
                }
            }
            else
            {
                Console.WriteLine("The diagram contains no pages; cannot perform modifications.");
            }

            // Save the modified diagram to a new file.
            // Using SaveFileFormat.Vsdx preserves all master shape definitions.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}