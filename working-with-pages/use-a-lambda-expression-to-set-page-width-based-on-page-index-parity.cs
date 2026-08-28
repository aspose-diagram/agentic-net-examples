using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Lambda that sets the page width based on the page index parity
                Action<int> setPageWidth = index =>
                {
                    // Retrieve the page by index
                    Page page = diagram.Pages[index];
                    // Even index -> 8.5 inches, Odd index -> 11 inches
                    page.PageSheet.PageProps.PageWidth.Value = (index % 2 == 0) ? 8.5 : 11.0;
                };

                // Apply the lambda to each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    setPageWidth(i);
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
