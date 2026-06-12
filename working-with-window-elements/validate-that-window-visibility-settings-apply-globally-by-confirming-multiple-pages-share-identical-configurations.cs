using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (replace with your actual file path)
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // If there is only one window, the settings are trivially consistent
            if (diagram.Windows.Count < 2)
            {
                Console.WriteLine("Only one window present; visibility settings are globally consistent.");
                return;
            }

            // Capture visibility settings from the first window as the reference
            Window reference = diagram.Windows[0];
            var showGrid = reference.ShowGrid;
            var showGuides = reference.ShowGuides;
            var showRulers = reference.ShowRulers;
            var showPageBreaks = reference.ShowPageBreaks;
            var dynamicGrid = reference.DynamicGridEnabled;
            var showConnectionPoints = reference.ShowConnectionPoints;

            // Compare each subsequent window's settings with the reference
            for (int i = 1; i < diagram.Windows.Count; i++)
            {
                Window w = diagram.Windows[i];
                if (w.ShowGrid != showGrid ||
                    w.ShowGuides != showGuides ||
                    w.ShowRulers != showRulers ||
                    w.ShowPageBreaks != showPageBreaks ||
                    w.DynamicGridEnabled != dynamicGrid ||
                    w.ShowConnectionPoints != showConnectionPoints)
                {
                    throw new Exception($"Window at index {i} does not share identical visibility settings with the first window.");
                }
            }

            Console.WriteLine("All windows share identical visibility configurations.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
