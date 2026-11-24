using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, page ID, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <pageId> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string pageIdArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the page ID
        if (!int.TryParse(pageIdArg, out int pageId))
        {
            Console.Error.WriteLine($"Error: Invalid page ID '{pageIdArg}'. It must be an integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the page by its numeric ID
                Page targetPage = diagram.Pages.GetPage(pageId);
                if (targetPage == null)
                {
                    Console.Error.WriteLine($"Error: No page found with ID {pageId}.");
                    return;
                }

                // Store the reference for later modifications (example: rename the page)
                targetPage.Name = "ModifiedPage";

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page ID {pageId} retrieved and modified. Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}