using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToSwfConverter
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "sample.vsd";
            // Output SWF file path
            string outputSwf = "sample.swf";
            // HTML file that will embed the SWF for browser playback
            string htmlPath = "sample.html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SWF save options (include integrated viewer)
            SWFSaveOptions options = new SWFSaveOptions();
            options.ViewerIncluded = true; // default is true, set explicitly

            // Save the diagram as SWF using the configured options
            diagram.Save(outputSwf, options);

            // Generate a simple HTML page that embeds the SWF file
            string htmlContent = $@"<!DOCTYPE html>
            <html>
            <head>
            <title>Visio SWF Playback</title>
            </head>
            <body>
            <object width='100%' height='800' data='{outputSwf}' type='application/x-shockwave-flash'>
            <param name='movie' value='{outputSwf}' />
            <param name='play' value='true' />
            <param name='loop' value='false' />
            <param name='quality' value='high' />
            Your browser does not support SWF playback.
            </object>
            </body>
            </html>";

            // Write the HTML file to disk
            System.IO.File.WriteAllText(htmlPath, htmlContent);

            // Open the HTML file in the default web browser to verify playback
            Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
