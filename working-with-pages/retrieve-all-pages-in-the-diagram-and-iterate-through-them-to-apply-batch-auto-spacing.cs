using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram using the provided load rule
            Diagram diagram = LoadDiagram("input.vsdx");   // <-- provided rule

            // Configure autospace options (distance in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical   = 0.5  // vertical spacing
            };

            // Iterate through all pages and apply batch auto‑spacing
            foreach (Page page in diagram.Pages)
            {
                // Auto‑space all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);
            }

            // Save the diagram using the provided save rule
            SaveDiagram(diagram, "output.vsdx");   // <-- provided rule

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // -----------------------------------------------------------------
    // The following helper methods represent the lifecycle rules that
    // must be used for loading and saving. Their implementations are
    // supplied by the surrounding framework and should not be altered.
    // -----------------------------------------------------------------
    static Diagram LoadDiagram(string path) => new Diagram(path);
    static void SaveDiagram(Diagram diagram, string path) => diagram.Save(path, SaveFileFormat.Vsdx);
}
