using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be checked
            string diagramPath = "input.vsdx";

            // Expected orientation (change as needed)
            PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape;

            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Perform the diagnostic check
                CheckPrintPageOrientation(diagram, expectedOrientation);
            }

            Console.WriteLine("Diagnostic completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Checks each page's PrintPageOrientation against the expected value.
    /// Throws an exception if any page does not match.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="expected">The expected PrintPageOrientationValue.</param>
    static void CheckPrintPageOrientation(Diagram diagram, PrintPageOrientationValue expected)
    {
        foreach (Page page in diagram.Pages)
        {
            // Access the orientation cell value
            PrintPageOrientationValue actual = page.PageSheet.PrintProps.PrintPageOrientation.Value;

            if (actual != expected)
            {
                string message = $"Page '{page.Name}' (ID: {page.ID}) has orientation '{actual}', expected '{expected}'.";
                // Output diagnostic information
                Console.WriteLine(message);
                // Fail the diagnostic
                throw new Exception(message);
            }
            else
            {
                Console.WriteLine($"Page '{page.Name}' orientation matches expected value '{expected}'.");
            }
        }
    }
}
