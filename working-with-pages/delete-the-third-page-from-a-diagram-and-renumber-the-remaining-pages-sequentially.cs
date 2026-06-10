using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the diagram (assumes a load rule exists elsewhere in the project)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Ensure there are at least three pages
            if (diagram.Pages.Count >= 3)
            {
                // Get the third page (index is zero‑based)
                Aspose.Diagram.Page thirdPage = diagram.Pages[2];

                // Remove the third page using the provided Remove method
                diagram.Pages.Remove(thirdPage);
            }

            // Renumber the remaining pages sequentially
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Set a new sequential ID (starting from 1)
                diagram.Pages[i].ID = i + 1;

                // Optionally update the page name to reflect the new order
                diagram.Pages[i].Name = "Page-" + (i + 1);
            }

            // Save the modified diagram (assumes a save rule exists elsewhere in the project)
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
