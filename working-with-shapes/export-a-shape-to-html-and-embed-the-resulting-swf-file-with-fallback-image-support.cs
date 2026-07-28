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

                // Load the Visio diagram from file
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page and the first shape on that page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes[0];

                // Export the shape to HTML (this also creates associated SWF and PNG files)
                string htmlFilePath = "shape.html";
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                shape.ToHTML(htmlFilePath, htmlOptions);

                // Build a custom HTML snippet that embeds the generated SWF with a PNG fallback
                string swfFileName = Path.GetFileNameWithoutExtension(htmlFilePath) + ".swf";
                string pngFileName = Path.GetFileNameWithoutExtension(htmlFilePath) + ".png";

                string embeddedHtml = $@"<!DOCTYPE html>
                <html>
                <head>
                <meta charset=""UTF-8"">
                <title>Shape Export with SWF Fallback</title>
                </head>
                <body>
                <object data=""{swfFileName}"" type=""application/x-shockwave-flash"" width=""800"" height=""600"">
                <img src=""{pngFileName}"" alt=""Shape image fallback"" width=""800"" height=""600"" />
                </object>
                </body>
                </html>";

                // Write the custom HTML to a file
                string outputHtmlPath = "shape_with_fallback.html";
                File.WriteAllText(outputHtmlPath, embeddedHtml);

                Console.WriteLine($"Shape exported to HTML: {htmlFilePath}");
                Console.WriteLine($"Custom HTML with SWF fallback created: {outputHtmlPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }