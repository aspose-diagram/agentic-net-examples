using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Create a new diagram (empty or loaded as needed)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram();

            // Create a new blank page
            Aspose.Diagram.Page newPage = new Aspose.Diagram.Page();

            // Add the page to the diagram's page collection (adds at the end)
            diagram.Pages.Add(newPage);

            // Move the newly added page to index 2 (third position, zero‑based)
            newPage.MoveTo(2);

            // (Optional) Save the diagram to verify the insertion
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            Console.Error.WriteLine($"[ArgumentOutOfRangeException] {ex.Message}");
        }
    }
}
