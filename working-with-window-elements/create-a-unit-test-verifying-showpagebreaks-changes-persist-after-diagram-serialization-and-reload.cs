using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a window because a new diagram has none by default
        Window window = new Window();
        window.WindowType = WindowTypeValue.Drawing;
        window.WindowState = WindowStateValue.Maximized;
        window.WindowWidth = 800;
        window.WindowHeight = 600;
        diagram.Windows.Add(window);

        // Set ShowPageBreaks to true
        diagram.Windows[0].ShowPageBreaks = BOOL.True;

        // Prepare a temporary file path for saving
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");

        try
        {
            // Save the diagram to VSDX format
            diagram.Save(tempPath, SaveFileFormat.Vsdx);

            // Load the diagram back from the saved file
            Diagram loadedDiagram = new Diagram(tempPath);

            // Verify that the ShowPageBreaks setting persisted
            if (loadedDiagram.Windows.Count == 0)
            {
                throw new Exception("Loaded diagram contains no windows.");
            }

            BOOL showPageBreaks = loadedDiagram.Windows[0].ShowPageBreaks;
            if (showPageBreaks != BOOL.True)
            {
                throw new Exception("ShowPageBreaks value did not persist after reload.");
            }

            Console.WriteLine("ShowPageBreaks persisted correctly after serialization and reload.");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}
