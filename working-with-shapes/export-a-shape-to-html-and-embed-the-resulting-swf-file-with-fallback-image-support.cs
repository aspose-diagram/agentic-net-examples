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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define output file names
            string swfPath = "diagram.swf";
            string fallbackImagePath = "diagram.png";
            string htmlPath = "diagram.html";

            // Export the diagram to SWF format
            diagram.Save(swfPath, SaveFileFormat.Swf);

            // Export the diagram to PNG format for fallback image
            diagram.Save(fallbackImagePath, SaveFileFormat.Png);

            // Build HTML that embeds the SWF with a fallback PNG image
            string htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset=""utf-8"" />
            <title>Visio Diagram Export</title>
            </head>
            <body>
            <object type=""application/x-shockwave-flash"" data=""{Path.GetFileName(swfPath)}"" width=""800"" height=""600"">
            <param name=""movie"" value=""{Path.GetFileName(swfPath)}"" />
            <!-- Fallback image for browsers that do not support Flash -->
            <img src=""{Path.GetFileName(fallbackImagePath)}"" alt=""Visio diagram fallback image"" width=""800"" height=""600"" />
            Your browser does not support Flash.
            </object>
            </body>
            </html>";

            // Write the HTML to disk
            File.WriteAllText(htmlPath, htmlContent);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
