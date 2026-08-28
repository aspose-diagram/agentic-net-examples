using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Folder containing Visio files to merge
        string inputFolder = @"C:\VisioFiles";
        // Output merged diagram path
        string outputPath = @"C:\MergedOverview.vsdx";

        // Create an empty diagram that will hold the merged result
        Diagram mergedDiagram = new Diagram();

        // Iterate over all Visio files in the specified folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            // Load the source diagram
            Diagram sourceDiagram = new Diagram(filePath);

            // Combine the source diagram into the merged diagram
            mergedDiagram.Combine(sourceDiagram);
        }

        // After combining, adjust layer visibility so that only layers named "Overview" are visible
        foreach (Page page in mergedDiagram.Pages)
        {
            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Keep the "Overview" layer visible, hide all others
                layer.Visible.Value = layer.Name.Value.Equals("Overview", StringComparison.OrdinalIgnoreCase)
                    ? BOOL.True
                    : BOOL.False;
            }
        }

        // Save the merged diagram with only the Overview layers visible
        mergedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
