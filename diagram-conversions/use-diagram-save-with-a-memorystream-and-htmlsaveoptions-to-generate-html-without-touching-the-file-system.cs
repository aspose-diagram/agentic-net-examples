using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram from a file.
            // Replace "example.vsdx" with the actual path to your diagram.
            string inputPath = "example.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = false; // Do not export hidden pages.

            // Export the diagram to a memory stream as HTML.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, htmlOptions);
                memoryStream.Position = 0; // Reset stream position for reading.

                // Read the generated HTML content.
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    string htmlContent = reader.ReadToEnd();
                    Console.WriteLine($"HTML generated, length: {htmlContent.Length}");
                    // Optionally display a snippet of the HTML.
                    Console.WriteLine(htmlContent.Substring(0, Math.Min(200, htmlContent.Length)));
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
