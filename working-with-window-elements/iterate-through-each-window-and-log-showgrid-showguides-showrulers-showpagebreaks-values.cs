using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each Window and log the required properties
            foreach (Window win in diagram.Windows)
            {
                Console.WriteLine($"Window ID: {win.ID}");
                Console.WriteLine($"ShowGrid: {win.ShowGrid}");
                Console.WriteLine($"ShowGuides: {win.ShowGuides}");
                Console.WriteLine($"ShowRulers: {win.ShowRulers}");
                Console.WriteLine($"ShowPageBreaks: {win.ShowPageBreaks}");
                Console.WriteLine();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
