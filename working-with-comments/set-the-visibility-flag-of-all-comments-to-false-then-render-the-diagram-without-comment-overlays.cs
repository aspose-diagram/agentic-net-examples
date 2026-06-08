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

            // Path for the rendered image output
            string outputPath = "output.png";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and hide every comment
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation comment in page.PageSheet.Annotations)
                {
                    // Clear the comment text to effectively hide it
                    comment.Comment.Value = "";
                }
            }

            // Configure image export options without comment overlays
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            options.IsExportComments = false; // Do not render comments

            // Save the diagram as an image
            diagram.Save(outputPath, options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
