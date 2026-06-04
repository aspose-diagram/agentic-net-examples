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

            // Configure HTML save options (optional settings can be adjusted here)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.Title = "My Diagram";

            // Create a FileStream for the target HTML file
            using (FileStream fileStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
            {
                // Save the diagram as HTML directly to the FileStream
                diagram.Save(fileStream, htmlOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
