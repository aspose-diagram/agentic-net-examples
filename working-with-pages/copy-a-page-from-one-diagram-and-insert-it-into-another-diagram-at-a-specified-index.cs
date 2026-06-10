using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string sourceFile = "source.vsdx";
        if (!System.IO.File.Exists(sourceFile))
        {
            Console.Error.WriteLine($"File not found: {sourceFile}");
            return;
        }
        string targetFile = "target.vsdx";
        string outputFile = "merged.vsdx";

        // Load the source and target diagrams (lifecycle rule: load)
        Diagram sourceDiagram = new Diagram(sourceFile);
        Diagram targetDiagram = new Diagram(targetFile);

        // Indices (adjust as needed)
        int sourcePageIndex = 0;   // page to copy from source diagram
        int insertIndex = 2;       // position where the page will be inserted in target diagram

        // Get the page to copy
        Page pageToCopy = sourceDiagram.Pages[sourcePageIndex];

        // Create a new empty page and copy the contents of the source page (feature rule: Page.Copy)
        Page newPage = new Page();
        newPage.Copy(pageToCopy);

        // Add the new page to the target diagram's Pages collection (lifecycle rule: create/add)
        targetDiagram.Pages.Add(newPage);

        // Move the newly added page to the desired index (feature rule: Page.MoveTo)
        newPage.MoveTo(insertIndex);

        // Save the modified target diagram (lifecycle rule: save)
        targetDiagram.Save(outputFile, SaveFileFormat.Vsdx);
    }
}
