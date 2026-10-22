using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, page name, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <pageName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string pageName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the page by its textual name
                Page page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.Error.WriteLine($"Page with name \"{pageName}\" not found.");
                    return;
                }

                // Set UIVisibility to hide the page in the UI.
                // The UIVisibility cell expects a UIVisibilityValue; using Hidden to represent false.
                page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page \"{pageName}\" UIVisibility set to hidden and diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}