using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each Window element in the diagram
            foreach (Window window in diagram.Windows)
            {
                // Log the window ID and the required display settings
                Console.WriteLine($"Window ID: {window.ID}");
                Console.WriteLine($"  ShowGrid: {window.ShowGrid}");
                Console.WriteLine($"  ShowGuides: {window.ShowGuides}");
                Console.WriteLine($"  ShowRulers: {window.ShowRulers}");
                Console.WriteLine($"  ShowPageBreaks: {window.ShowPageBreaks}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
