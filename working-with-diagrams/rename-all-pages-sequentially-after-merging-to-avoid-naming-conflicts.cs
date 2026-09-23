using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the merged diagram (replace with your actual file path)
            Diagram diagram = new Diagram("merged.vsdx");

            // Rename each page sequentially to avoid naming conflicts
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Page names are 1‑based for readability (Page1, Page2, ...)
                diagram.Pages[i].Name = $"Page{i + 1}";
            }

            // Save the diagram with the updated page names
            diagram.Save("merged_renamed.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
