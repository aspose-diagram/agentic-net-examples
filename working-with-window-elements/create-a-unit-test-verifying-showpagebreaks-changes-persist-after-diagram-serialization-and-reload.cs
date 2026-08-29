using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define a temporary file path for the diagram
        string filePath = Path.Combine(Path.GetTempPath(), "ShowPageBreaksTest.vsdx");

        // ---------- Create a new diagram ----------
        Diagram diagram = new Diagram();

        // Add a window to the diagram (new diagrams have no windows by default)
        Window window = new Window();
        window.WindowType = WindowTypeValue.Drawing;          // Set window type to Drawing
        window.WindowState = WindowStateValue.Maximized;      // Maximize the window
        window.ShowPageBreaks = BOOL.True;                    // Enable ShowPageBreaks

        // Add the window to the diagram's window collection
        diagram.Windows.Add(window);

        // Save the diagram to a file (VSDX format)
        diagram.Save(filePath, SaveFileFormat.Vsdx);

        // ---------- Load the diagram back ----------
        Diagram loadedDiagram = new Diagram(filePath);

        // Verify that a window exists
        if (loadedDiagram.Windows.Count == 0)
        {
            throw new Exception("No windows were found after loading the diagram.");
        }

        // Retrieve the first window
        Window loadedWindow = loadedDiagram.Windows[0];

        // Verify that ShowPageBreaks persisted
        if (loadedWindow.ShowPageBreaks != BOOL.True)
        {
            throw new Exception("ShowPageBreaks value did not persist after serialization.");
        }

        // If we reach this point, the test succeeded
        Console.WriteLine("ShowPageBreaks persisted correctly after save and reload.");

        // Optional: clean up the temporary file
        try
        {
            File.Delete(filePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
