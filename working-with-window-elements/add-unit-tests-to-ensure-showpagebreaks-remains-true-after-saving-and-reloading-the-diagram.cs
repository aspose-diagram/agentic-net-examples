using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {
            TestShowPageBreaksPersistence();
            Console.WriteLine("Test passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
    }

    static void TestShowPageBreaksPersistence()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one window and set ShowPageBreaks to true
        if (diagram.Windows.Count == 0)
        {
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.ShowPageBreaks = BOOL.True;
            diagram.Windows.Add(window);
        }
        else
        {
            diagram.Windows[0].ShowPageBreaks = BOOL.True;
        }

        // Save to a temporary file
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
        diagram.Save(tempPath, SaveFileFormat.Vsdx);

        // Load the diagram back
        Diagram loadedDiagram = new Diagram(tempPath);

        // Verify that ShowPageBreaks is still true
        if (loadedDiagram.Windows.Count == 0)
        {
            throw new Exception("No windows found after loading.");
        }

        if (loadedDiagram.Windows[0].ShowPageBreaks != BOOL.True)
        {
            throw new Exception("ShowPageBreaks was not preserved after save/load.");
        }

        // Clean up the temporary file
        File.Delete(tempPath);
    }
}
