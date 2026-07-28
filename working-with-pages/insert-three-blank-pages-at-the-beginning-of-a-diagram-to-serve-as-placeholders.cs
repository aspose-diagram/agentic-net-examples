using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class InsertBlankPages
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (uses the provided load rule)
            string inputFile = "input.vsdx";
            Diagram diagram = new Diagram(inputFile);

            // Insert three blank pages at the beginning
            for (int i = 0; i < 3; i++)
            {
                // Create a new empty page
                Page blankPage = new Page();

                // Add the page to the document's page collection
                diagram.Pages.Add(blankPage);

                // Move the newly added page to the first position (index 0)
                blankPage.MoveTo(0);
            }

            // Save the modified diagram (uses the provided save rule)
            string outputFile = "output.vsdx";
            diagram.Save(outputFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
