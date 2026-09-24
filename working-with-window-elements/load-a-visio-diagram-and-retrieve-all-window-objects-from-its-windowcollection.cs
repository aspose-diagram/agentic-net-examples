using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram contains windows
            if (diagram.Windows.Count == 0)
            {
                Console.WriteLine("The diagram does not contain any windows.");
                return;
            }

            // Iterate through all Window objects in the diagram's WindowCollection
            foreach (Window window in diagram.Windows)
            {
                Console.WriteLine($"Window ID: {window.ID}");
                Console.WriteLine($"Window Type: {window.WindowType}");
                Console.WriteLine($"Window State: {window.WindowState}");
                Console.WriteLine($"Window Width: {window.WindowWidth}");
                Console.WriteLine($"Window Height: {window.WindowHeight}");
                Console.WriteLine(new string('-', 30));
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
