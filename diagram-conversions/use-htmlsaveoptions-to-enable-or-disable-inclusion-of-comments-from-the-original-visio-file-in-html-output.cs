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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("sample.vsdx");

            // Create HTML save options instance
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Enable inclusion of comments in the generated HTML
            htmlOptions.IsExportComments = true; // Set to false to exclude comments

            // Save the diagram to HTML with comments included
            diagram.Save("output_with_comments.html", htmlOptions);

            // Disable inclusion of comments
            htmlOptions.IsExportComments = false;

            // Save the diagram to HTML without comments
            diagram.Save("output_without_comments.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
