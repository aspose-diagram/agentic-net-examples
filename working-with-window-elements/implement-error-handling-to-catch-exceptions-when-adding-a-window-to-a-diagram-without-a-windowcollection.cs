using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Load an existing diagram (or create a new one)
        Diagram diagram = new Diagram(); // using the provided create rule

        // Attempt to add a Window to the diagram
        try
        {
            // The Windows collection may be null if the diagram does not support windows.
            // This will throw a NullReferenceException or ArgumentNullException.
            diagram.Windows.Add(new Window());
            Console.WriteLine("Window added successfully.");
        }
        catch (Exception ex)
        {
            // Handle any exception that occurs during the addition.
            Console.WriteLine($"Error adding Window: {ex.Message}");
        }

        // Save the diagram (using the provided save rule)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
