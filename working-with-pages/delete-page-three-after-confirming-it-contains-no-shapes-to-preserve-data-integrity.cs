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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least three pages (zero‑based index 2).
            if (diagram.Pages.Count < 3)
            {
                Console.Error.WriteLine("Error: The diagram contains fewer than three pages; nothing to delete.");
                return;
            }

            // Retrieve the third page (index 2).
            Page pageThree = diagram.Pages[2];

            // Confirm the page contains no shapes.
            if (pageThree.Shapes.Count > 0)
            {
                Console.Error.WriteLine("Error: Page three contains shapes and cannot be deleted to preserve data integrity.");
                return;
            }

            // Delete the empty third page.
            // PageCollection supports removal via the Remove method.
            diagram.Pages.Remove(pageThree);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Page three successfully deleted and diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}