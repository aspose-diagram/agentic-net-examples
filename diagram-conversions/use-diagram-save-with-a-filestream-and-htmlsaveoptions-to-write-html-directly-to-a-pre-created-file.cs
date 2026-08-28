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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure HTML save options (customize as needed)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Example: htmlOptions.SaveAsSingleFile = true;

            // Open a FileStream for the pre‑created output HTML file
            using (FileStream fileStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
            {
                // Save the diagram as HTML directly to the stream
                diagram.Save(fileStream, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
