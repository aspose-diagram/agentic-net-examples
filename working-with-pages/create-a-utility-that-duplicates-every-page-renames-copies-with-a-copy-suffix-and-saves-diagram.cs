using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public static class DiagramPageDuplicator
{
    /// <summary>
    /// Duplicates every page in the source Visio diagram, appends "_Copy" to the duplicated page names,
    /// and saves the result to the specified output file.
    /// </summary>
    /// <param name="inputPath">Path to the source diagram file.</param>
    /// <param name="outputPath">Path where the modified diagram will be saved.</param>
    public static void DuplicatePages(string inputPath, string outputPath)
    {
        // Load the diagram using the provided constructor rule.
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Store the original page count to avoid iterating over pages added during duplication.
            int originalPageCount = diagram.Pages.Count;

            for (int i = 0; i < originalPageCount; i++)
            {
                // Reference to the source page.
                Page sourcePage = diagram.Pages[i];

                // Create a new page instance.
                Page copiedPage = new Page();

                // Set the new page's name with the required suffix.
                copiedPage.Name = sourcePage.Name + "_Copy";

                // Copy the page content (shapes, connectors, etc.) from the source page.
                copiedPage.Copy(sourcePage);

                // Copy the pagesheet (page-level properties) from the source page.
                copiedPage.PageSheet.Copy(sourcePage.PageSheet);

                // Add the newly created page to the diagram's page collection.
                diagram.Pages.Add(copiedPage);
            }

            // Save the modified diagram using the provided Save method rule.
            diagram.Save(outputPath, SaveFileFormat.Vdx);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramPageDuplicator.DuplicatePages("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
