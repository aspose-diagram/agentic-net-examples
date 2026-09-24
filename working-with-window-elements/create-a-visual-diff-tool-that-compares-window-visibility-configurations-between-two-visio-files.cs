using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiffTool <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Load the two Visio diagrams
        Diagram diagram1 = new Diagram(filePath1);
        Diagram diagram2 = new Diagram(filePath2);

        CompareWindowConfigurations(diagram1, diagram2);
    }

    static void CompareWindowConfigurations(Diagram d1, Diagram d2)
    {
        int count1 = d1.Windows.Count;
        int count2 = d2.Windows.Count;
        int maxCount = Math.Max(count1, count2);

        for (int i = 0; i < maxCount; i++)
        {
            Console.WriteLine($"--- Window Index {i} ---");

            if (i >= count1)
            {
                Console.WriteLine("Only present in second diagram.");
                PrintWindowDetails(d2.Windows[i], "Second");
                continue;
            }

            if (i >= count2)
            {
                Console.WriteLine("Only present in first diagram.");
                PrintWindowDetails(d1.Windows[i], "First");
                continue;
            }

            Window w1 = d1.Windows[i];
            Window w2 = d2.Windows[i];

            CompareBoolProperty("ShowGrid", w1.ShowGrid, w2.ShowGrid);
            CompareBoolProperty("ShowGuides", w1.ShowGuides, w2.ShowGuides);
            CompareBoolProperty("ShowRulers", w1.ShowRulers, w2.ShowRulers);
            CompareBoolProperty("ShowPageBreaks", w1.ShowPageBreaks, w2.ShowPageBreaks);
            CompareBoolProperty("DynamicGridEnabled", w1.DynamicGridEnabled, w2.DynamicGridEnabled);
            CompareBoolProperty("ShowConnectionPoints", w1.ShowConnectionPoints, w2.ShowConnectionPoints);

            CompareLongProperty("WindowWidth", w1.WindowWidth, w2.WindowWidth);
            CompareLongProperty("WindowHeight", w1.WindowHeight, w2.WindowHeight);

            CompareEnumProperty("WindowState", w1.WindowState, w2.WindowState);
            CompareEnumProperty("WindowType", w1.WindowType, w2.WindowType);
        }
    }

    static void CompareBoolProperty(string name, BOOL val1, BOOL val2)
    {
        if (val1 != val2)
            Console.WriteLine($"{name} differs: {val1} vs {val2}");
        else
            Console.WriteLine($"{name} same: {val1}");
    }

    static void CompareLongProperty(string name, long val1, long val2)
    {
        if (val1 != val2)
            Console.WriteLine($"{name} differs: {val1} vs {val2}");
        else
            Console.WriteLine($"{name} same: {val1}");
    }

    static void CompareEnumProperty<T>(string name, T val1, T val2) where T : Enum
    {
        if (!val1.Equals(val2))
            Console.WriteLine($"{name} differs: {val1} vs {val2}");
        else
            Console.WriteLine($"{name} same: {val1}");
    }

    static void PrintWindowDetails(Window window, string diagramLabel)
    {
        Console.WriteLine($"{diagramLabel} Window Details:");
        Console.WriteLine($"  ShowGrid: {window.ShowGrid}");
        Console.WriteLine($"  ShowGuides: {window.ShowGuides}");
        Console.WriteLine($"  ShowRulers: {window.ShowRulers}");
        Console.WriteLine($"  ShowPageBreaks: {window.ShowPageBreaks}");
        Console.WriteLine($"  DynamicGridEnabled: {window.DynamicGridEnabled}");
        Console.WriteLine($"  ShowConnectionPoints: {window.ShowConnectionPoints}");
        Console.WriteLine($"  WindowWidth: {window.WindowWidth}");
        Console.WriteLine($"  WindowHeight: {window.WindowHeight}");
        Console.WriteLine($"  WindowState: {window.WindowState}");
        Console.WriteLine($"  WindowType: {window.WindowType}");
    }
}
