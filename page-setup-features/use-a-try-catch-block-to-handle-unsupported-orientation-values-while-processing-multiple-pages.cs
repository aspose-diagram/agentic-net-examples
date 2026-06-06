using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Sample orientation values for demonstration (could come from any source)
            string[] orientationValues = { "Landscape", "Portrait", "InvalidValue" };

            // Process each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                string orientStr = orientationValues[i % orientationValues.Length];

                try
                {
                    // Convert the string to the corresponding enum value
                    PrintPageOrientationValue orientation = (PrintPageOrientationValue)Enum.Parse(
                        typeof(PrintPageOrientationValue), orientStr, ignoreCase: true);

                    // Apply the orientation to the page's print properties
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                    Console.WriteLine($"Page {page.ID} orientation set to {orientation}.");
                }
                catch (ArgumentException)
                {
                    // Thrown when the string cannot be parsed to a valid enum value
                    Console.WriteLine($"Unsupported orientation '{orientStr}' for page {page.ID}. Skipping this page.");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors
                    Console.WriteLine($"Error processing page {page.ID}: {ex.Message}");
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
