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
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Lambda that sets the page width:
                // Even-indexed pages -> 8.5 inches, Odd-indexed pages -> 11 inches
                Action<int, Page> setPageWidth = (index, page) =>
                {
                    page.PageSheet.PageProps.PageWidth.Value = (index % 2 == 0) ? 8.5 : 11.0;
                };

                // Apply the lambda to each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    setPageWidth(i, page);
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
