using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Maximum allowed page height in inches
            const double MaxPageHeight = 15.0;

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Check each page's height
            foreach (Page page in diagram.Pages)
            {
                double height = page.PageSheet.PageProps.PageHeight.Value;
                if (height > MaxPageHeight)
                {
                    throw new Exception($"Page '{page.Name}' height {height} exceeds the maximum allowed {MaxPageHeight} inches.");
                }
            }

            // Save the diagram (unchanged) to confirm successful processing
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
