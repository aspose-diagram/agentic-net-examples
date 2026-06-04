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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Set to true to include comments, false to exclude them
            htmlOptions.IsExportComments = true; // change to false to disable comment export

            // Save the diagram as HTML using the specified options
            diagram.Save("output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
