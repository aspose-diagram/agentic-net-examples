using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Create a new blank page
            Page newPage = new Page();

            // Add the page to the diagram's page collection
            diagram.Pages.Add(newPage);

            // Move the newly added page to index 2 (0‑based index)
            // If there are fewer than 3 pages, MoveTo will place it at the end.
            newPage.MoveTo(2);

            // Save the diagram (using the provided save rule)
            diagram.Save("Result.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            Console.Error.WriteLine($"[ArgumentOutOfRangeException] {ex.Message}");
        }
    }
}
