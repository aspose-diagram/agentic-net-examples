using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Sample orientation values for each page (could come from any source)
            string[] orientationValues = { "Landscape", "Portrait", "InvalidOrientation", "SameAsPrinter" };

            int i = 0;
            foreach (Page page in diagram.Pages)
            {
                if (i >= orientationValues.Length)
                    break;

                string orientStr = orientationValues[i];
                try
                {
                    // Try to parse the string to the enum; throws ArgumentException if not valid
                    PrintPageOrientationValue orientation = (PrintPageOrientationValue)Enum.Parse(
                        typeof(PrintPageOrientationValue), orientStr, ignoreCase: true);

                    // Apply the orientation to the page
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
                    Console.WriteLine($"Page {page.ID} orientation set to {orientation}.");
                }
                catch (ArgumentException)
                {
                    // Handle unsupported orientation values
                    Console.WriteLine($"Unsupported orientation \"{orientStr}\" for page {page.ID}. Defaulting to Portrait.");
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }

                i++;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
