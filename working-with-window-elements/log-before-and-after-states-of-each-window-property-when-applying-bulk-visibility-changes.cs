using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        if (diagram.Windows.Count == 0)
        {
            Window defaultWindow = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 800,
                WindowHeight = 600
            };
            diagram.Windows.Add(defaultWindow);
        }

        foreach (Window window in diagram.Windows)
        {
            BOOL beforeShowGrid = window.ShowGrid;
            BOOL beforeShowGuides = window.ShowGuides;
            BOOL beforeShowRulers = window.ShowRulers;
            BOOL beforeShowPageBreaks = window.ShowPageBreaks;
            BOOL beforeShowConnectionPoints = window.ShowConnectionPoints;
            BOOL beforeDynamicGridEnabled = window.DynamicGridEnabled;

            window.ShowGrid = BOOL.False;
            window.ShowGuides = BOOL.False;
            window.ShowRulers = BOOL.False;
            window.ShowPageBreaks = BOOL.False;
            window.ShowConnectionPoints = BOOL.False;
            window.DynamicGridEnabled = BOOL.False;

            Console.WriteLine($"Window ID {window.ID}:");
            Console.WriteLine($"  ShowGrid               before={beforeShowGrid}, after={window.ShowGrid}");
            Console.WriteLine($"  ShowGuides             before={beforeShowGuides}, after={window.ShowGuides}");
            Console.WriteLine($"  ShowRulers             before={beforeShowRulers}, after={window.ShowRulers}");
            Console.WriteLine($"  ShowPageBreaks         before={beforeShowPageBreaks}, after={window.ShowPageBreaks}");
            Console.WriteLine($"  ShowConnectionPoints   before={beforeShowConnectionPoints}, after={window.ShowConnectionPoints}");
            Console.WriteLine($"  DynamicGridEnabled     before={beforeDynamicGridEnabled}, after={window.DynamicGridEnabled}");
        }

        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}