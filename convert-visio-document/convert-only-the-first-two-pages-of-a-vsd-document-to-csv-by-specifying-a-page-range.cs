using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the source VSD file and the CSV output
            string inputPath = "input.vsd";
            string outputPath = "output.csv";

            // Load the original diagram
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages to export
                if (sourceDiagram.Pages.Count < 2)
                    throw new Exception("The source diagram must contain at least two pages.");

                // Create a new empty diagram that will hold only the desired pages
                using (Diagram subsetDiagram = new Diagram())
                {
                    // Remove the default blank page created by the empty constructor
                    Page blankPage = subsetDiagram.Pages[0];
                    subsetDiagram.Pages.Remove(blankPage);

                    // Add the first two pages from the source diagram
                    subsetDiagram.Pages.Add(sourceDiagram.Pages[0]);
                    subsetDiagram.Pages.Add(sourceDiagram.Pages[1]);

                    // Save the new diagram as CSV; only the added pages are exported
                    subsetDiagram.Save(outputPath, SaveFileFormat.Csv);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
