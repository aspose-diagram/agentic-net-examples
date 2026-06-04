using System;
using System.IO;
using System.Threading;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        int pageCount = diagram.Pages.Count;
        Console.WriteLine($"Diagram loaded. Total pages: {pageCount}");
        Console.WriteLine();

        for (int i = 0; i < pageCount; i++)
        {
            Console.Write($"Processing page {i + 1}/{pageCount} ");
            ShowSpinner(3); // simulate 3 seconds of work per page
            Console.WriteLine(" - Done");
        }

        Console.WriteLine();
        Console.WriteLine("All pages processed.");
    }

    private static void ShowSpinner(int seconds)
    {
        char[] sequence = new[] { '|', '/', '-', '\\' };
        int totalTicks = seconds * 10; // update every 100ms
        for (int i = 0; i < totalTicks; i++)
        {
            Console.Write(sequence[i % sequence.Length]);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Thread.Sleep(100);
        }
    }
}