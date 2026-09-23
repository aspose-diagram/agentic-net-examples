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

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export to include only pages 2 through 4
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.PageIndex = 1; // zero‑based index, so 1 = second page
            htmlOptions.PageCount = 3; // export three pages (2, 3, 4)

            // Save the selected pages as HTML
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Pages 2‑4 exported to HTML successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
