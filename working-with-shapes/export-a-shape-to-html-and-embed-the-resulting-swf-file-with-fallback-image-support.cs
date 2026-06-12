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

                // Path to the source Visio file
                const string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example ID = 1)
                // Adjust the ID as needed for your diagram
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    throw new Exception("Shape with ID 1 not found on the first page.");
                }

                // Export the shape to HTML (this will generate an HTML file and a SWF file)
                string htmlFilePath = "shape.html";
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                shape.ToHTML(htmlFilePath, htmlOptions);

                // Export the same shape to a PNG image for fallback
                string pngFilePath = "shape.png";
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                shape.ToImage(pngFilePath, imgOptions);

                // Determine width and height in pixels (using 96 DPI as a common screen resolution)
                double widthInches = shape.XForm.Width.Value;
                double heightInches = shape.XForm.Height.Value;
                int widthPx = (int)(widthInches * 96);
                int heightPx = (int)(heightInches * 96);

                // Build HTML that embeds the SWF with a fallback PNG image
                string swfFileName = Path.GetFileNameWithoutExtension(htmlFilePath) + ".swf";
                string combinedHtml = $@"
                <!DOCTYPE html>
                <html>
                <head>
                <meta charset=""utf-8"" />
                <title>Shape Export with SWF and Fallback Image</title>
                </head>
                <body>
                <object data=""{swfFileName}"" type=""application/x-shockwave-flash"" width=""{widthPx}"" height=""{heightPx}"">
                <img src=""{Path.GetFileName(pngFilePath)}"" width=""{widthPx}"" height=""{heightPx}"" alt=""Shape fallback image"" />
                <p>Your browser does not support Flash. The fallback image is displayed above.</p>
                </object>
                </body>
                </html>";

                // Write the combined HTML to a new file
                string combinedHtmlPath = "shape_with_fallback.html";
                File.WriteAllText(combinedHtmlPath, combinedHtml);

                Console.WriteLine("Export completed:");
                Console.WriteLine($"- HTML file: {htmlFilePath}");
                Console.WriteLine($"- SWF file (generated alongside HTML): {swfFileName}");
                Console.WriteLine($"- PNG fallback image: {pngFilePath}");
                Console.WriteLine($"- Combined HTML with fallback: {combinedHtmlPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }