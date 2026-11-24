using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Verify the diagram has at least three pages
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram must contain at least three pages.");
                    return;
                }

                // Retrieve the third page (zero‑based index 2)
                Page thirdPage = diagram.Pages[2];

                // Move the third page to the first position (index 0)
                thirdPage.MoveTo(0);

                // Save the updated diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}