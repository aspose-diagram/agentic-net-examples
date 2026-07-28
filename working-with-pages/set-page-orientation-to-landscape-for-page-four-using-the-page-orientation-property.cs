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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least four pages (index 0‑based)
                if (diagram.Pages.Count < 4)
                    throw new Exception("The diagram does not contain a fourth page.");

                // Retrieve the fourth page (index 3)
                Page page = diagram.Pages[3];

                // Set the page orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page orientation set to Landscape for page four and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
