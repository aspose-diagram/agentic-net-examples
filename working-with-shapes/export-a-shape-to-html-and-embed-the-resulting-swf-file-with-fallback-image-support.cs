using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.IO;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Assume we work with the first page and the first shape on that page
                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("No shapes found on the first page.");
                }

                // Retrieve the shape (using the first shape's ID)
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Export the shape to a PNG image (fallback image)
                string pngPath = "shape.png";
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                shape.ToImage(pngPath, pngOptions);

                // Export the entire diagram to SWF (the SWF will contain the shape)
                string swfPath = "diagram.swf";
                diagram.Save(swfPath, SaveFileFormat.Swf);

                // Create an HTML file that embeds the SWF with a fallback PNG image
                string htmlPath = "shape.html";
                using (StreamWriter writer = new StreamWriter(htmlPath))
                {
                    writer.WriteLine("<!DOCTYPE html>");
                    writer.WriteLine("<html lang=\"en\">");
                    writer.WriteLine("<head>");
                    writer.WriteLine("    <meta charset=\"UTF-8\">");
                    writer.WriteLine("    <title>Shape Export</title>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine("    <!-- Embed SWF with fallback PNG -->");
                    writer.WriteLine("    <object data=\"diagram.swf\" type=\"application/x-shockwave-flash\" width=\"800\" height=\"600\">");
                    writer.WriteLine("        <img src=\"shape.png\" alt=\"Shape fallback image\" width=\"800\" height=\"600\" />");
                    writer.WriteLine("    </object>");
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }

                Console.WriteLine("Export completed:");
                Console.WriteLine($"  PNG fallback image: {pngPath}");
                Console.WriteLine($"  SWF file: {swfPath}");
                Console.WriteLine($"  HTML file: {htmlPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }