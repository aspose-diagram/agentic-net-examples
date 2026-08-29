using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = null;
            try
            {
                diagram = new Diagram("input.vsdx");
            }
            catch (DiagramException loadEx)
            {
                Console.WriteLine($"Failed to load diagram: {loadEx.Message}");
                return;
            }

            // Ensure the diagram has a Windows collection before adding a new window
            if (diagram.Windows == null)
            {
                Console.WriteLine("The diagram does not contain a Windows collection.");
                return;
            }

            // Create a new window instance
            Window newWindow = new Window
            {
                // Example: set the window type to Drawing
                WindowType = WindowTypeValue.Drawing
            };

            // Attempt to add the window and handle possible exceptions
            try
            {
                diagram.Windows.Add(newWindow);
                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Window added and diagram saved successfully.");
            }
            catch (DiagramException addEx)
            {
                Console.WriteLine($"Error adding window to diagram: {addEx.Message}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
