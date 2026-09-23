using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Verify that the diagram has at least three pages
            if (diagram.Pages.Count >= 3)
            {
                // Access the third page (zero‑based index 2)
                Page page = diagram.Pages[2];

                // Check whether the page contains any shapes
                if (page.Shapes.Count == 0)
                {
                    // Remove the empty page (delete operation)
                    diagram.Pages.RemoveAt(2);
                    Console.WriteLine("Page 3 was empty and has been removed.");
                }
                else
                {
                    Console.WriteLine("Page 3 contains shapes; it will not be removed.");
                }
            }
            else
            {
                Console.WriteLine("The diagram contains fewer than three pages.");
            }

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
