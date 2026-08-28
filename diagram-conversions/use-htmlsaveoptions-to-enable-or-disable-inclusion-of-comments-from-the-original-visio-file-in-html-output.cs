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

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(sourcePath);

            // -------------------------------------------------
            // Export HTML with comments included
            // -------------------------------------------------
            HTMLSaveOptions includeCommentsOptions = new HTMLSaveOptions();
            includeCommentsOptions.IsExportComments = true; // Enable comment export
            string outputWithComments = "output_with_comments.html";
            diagram.Save(outputWithComments, includeCommentsOptions);
            Console.WriteLine($"HTML saved with comments: {outputWithComments}");

            // -------------------------------------------------
            // Export HTML without comments
            // -------------------------------------------------
            HTMLSaveOptions excludeCommentsOptions = new HTMLSaveOptions();
            excludeCommentsOptions.IsExportComments = false; // Disable comment export
            string outputWithoutComments = "output_without_comments.html";
            diagram.Save(outputWithoutComments, excludeCommentsOptions);
            Console.WriteLine($"HTML saved without comments: {outputWithoutComments}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
