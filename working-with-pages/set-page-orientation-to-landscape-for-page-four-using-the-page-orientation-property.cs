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

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least four pages
            if (diagram.Pages.Count < 4)
            {
                throw new Exception("The diagram does not contain a fourth page.");
            }

            // Access the fourth page (zero‑based index 3)
            Page pageFour = diagram.Pages[3];

            // Set the page orientation to Landscape
            pageFour.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

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
