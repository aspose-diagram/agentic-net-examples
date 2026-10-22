using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <expectedOrientation>");
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

            bool allMatch = true;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the current page orientation.
                PrintPageOrientationValue actualOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                // Compare with the expected orientation.
                if (actualOrientation == expectedOrientation)
                {
                    Console.WriteLine($"Page '{page.Name}' (ID: {page.ID}) orientation matches expected value: {expectedOrientation}.");
                }
                else
                {
                    Console.WriteLine($"Page '{page.Name}' (ID: {page.ID}) orientation mismatch. Expected: {expectedOrientation}, Actual: {actualOrientation}.");
                    allMatch = false;
                }
            }

            // Summary result.
            if (allMatch)
            {
                Console.WriteLine("All pages have the expected print orientation.");
            }
            else
            {
                Console.WriteLine("One or more pages do not match the expected print orientation.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}