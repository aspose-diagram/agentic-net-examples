using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string dataPath = "externalData.txt";
        string outputPath = "output.vsdx";

        // Guard: check diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: check external data file exists
        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"External data file not found: {dataPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read external data lines
            string[] rawLines = File.ReadAllLines(dataPath);

            // Apply transformation: trim and convert to upper case
            List<string> transformedLines = rawLines
                .Select(line => line.Trim().ToUpperInvariant())
                .ToList();

            // Index to track which transformed line to assign
            int lineIndex = 0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // If we have run out of transformed lines, stop updating further shapes
                    if (lineIndex >= transformedLines.Count)
                        break;

                    // Clear existing text collection
                    shape.Text.Value.Clear();

                    // Add new transformed text
                    shape.Text.Value.Add(new Txt(transformedLines[lineIndex]));

                    // Refresh shape data to apply changes
                    shape.RefreshData();

                    lineIndex++;
                }

                // If all lines have been used, exit outer loop as well
                if (lineIndex >= transformedLines.Count)
                    break;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}