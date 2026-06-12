using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string filePath = "sample.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Retrieve the collection of Window objects
            WindowCollection windows = diagram.Windows;

            // Iterate through all windows in the collection
            for (int i = 0; i < windows.Count; i++)
            {
                Window win = windows[i];
                // Example: display the window's ID and type
                Console.WriteLine($"Window {i}: ID = {win.ID}, Type = {win.WindowType}");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
