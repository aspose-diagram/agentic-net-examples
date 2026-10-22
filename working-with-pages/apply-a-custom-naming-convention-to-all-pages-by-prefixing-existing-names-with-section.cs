using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and rename them with the required prefix.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Explicitly type the page variable.
                    Page page = diagram.Pages[i];

                    // Prefix the existing page name with "Section_".
                    // Ensure we do not create duplicate prefixes if the program is run multiple times.
                    if (!page.Name.StartsWith("Section_"))
                    {
                        page.Name = "Section_" + page.Name;
                    }
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram pages renamed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}