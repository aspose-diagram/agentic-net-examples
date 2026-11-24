using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            try
            {
                // Attempt to assign an undefined orientation value.
                // This should throw an ArgumentException according to the API contract.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
                Console.WriteLine("Orientation assigned successfully (unexpected).");
            }
            catch (ArgumentException ex)
            {
                // Log the expected error.
                Console.Error.WriteLine($"ArgumentException caught while setting PrintPageOrientation: {ex.Message}");
            }

            // Save the diagram (optional, demonstrates a valid save operation).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any unexpected errors from Aspose operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}