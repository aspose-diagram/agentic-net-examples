using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each Window in the diagram
            foreach (Window window in diagram.Windows)
            {
                // Log window identifier and visibility settings
                Console.WriteLine($"Window ID: {window.ID}");
                Console.WriteLine($"ShowGrid: {window.ShowGrid}");
                Console.WriteLine($"ShowGuides: {window.ShowGuides}");
                Console.WriteLine($"ShowRulers: {window.ShowRulers}");
                Console.WriteLine($"ShowPageBreaks: {window.ShowPageBreaks}");
                Console.WriteLine(); // separator
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
