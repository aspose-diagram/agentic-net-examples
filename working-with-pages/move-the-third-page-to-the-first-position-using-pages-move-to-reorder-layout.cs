using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio document
            Diagram diagram = new Diagram("input.vsdx");

            // Verify that the document has at least three pages
            if (diagram.Pages.Count >= 3)
            {
                // Move the third page (index 2) to the first position (index 0)
                diagram.Pages[2].MoveTo(0);
            }

            // Save the updated document
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
