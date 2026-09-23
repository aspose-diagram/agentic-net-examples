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

            // Paths for input Visio file and output CSV file
            string inputPath = "input.vsdx";
            string outputPath = "output.csv";

            // Load the source diagram
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Create a new empty diagram
                using (Diagram newDiagram = new Diagram())
                {
                    // Remove the automatically created empty page from the new diagram
                    if (newDiagram.Pages.Count > 0)
                    {
                        Page defaultPage = newDiagram.Pages[0];
                        newDiagram.Pages.Remove(defaultPage);
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

                    // Save the new diagram as CSV (only the added pages will be exported)
                    newDiagram.Save(outputPath, SaveFileFormat.Csv);
                    Console.WriteLine($"First two pages exported to CSV at: {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
