using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and maximum allowed page count
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramGuidePlugin <inputFilePath> <maxPageCount>");
            return;
        }

        string inputPath = args[0];
        if (!int.TryParse(args[1], out int maxPageCount))
        {
            Console.WriteLine("Invalid maxPageCount. It must be an integer.");
            return;
        }

        // Load the diagram from the specified file
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        int pageCount = diagram.Pages.Count;
        Console.WriteLine($"Diagram contains {pageCount} page(s).");

        // If the page count exceeds the threshold, disable guide visibility
        if (pageCount > maxPageCount)
        {
            Console.WriteLine($"Page count exceeds {maxPageCount}. Setting ShowGuides to false.");

            // Ensure there is at least one window; create a default one if none exist
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.ShowGuides = BOOL.False;
                diagram.Windows.Add(defaultWindow);
            }
            else
            {
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGuides = BOOL.False;
                }
            }
        }
        else
        {
            Console.WriteLine("Page count within limit. No changes applied.");
        }

        // Prepare output file path (adds "_modified" suffix)
        string outputPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(inputPath) ?? string.Empty,
            System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        // Save the (potentially) modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
