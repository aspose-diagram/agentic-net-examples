using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and page ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <pageId> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string pageIdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "modified_output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the page ID.
        if (!int.TryParse(pageIdArg, out int pageId))
        {
            Console.Error.WriteLine($"Error: Page ID \"{pageIdArg}\" is not a valid integer.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the page by its numeric ID.
                Page page = diagram.Pages.GetPage(pageId);
                if (page == null)
                {
                    Console.Error.WriteLine($"Error: No page found with ID {pageId}.");
                    return;
                }

                // Store the reference for later modifications.
                // Example modification: change the page name.
                page.Name = "ModifiedPage";

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page ID {pageId} retrieved and modified. Diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}