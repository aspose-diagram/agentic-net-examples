using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Create a new window instance
        Window window = new Window();

        // Enable the grid display for this window
        window.ShowGrid = BOOL.True;

        // Disable the guides display for this window
        window.ShowGuides = BOOL.False;

        // Add the configured window to the diagram
        diagram.Windows.Add(window);

        // The diagram now contains the new window with the specified settings.
        // If you need to persist the diagram, uncomment the following line:
        // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
