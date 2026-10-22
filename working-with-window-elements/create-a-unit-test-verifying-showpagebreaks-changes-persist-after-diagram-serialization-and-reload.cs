using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define a temporary file path for the test diagram
        string tempFilePath = Path.Combine(Path.GetTempPath(), "ShowPageBreaksTest.vsdx");

        // Ensure any previous test file is removed
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }

        try
        {
            // ---------- Create a new diagram and set ShowPageBreaks ----------
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one window; add one if necessary
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    diagram.Windows.Add(newWindow);
                }

                // Set ShowPageBreaks to TRUE on the first window
                Window window = diagram.Windows[0];
                window.ShowPageBreaks = BOOL.True;

                // Save the diagram to the temporary file
                diagram.Save(tempFilePath, SaveFileFormat.Vsdx);
            }

            // Guard: verify the file was created
            if (!File.Exists(tempFilePath))
            {
                Console.Error.WriteLine("Failed to create the test diagram file.");
                return;
            }

            // ---------- Reload the diagram and verify the setting ----------
            using (Diagram loadedDiagram = new Diagram(tempFilePath))
            {
                // Ensure the loaded diagram contains a window
                if (loadedDiagram.Windows.Count == 0)
                {
                    throw new Exception("No windows found in the loaded diagram.");
                }

                // Retrieve the window and check ShowPageBreaks value
                Window loadedWindow = loadedDiagram.Windows[0];
                if (loadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks value was not persisted after reload.");
                }

                // If the check passes, output success message
                Console.WriteLine("ShowPageBreaks persisted correctly after serialization and reload.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}