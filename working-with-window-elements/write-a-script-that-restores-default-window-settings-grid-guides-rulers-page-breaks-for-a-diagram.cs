using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; Visio diagrams may start with none
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window();
                diagram.Windows.Add(newWindow);
            }

            // Restore default window settings for all windows in the diagram
            foreach (Window window in diagram.Windows)
            {
                // Grid, guides, rulers, and page breaks are shown by default
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;

                // Optional defaults that are commonly enabled
                window.DynamicGridEnabled = BOOL.True;
                window.ShowConnectionPoints = BOOL.True;
            }

            // Save the modified diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
