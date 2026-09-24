using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define a temporary file path for the diagram
        string filePath = "ShowPageBreaksTest.vsdx";

        // -------------------- Create and configure diagram --------------------
        Diagram diagram = new Diagram();

        // Add a page (required for a valid diagram)
        diagram.Pages.Add(new Page());

        // Create a window and enable ShowPageBreaks
        Window window = new Window();
        window.WindowType = WindowTypeValue.Drawing;
        window.WindowState = WindowStateValue.Maximized;
        window.ShowPageBreaks = BOOL.True;

        // Add the window to the diagram
        diagram.Windows.Add(window);

        // Save the diagram to file
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // -------------------- Load diagram and verify setting --------------------
        Diagram loadedDiagram = new Diagram(filePath);

        // Ensure at least one window exists
        if (loadedDiagram.Windows.Count == 0)
        {
            throw new Exception("No windows found after loading the diagram.");
        }

        // Retrieve the first window
        Window loadedWindow = loadedDiagram.Windows[0];

        // Verify that ShowPageBreaks is still true
        if (loadedWindow.ShowPageBreaks != BOOL.True)
        {
            throw new Exception("ShowPageBreaks was not preserved after saving and loading.");
        }

        // If we reach this point, the test passed
        Console.WriteLine("Test passed: ShowPageBreaks remains true after save and reload.");
    }
}
