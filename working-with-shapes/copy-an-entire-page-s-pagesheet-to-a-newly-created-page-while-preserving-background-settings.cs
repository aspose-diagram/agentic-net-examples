using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the source diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Select the page to copy (e.g., the first page)
                Page sourcePage = diagram.Pages[0];

                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId)
                        maxId = p.ID;
                }

                // Create a new blank page
                Page newPage = new Page();
                newPage.ID = maxId + 1;
                newPage.Name = sourcePage.Name + "_Copy";

                // Clone the source page's PageSheet (including background settings)
                newPage.Copy(sourcePage);

                // Ensure background flags are explicitly preserved
                newPage.Background = sourcePage.Background;
                newPage.BackPage = sourcePage.BackPage;

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page copied successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
