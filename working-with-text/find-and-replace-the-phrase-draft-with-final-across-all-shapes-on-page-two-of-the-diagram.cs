using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Access the second page (pages are zero‑based indexed)
            var page = diagram.Pages[1];

            // Iterate through all shapes on that page
            foreach (Shape shape in page.Shapes)
            {
                // Replace every occurrence of the word "Draft" with "Final"
                shape.ReplaceText("Draft", "Final");
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
