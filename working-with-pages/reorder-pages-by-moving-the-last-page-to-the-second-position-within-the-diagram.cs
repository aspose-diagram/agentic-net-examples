using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Check that there are at least two pages to reorder
            if (diagram.Pages.Count > 1)
            {
                // Get the last page in the collection
                int lastIndex = diagram.Pages.Count - 1;
                Page lastPage = diagram.Pages[lastIndex];

                // Move the last page to the second position (index 1)
                lastPage.MoveTo(1);
            }

            // Save the diagram with the new page order
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
