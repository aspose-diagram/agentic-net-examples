using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page (index 0).
            Page firstPage = diagram.Pages[0];

            // Change the page width to 8.5 inches.
            // Page dimensions are stored in inches via the PageProps object.
            firstPage.PageSheet.PageProps.PageWidth.Value = 8.5;

            // Save the modified diagram back to a file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading, modification, or saving.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}