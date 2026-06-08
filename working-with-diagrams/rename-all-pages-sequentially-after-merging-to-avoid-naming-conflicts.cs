using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input diagram file paths
            string targetPath = "target.vsdx";
            string sourcePath = "source.vsdx";

            // Output merged diagram path
            string outputPath = "merged.vsdx";

            // Load both diagrams within using blocks to ensure proper disposal
            using (Diagram targetDiagram = new Diagram(targetPath))
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Merge the source diagram into the target diagram
                targetDiagram.Combine(sourceDiagram);

                // Rename all pages sequentially to avoid naming conflicts
                int pageNumber = 1;
                foreach (Page page in targetDiagram.Pages)
                {
                    string newName = $"Page-{pageNumber}";
                    page.Name = newName;   // Visible name
                    page.NameU = newName;  // Universal name
                    pageNumber++;
                }

                // Save the merged diagram using the correct SaveFileFormat enum
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
