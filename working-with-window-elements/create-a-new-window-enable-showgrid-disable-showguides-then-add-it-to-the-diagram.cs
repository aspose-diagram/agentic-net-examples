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

            // Enable the grid display
            window.ShowGrid = BOOL.True;

            // Disable the guides display
            window.ShowGuides = BOOL.False;

            // Add the configured window to the diagram's window collection
            diagram.Windows.Add(window);

            // Save the diagram to a VSDX file (optional, demonstrates persistence)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }