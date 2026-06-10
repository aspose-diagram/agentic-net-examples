using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new (empty) diagram instance
            Diagram diagram = new Diagram();

            // Create a new blank page
            Page newPage = new Page();

            // Add the page to the diagram's page collection
            diagram.Pages.Add(newPage);

            // Move the newly added page to index 2 (third position, zero‑based)
            newPage.MoveTo(2);

            // Save the diagram (adjust path/format as needed)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            Console.Error.WriteLine($"[ArgumentOutOfRangeException] {ex.Message}");
        }
    }
}
