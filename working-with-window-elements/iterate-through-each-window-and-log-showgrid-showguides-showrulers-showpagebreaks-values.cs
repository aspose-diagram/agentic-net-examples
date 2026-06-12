using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each Window element in the diagram
            foreach (Window window in diagram.Windows)
            {
                // Log the required properties for the current window
                Console.WriteLine(
                    $"Window ID: {window.ID}, " +
                    $"ShowGrid: {window.ShowGrid}, " +
                    $"ShowGuides: {window.ShowGuides}, " +
                    $"ShowRulers: {window.ShowRulers}, " +
                    $"ShowPageBreaks: {window.ShowPageBreaks}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
