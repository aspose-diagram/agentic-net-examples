using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram using the provided constructor
            using (Diagram diagram = new Diagram(filePath))
            {
                // Retrieve the collection of Window objects
                WindowCollection windows = diagram.Windows;

                // Iterate through the collection and output basic information
                for (int i = 0; i < windows.Count; i++)
                {
                    Window win = windows[i];
                    Console.WriteLine($"Window {i}: ID = {win.ID}, Type = {win.WindowType}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
