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

            // Load the existing diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Insert three blank pages at the beginning of the diagram
            for (int i = 0; i < 3; i++)
            {
                // Create a new blank page
                Page blankPage = new Page();

                // Add the page to the diagram (adds at the end of the collection)
                diagram.Pages.Add(blankPage);

                // Move the newly added page to the first position (index 0)
                blankPage.MoveTo(0);
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
