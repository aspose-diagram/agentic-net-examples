using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram from file.
            // Replace "input.vsdx" with the actual path to your Visio file.
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Access the WindowCollection of the diagram.
                WindowCollection windows = diagram.Windows;

                // Iterate through all Window objects.
                for (int i = 0; i < windows.Count; i++)
                {
                    Window win = windows[i];

                    // Example: output some key properties of each window.
                    Console.WriteLine($"Window ID: {win.ID}");
                    Console.WriteLine($"  Type: {win.WindowType}");
                    Console.WriteLine($"  Width: {win.WindowWidth}, Height: {win.WindowHeight}");
                    Console.WriteLine($"  Left: {win.WindowLeft}, Top: {win.WindowTop}");
                    Console.WriteLine();
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
