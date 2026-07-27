using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the original diagram
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Create a new diagram that will contain only the desired pages
                using (Diagram newDiagram = new Diagram())
                {
                    // Remove the automatically created empty page in the new diagram
                    if (newDiagram.Pages.Count > 0)
                    {
                        Page emptyPage = newDiagram.Pages[0];
                        newDiagram.Pages.Remove(emptyPage);
                    }

                    // Verify that the source diagram has at least two pages
                    if (sourceDiagram.Pages.Count < 2)
                    {
                        Console.WriteLine("The source diagram does not contain two pages.");
                        return;
                    }

                    // Add the first two pages from the source diagram to the new diagram
                    newDiagram.Pages.Add(sourceDiagram.Pages[0]);
                    newDiagram.Pages.Add(sourceDiagram.Pages[1]);

                    // Export the new diagram (containing only the first two pages) to CSV
                    string outputPath = "output.csv";
                    newDiagram.Save(outputPath, SaveFileFormat.Csv);
                    Console.WriteLine($"First two pages have been exported to CSV at: {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
