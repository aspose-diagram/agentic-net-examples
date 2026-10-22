using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Adjust the horizontal scaling factor to 75% (reduce size by 25%).
            // PrintProps is located under PageSheet.PrintProps.
            page.PageSheet.PrintProps.ScaleX.Value = 0.75;

            // Optionally, you could also adjust ScaleY if uniform scaling is desired:
            // page.PageSheet.PrintProps.ScaleY.Value = 0.75;

            // Save the modified diagram back to a Visio format (VSDX).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}