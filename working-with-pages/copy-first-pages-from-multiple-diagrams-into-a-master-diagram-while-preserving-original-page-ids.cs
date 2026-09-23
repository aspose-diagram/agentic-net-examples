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

            // Paths to source Visio files whose first pages will be copied.
            string[] sourceFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Create an empty master diagram.
            Diagram masterDiagram = new Diagram();

            // The newly created diagram contains a default empty page.
            // Remove it so we can add only the copied pages.
            if (masterDiagram.Pages.Count > 0)
            {
                masterDiagram.Pages.Remove(masterDiagram.Pages[0]);
            }

            foreach (string filePath in sourceFiles)
            {
                // Load the source diagram.
                Diagram sourceDiagram = new Diagram(filePath);

                // Ensure the source diagram has at least one page.
                if (sourceDiagram.Pages.Count == 0)
                {
                    Console.WriteLine($"Source file '{filePath}' contains no pages. Skipping.");
                    continue;
                }

                // Get the first page from the source diagram.
                Page sourcePage = sourceDiagram.Pages[0];

                // Create a new page in the master diagram.
                Page newPage = new Page();

                // Copy the content of the source page into the new page.
                newPage.Copy(sourcePage);

                // Preserve the original page ID and name.
                newPage.ID = sourcePage.ID;
                newPage.Name = sourcePage.Name;

                // Add the new page to the master diagram.
                masterDiagram.Pages.Add(newPage);

                Console.WriteLine($"Copied page '{sourcePage.Name}' (ID={sourcePage.ID}) from '{filePath}'.");
            }

            // Save the master diagram containing the copied pages.
            string outputPath = "MasterDiagram.vsdx";
            masterDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Master diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
