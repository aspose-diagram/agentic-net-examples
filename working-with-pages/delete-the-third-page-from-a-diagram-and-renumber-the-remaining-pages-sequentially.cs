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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Delete the third page (index 2) if it exists
            if (diagram.Pages.Count >= 3)
            {
                Page pageToRemove = diagram.Pages[2];
                diagram.Pages.Remove(pageToRemove);
            }

            // Renumber the remaining pages sequentially starting from 1
            int newId = 1;
            foreach (Page page in diagram.Pages)
            {
                page.ID = newId;                     // Set new unique ID
                page.Name = "Page-" + newId;         // Optional: update the page name
                newId++;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
