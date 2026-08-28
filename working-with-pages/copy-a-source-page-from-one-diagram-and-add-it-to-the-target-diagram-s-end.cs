using System.IO;
using System;
using Aspose.Diagram;

class DiagramPageCopier
{
    static void Main()
    {
        try
        {

            // Paths to the source and target Visio files
            string sourcePath = @"C:\Diagrams\SourceDiagram.vdx";
            string targetPath = @"C:\Diagrams\TargetDiagram.vdx";
            string outputPath = @"C:\Diagrams\TargetDiagram_WithCopiedPage.vdx";

            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Load the target diagram
            Diagram targetDiagram = new Diagram(targetPath);

            // Select the page to copy from the source diagram (e.g., the first page)
            Page sourcePage = sourceDiagram.Pages[0];

            // Create a new page in the target diagram
            Page newPage = new Page();
            targetDiagram.Pages.Add(newPage);

            // Copy the contents of the source page into the newly created page
            newPage.Copy(sourcePage);

            // Save the modified target diagram
            targetDiagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
