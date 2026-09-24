using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window to work with
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 800;
                defaultWindow.WindowHeight = 600;
                defaultWindow.ShowRulers = BOOL.False; // initial state for demonstration
                diagram.Windows.Add(defaultWindow);
            }

            // Use LINQ to find windows where ShowRulers is false
            var windowsToEnable = diagram.Windows
                                        .Where(w => w.ShowRulers == BOOL.False)
                                        .ToList();

            // Enable rulers for the filtered windows
            foreach (var win in windowsToEnable)
            {
                win.ShowRulers = BOOL.True;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
