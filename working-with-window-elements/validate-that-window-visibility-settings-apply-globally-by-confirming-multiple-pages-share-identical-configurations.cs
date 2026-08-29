using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (replace with actual path)
        string filePath = "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // ---------- Validate window visibility settings ----------
            // Ensure the diagram contains at least one Window element
            if (diagram.Windows.Count == 0)
                throw new Exception("The diagram does not contain any Window elements.");

            // Capture visibility settings from the first window for comparison
            Window firstWindow = diagram.Windows[0];
            BOOL firstDynamicGrid = firstWindow.DynamicGridEnabled;
            BOOL firstShowConnectionPoints = firstWindow.ShowConnectionPoints;
            BOOL firstShowGrid = firstWindow.ShowGrid;
            BOOL firstShowGuides = firstWindow.ShowGuides;
            BOOL firstShowPageBreaks = firstWindow.ShowPageBreaks;
            BOOL firstShowRulers = firstWindow.ShowRulers;

            // Iterate through remaining windows and compare each setting
            for (int i = 1; i < diagram.Windows.Count; i++)
            {
                Window w = diagram.Windows[i];

                if (w.DynamicGridEnabled != firstDynamicGrid ||
                    w.ShowConnectionPoints != firstShowConnectionPoints ||
                    w.ShowGrid != firstShowGrid ||
                    w.ShowGuides != firstShowGuides ||
                    w.ShowPageBreaks != firstShowPageBreaks ||
                    w.ShowRulers != firstShowRulers)
                {
                    throw new Exception($"Window at index {i} has different visibility settings than the first window.");
                }
            }

            // ---------- Validate that all pages share identical UI visibility ----------
            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram does not contain any pages.");

            // Capture UIVisibility enum value from the first page
            Page firstPage = diagram.Pages[0];
            UIVisibilityValue firstUIVisibility = firstPage.PageSheet.PageProps.UIVisibility.Value;

            // Compare UIVisibility of each subsequent page with the first page
            for (int i = 1; i < diagram.Pages.Count; i++)
            {
                Page p = diagram.Pages[i];
                if (p.PageSheet.PageProps.UIVisibility.Value != firstUIVisibility)
                {
                    throw new Exception($"Page at index {i} has a different UIVisibility setting than the first page.");
                }
            }

            Console.WriteLine("All window visibility settings are identical across windows, and all pages share the same UI visibility configuration.");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}