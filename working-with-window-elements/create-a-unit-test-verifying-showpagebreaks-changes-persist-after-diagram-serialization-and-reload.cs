using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Define a temporary file path for the diagram
            string filePath = "ShowPageBreaksTest.vsdx";

            // Create a new diagram, add a window, set ShowPageBreaks, and save it
            using (var diagram = new Diagram())
            {
                // Create a window and enable ShowPageBreaks
                var window = new Window();
                window.ShowPageBreaks = BOOL.True;

                // Add the window to the diagram
                diagram.Windows.Add(window);

                // Save the diagram to a VSDX file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            // Load the saved diagram and verify that ShowPageBreaks persisted
            using (var loadedDiagram = new Diagram(filePath))
            {
                // Ensure at least one window exists
                if (loadedDiagram.Windows.Count == 0)
                {
                    throw new Exception("No windows were loaded from the diagram.");
                }

                // Retrieve the first window
                var loadedWindow = loadedDiagram.Windows[0];

                // Verify the ShowPageBreaks property
                if (loadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks value did not persist after saving and reloading the diagram.");
                }

                // If verification passes, output success message
                Console.WriteLine("ShowPageBreaks persisted successfully after diagram serialization and reload.");
            }
        }
    }