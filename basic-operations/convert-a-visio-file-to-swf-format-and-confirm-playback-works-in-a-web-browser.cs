using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (can be .vsdx, .vsd, etc.)
                string visioPath = @"C:\Input\sample.vsdx";

                // Output SWF file path
                string swfPath = @"C:\Output\sample.swf";

                // Load the Visio diagram (create/load rule)
                Diagram diagram = new Diagram(visioPath);

                // Save the diagram as SWF (save rule)
                diagram.Save(swfPath, SaveFileFormat.Swf);

                // Generate a simple HTML page to test playback in a web browser
                string htmlPath = Path.Combine(Path.GetDirectoryName(swfPath), "test.html");
                string htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                <title>Visio SWF Playback Test</title>
                </head>
                <body>
                <h2>SWF Playback Test</h2>
                <!-- Embed the generated SWF file -->
                <object width=""800"" height=""600"" data=""{Path.GetFileName(swfPath)}"" type=""application/x-shockwave-flash"">
                <param name=""movie"" value=""{Path.GetFileName(swfPath)}"" />
                <param name=""quality"" value=""high"" />
                <embed src=""{Path.GetFileName(swfPath)}"" width=""800"" height=""600"" quality=""high"" type=""application/x-shockwave-flash""></embed>
                Your browser does not support SWF playback.
                </object>
                </body>
                </html>";

                File.WriteAllText(htmlPath, htmlContent);

                Console.WriteLine("Conversion completed.");
                Console.WriteLine($"SWF file saved at: {swfPath}");
                Console.WriteLine($"HTML test page created at: {htmlPath}");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }