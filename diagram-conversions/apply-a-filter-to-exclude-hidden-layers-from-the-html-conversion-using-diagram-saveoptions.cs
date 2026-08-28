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
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Remove hidden shapes and masters (including hidden layers if they are represented as hidden shapes)
            diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

            // Configure HTML export options to exclude hidden pages (hidden layers are not exported by default)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = false;

            // Save the diagram as HTML with the specified options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
