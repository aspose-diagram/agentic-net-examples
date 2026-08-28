using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be inspected
            string filePath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Define the expected page orientation (change as needed)
            PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape;

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the actual orientation value from the page's PrintProps
                PrintPageOrientationValue actualOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                // Compare actual orientation with the expected value
                if (actualOrientation != expectedOrientation)
                {
                    Console.WriteLine($"[ERROR] Page \"{page.Name}\" orientation mismatch. Expected: {expectedOrientation}, Actual: {actualOrientation}");
                    // Optionally, throw an exception to halt execution on mismatch
                    // throw new Exception($"Orientation mismatch on page \"{page.Name}\".");
                }
                else
                {
                    Console.WriteLine($"[OK] Page \"{page.Name}\" orientation matches expected value: {expectedOrientation}");
                }
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
