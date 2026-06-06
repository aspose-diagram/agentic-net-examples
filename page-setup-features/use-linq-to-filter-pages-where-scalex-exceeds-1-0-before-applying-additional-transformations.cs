using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Filter pages where the print scaling factor ScaleX exceeds 1.0
                var pagesToTransform = diagram.Pages
                    .Cast<Page>()
                    .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                    .ToList();

                // Apply additional transformations to the filtered pages
                foreach (var page in pagesToTransform)
                {
                    // Example transformation: reset ScaleX to 1.0
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
