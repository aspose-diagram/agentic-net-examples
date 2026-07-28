using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and expected orientation (Landscape or Portrait)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Diagnostic.exe <diagramPath> <expectedOrientation>");
            return;
        }

        string diagramPath = args[0];
        string expectedStr = args[1];

        // Parse the expected orientation string to the enum
        if (!Enum.TryParse(expectedStr, ignoreCase: true, out PrintPageOrientationValue expectedOrientation))
        {
            Console.WriteLine($"Invalid expected orientation: {expectedStr}");
            return;
        }

        // Load the diagram
        Diagram diagram = new Diagram(diagramPath);

        // Iterate through each page and validate the PrintPageOrientation
        foreach (Page page in diagram.Pages)
        {
            PrintProps printProps = page.PageSheet.PrintProps;
            PrintPageOrientationValue actualOrientation = printProps.PrintPageOrientation.Value;

            if (actualOrientation != expectedOrientation)
            {
                string message = $"Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {actualOrientation}";
                Console.WriteLine(message);
                throw new Exception(message);
            }
            else
            {
                Console.WriteLine($"Page '{page.Name}' orientation matches expected value: {expectedOrientation}");
            }
        }

        // Clean up
        diagram.Dispose();
        Console.WriteLine("Diagnostic completed successfully.");
    }
}
