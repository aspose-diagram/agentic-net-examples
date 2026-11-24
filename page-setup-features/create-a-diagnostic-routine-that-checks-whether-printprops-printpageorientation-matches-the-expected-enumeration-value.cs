using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and expected orientation string.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramFilePath> <expectedOrientation>");
            Console.Error.WriteLine("expectedOrientation values: Landscape, Portrait, SameAsPrinter");
            return;
        }

        string diagramPath = args[0];
        string expectedOrientationStr = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the expected orientation string to the enum.
        if (!Enum.TryParse<PrintPageOrientationValue>(expectedOrientationStr, true, out var expectedOrientation))
        {
            Console.Error.WriteLine($"Error: Invalid orientation value '{expectedOrientationStr}'.");
            Console.Error.WriteLine("Valid values are: Landscape, Portrait, SameAsPrinter");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (or you could iterate all pages as needed).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the current print page orientation.
            PrintPageOrientationValue actualOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

            // Compare with the expected value and output the result.
            if (actualOrientation == expectedOrientation)
            {
                Console.WriteLine($"Success: PrintPageOrientation is '{actualOrientation}' as expected.");
            }
            else
            {
                Console.WriteLine($"Mismatch: PrintPageOrientation is '{actualOrientation}', expected '{expectedOrientation}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }
}