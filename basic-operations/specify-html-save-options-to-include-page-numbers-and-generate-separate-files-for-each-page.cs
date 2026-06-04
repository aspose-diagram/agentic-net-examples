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

            // Load the diagram (replace with your source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Generate separate files for each page (default is false, set explicitly for clarity)
            htmlOptions.SaveAsSingleFile = false;

            // Render all pages (default is MaxValue, set explicitly)
            htmlOptions.PageCount = int.MaxValue;

            // Save the diagram as HTML.
            // When SaveAsSingleFile is false, Aspose.Diagram creates one HTML file per page,
            // and the file names include the page numbers automatically.
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
