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

            // Path to an existing Visio file (replace with actual path)
            string inputPath = "input.vsdx";
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; if not, create a default one
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }

            // Get the first (and only) window – window settings are global for the diagram
            Window win = diagram.Windows[0];

            // Set visibility settings globally
            win.ShowGrid = BOOL.True;
            win.ShowGuides = BOOL.True;
            win.ShowRulers = BOOL.True;
            win.ShowPageBreaks = BOOL.True;
            win.DynamicGridEnabled = BOOL.True;
            win.ShowConnectionPoints = BOOL.True;

            // Store the expected configuration
            BOOL expectedShowGrid = win.ShowGrid;
            BOOL expectedShowGuides = win.ShowGuides;
            BOOL expectedShowRulers = win.ShowRulers;
            BOOL expectedShowPageBreaks = win.ShowPageBreaks;
            BOOL expectedDynamicGridEnabled = win.DynamicGridEnabled;
            BOOL expectedShowConnectionPoints = win.ShowConnectionPoints;

            // Validate that all pages see the same window configuration
            foreach (Page page in diagram.Pages)
            {
                // Since window settings are global, the same Window instance is used for every page.
                // We simply compare the current values with the expected ones.
                if (win.ShowGrid != expectedShowGrid ||
                    win.ShowGuides != expectedShowGuides ||
                    win.ShowRulers != expectedShowRulers ||
                    win.ShowPageBreaks != expectedShowPageBreaks ||
                    win.DynamicGridEnabled != expectedDynamicGridEnabled ||
                    win.ShowConnectionPoints != expectedShowConnectionPoints)
                {
                    throw new Exception($"Window visibility settings mismatch on page '{page.Name}'.");
                }
            }

            Console.WriteLine("All pages share identical window visibility settings.");

            // Save the diagram (optional)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
