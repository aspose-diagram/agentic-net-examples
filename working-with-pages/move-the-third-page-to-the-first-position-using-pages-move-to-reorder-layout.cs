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
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the diagram has at least three pages
                if (diagram.Pages.Count < 3)
                {
                    Console.WriteLine("The diagram must contain at least three pages.");
                    return;
                }

                // Retrieve the third page (zero‑based index 2)
                Page thirdPage = diagram.Pages[2];

                // Move the third page to the first position (index 0)
                thirdPage.MoveTo(0);

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Third page moved to first position and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
