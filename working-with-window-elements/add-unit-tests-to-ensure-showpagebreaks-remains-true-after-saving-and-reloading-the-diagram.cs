using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path for temporary diagram file
        string filePath = "test_diagram.vsdx";

        // Create a new diagram and set ShowPageBreaks to true
        using (Diagram diagram = new Diagram())
        {
            // Add a window because a new diagram has none by default
            Window window = new Window();
            window.ShowPageBreaks = BOOL.True;
            diagram.Windows.Add(window);

            // Save the diagram to a file
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }

        // Load the diagram back from the file
        using (Diagram loadedDiagram = new Diagram(filePath))
        {
            // Verify that a window exists
            if (loadedDiagram.Windows.Count == 0)
                throw new Exception("No windows found after loading the diagram.");

            // Check that ShowPageBreaks is still true
            Window loadedWindow = loadedDiagram.Windows[0];
            if (loadedWindow.ShowPageBreaks != BOOL.True)
                throw new Exception("ShowPageBreaks was not preserved after save and reload.");

            // If we reach here, the test succeeded
            Console.WriteLine("Test passed: ShowPageBreaks remains true after save and reload.");
        }
    }
}
