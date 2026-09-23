using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Lambda expression: set page width based on whether the page index is even or odd
            Action<int, Page> setPageWidth = (index, page) =>
            {
                // Even-indexed pages get 8.5 inches width, odd-indexed pages get 11 inches width
                page.PageSheet.PageProps.PageWidth.Value = (index % 2 == 0) ? 8.5 : 11.0;
            };

            // Apply the lambda to each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                setPageWidth(i, diagram.Pages[i]);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
