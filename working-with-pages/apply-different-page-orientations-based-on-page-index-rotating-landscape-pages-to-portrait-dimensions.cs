using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Explicitly type the page variable.
                    Page page = diagram.Pages[i];

                    // Retrieve current orientation.
                    PrintPageOrientationValue currentOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                    // If the page is landscape, change it to portrait and swap dimensions.
                    if (currentOrientation == PrintPageOrientationValue.Landscape)
                    {
                        // Set orientation to portrait.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                        // Swap width and height to reflect the rotation.
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        page.PageSheet.PageProps.PageWidth.Value = height;
                        page.PageSheet.PageProps.PageHeight.Value = width;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}