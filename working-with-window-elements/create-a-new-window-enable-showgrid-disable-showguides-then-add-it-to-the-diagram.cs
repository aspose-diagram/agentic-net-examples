using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Create a new window instance
            Window newWindow = new Window();

            // Enable the grid display in the window
            newWindow.ShowGrid = BOOL.True;

            // Disable the guides display in the window
            newWindow.ShowGuides = BOOL.False;

            // Add the configured window to the diagram's window collection
            diagram.Windows.Add(newWindow);

            // Optional: output confirmation
            Console.WriteLine("Window added. ShowGrid = " + newWindow.ShowGrid + ", ShowGuides = " + newWindow.ShowGuides);
        }
    }