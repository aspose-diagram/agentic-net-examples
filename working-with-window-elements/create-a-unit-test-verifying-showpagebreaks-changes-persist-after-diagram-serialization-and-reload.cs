using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Prepare temporary file path
        string tempFile = Path.Combine(Path.GetTempPath(), "ShowPageBreaksTest.vsdx");

        try
        {
            // 1. Create a new diagram
            Diagram diagram = new Diagram();

            // 2. Add a window (required for UI settings)
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing; // set as drawing window
            window.ShowPageBreaks = BOOL.True; // enable page breaks
            diagram.Windows.Add(window);

            // 3. Save the diagram to a file
            diagram.Save(tempFile, SaveFileFormat.Vsdx);

            // 4. Load the diagram back
            Diagram loadedDiagram = new Diagram(tempFile);

            // 5. Verify that ShowPageBreaks persisted
            if (loadedDiagram.Windows.Count == 0)
                throw new Exception("No windows found after loading the diagram.");

            Window loadedWindow = loadedDiagram.Windows[0];
            if (loadedWindow.ShowPageBreaks != BOOL.True)
                throw new Exception("ShowPageBreaks value did not persist as TRUE after reload.");

            // 6. Change the setting to FALSE and verify persistence again
            loadedWindow.ShowPageBreaks = BOOL.False;
            loadedDiagram.Save(tempFile, SaveFileFormat.Vsdx);

            Diagram reloadedDiagram = new Diagram(tempFile);
            Window reloadedWindow = reloadedDiagram.Windows[0];
            if (reloadedWindow.ShowPageBreaks != BOOL.False)
                throw new Exception("ShowPageBreaks value did not persist as FALSE after second reload.");

            Console.WriteLine("ShowPageBreaks persistence test passed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
            throw;
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }
}
