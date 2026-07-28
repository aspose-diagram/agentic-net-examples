using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load source and destination diagrams
            var sourceDiagram = new Aspose.Diagram.Diagram("SourceDiagram.vdx");
            var targetDiagram = new Aspose.Diagram.Diagram("TargetDiagram.vdx");

            // Index of the page to copy from the source diagram (0‑based)
            int sourcePageIndex = 2;

            // Desired insertion index in the target diagram (0‑based)
            int insertIndex = 1;

            // Get the page to be copied from the source diagram
            var sourcePage = sourceDiagram.Pages[sourcePageIndex];

            // Create a new empty page in the target diagram
            var newPage = new Aspose.Diagram.Page();

            // Add the new page to the target diagram's page collection
            targetDiagram.Pages.Add(newPage);

            // Copy the contents of the source page into the newly created page
            newPage.Copy(sourcePage);

            // Move the newly added page to the specified index within the target diagram
            newPage.MoveTo(insertIndex);

            // Save the modified target diagram
            targetDiagram.Save("MergedDiagram.vdx", Aspose.Diagram.SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
