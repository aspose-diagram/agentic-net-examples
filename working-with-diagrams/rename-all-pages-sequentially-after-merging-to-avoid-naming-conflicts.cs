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

            // Paths to the source Visio files
            string firstFilePath = "FirstDiagram.vsdx";
            string secondFilePath = "SecondDiagram.vsdx";
            string outputFilePath = "MergedDiagram.vsdx";

            // Load the first diagram (target) and the second diagram (source)
            using (Diagram targetDiagram = new Diagram(firstFilePath))
            using (Diagram sourceDiagram = new Diagram(secondFilePath))
            {
                // Merge the source diagram into the target diagram
                targetDiagram.Combine(sourceDiagram);

                // Rename all pages sequentially to avoid naming conflicts
                int pageIndex = 1;
                foreach (Page page in targetDiagram.Pages)
                {
                    string newName = $"Page-{pageIndex}";
                    page.Name = newName;      // Set the display name
                    page.NameU = newName;     // Set the universal name
                    pageIndex++;
                }

                // Save the merged diagram with the new page names
                targetDiagram.Save(outputFilePath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagrams merged and pages renamed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
