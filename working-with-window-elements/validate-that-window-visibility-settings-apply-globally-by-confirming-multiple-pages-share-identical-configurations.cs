using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string diagramPath = "sample.vsdx";

        // Guard: ensure the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one window definition
            if (diagram.Windows.Count == 0)
            {
                throw new Exception("No windows found in the diagram.");
            }

            // Capture visibility settings from the first window as reference values
            Window referenceWindow = diagram.Windows[0];
            BOOL refShowGrid = referenceWindow.ShowGrid;
            BOOL refShowGuides = referenceWindow.ShowGuides;
            BOOL refShowRulers = referenceWindow.ShowRulers;
            BOOL refShowPageBreaks = referenceWindow.ShowPageBreaks;
            BOOL refDynamicGridEnabled = referenceWindow.DynamicGridEnabled;
            BOOL refShowConnectionPoints = referenceWindow.ShowConnectionPoints;

            // Validate that all other windows share the same visibility settings
            for (int i = 1; i < diagram.Windows.Count; i++)
            {
                Window win = diagram.Windows[i];
                if (win.ShowGrid != refShowGrid ||
                    win.ShowGuides != refShowGuides ||
                    win.ShowRulers != refShowRulers ||
                    win.ShowPageBreaks != refShowPageBreaks ||
                    win.DynamicGridEnabled != refDynamicGridEnabled ||
                    win.ShowConnectionPoints != refShowConnectionPoints)
                {
                    throw new Exception($"Window at index {i} has differing visibility settings.");
                }
            }

            // Capture UI visibility setting from the first page (UIVisibilityValue, not BOOL)
            UIVisibilityValue firstPageVisibility = diagram.Pages[0].PageSheet.PageProps.UIVisibility.Value;

            // Validate that all pages share the same UI visibility configuration
            for (int p = 1; p < diagram.Pages.Count; p++)
            {
                UIVisibilityValue pageVis = diagram.Pages[p].PageSheet.PageProps.UIVisibility.Value;
                if (pageVis != firstPageVisibility)
                {
                    throw new Exception($"Page at index {p} has a different UI visibility setting.");
                }
            }

            Console.WriteLine("All window visibility settings are consistent across windows and pages.");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}