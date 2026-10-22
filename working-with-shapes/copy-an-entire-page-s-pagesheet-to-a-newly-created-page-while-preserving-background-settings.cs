using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Choose the source page to copy. Here we use the first page (index 0).
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages to copy.");
                    return;
                }

                // Retrieve the source page.
                Page sourcePage = diagram.Pages[0];

                // Create a new page instance.
                Page newPage = new Page();

                // Copy the entire source page (including its PageSheet) into the new page.
                newPage.Copy(sourcePage);

                // Preserve background settings explicitly.
                newPage.Background = sourcePage.Background; // BOOL enum
                newPage.BackPage = sourcePage.BackPage;     // preserve linked background page

                // Add the new page to the diagram's page collection.
                diagram.Pages.Add(newPage);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Page copied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}