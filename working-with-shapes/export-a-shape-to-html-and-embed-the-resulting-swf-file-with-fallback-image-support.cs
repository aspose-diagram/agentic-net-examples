using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeNameU> <outputDirectory>");
            return;
        }

        string diagramPath = args[0];
        string shapeNameU = args[1];
        string outputDir = args[2];

        // Guard: diagram file must exist
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: output directory must exist or be created
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Locate the target shape by its NameU property
        Shape targetShape = null;
        try
        {
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes.GetShape(i);
                    if (shape != null && shape.NameU == shapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while searching for shape '{shapeNameU}': {ex.Message}");
            return;
        }

        if (targetShape == null)
        {
            Console.Error.WriteLine($"Shape with NameU '{shapeNameU}' not found in the diagram.");
            return;
        }

        // Export the shape to HTML
        string htmlPath = Path.Combine(outputDir, $"{shapeNameU}.html");
        try
        {
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            targetShape.ToHTML(htmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting shape to HTML: {ex.Message}");
            return;
        }

        // Export the shape to PNG (fallback image)
        string pngPath = Path.Combine(outputDir, $"{shapeNameU}.png");
        try
        {
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            targetShape.ToImage(pngPath, imgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting shape to PNG: {ex.Message}");
            return;
        }

        // Export the whole diagram to SWF
        string swfFileName = Path.GetFileNameWithoutExtension(diagramPath) + ".swf";
        string swfPath = Path.Combine(outputDir, swfFileName);
        try
        {
            diagram.Save(swfPath, SaveFileFormat.Swf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting diagram to SWF: {ex.Message}");
            return;
        }

        // Generate an HTML file that embeds the SWF with fallback PNG
        string embedHtmlPath = Path.Combine(outputDir, $"{shapeNameU}_embed.html");
        try
        {
            string embedHtmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Shape Export - {shapeNameU}</title>
</head>
<body>
    <!-- Embed SWF with PNG fallback -->
    <object type=""application/x-shockwave-flash"" data=""{Path.GetFileName(swfPath)}"" width=""800"" height=""600"">
        <param name=""movie"" value=""{Path.GetFileName(swfPath)}"" />
        <img src=""{Path.GetFileName(pngPath)}"" alt=""Fallback image for SWF"" width=""800"" height=""600"" />
        Your browser does not support Flash content.
    </object>
    <p>HTML representation of the shape is available <a href=""{Path.GetFileName(htmlPath)}"">here</a>.</p>
</body>
</html>";
            File.WriteAllText(embedHtmlPath, embedHtmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing embed HTML file: {ex.Message}");
            return;
        }

        Console.WriteLine("Export completed successfully.");
        Console.WriteLine($"HTML: {htmlPath}");
        Console.WriteLine($"PNG fallback: {pngPath}");
        Console.WriteLine($"SWF: {swfPath}");
        Console.WriteLine($"Embedded HTML: {embedHtmlPath}");
    }
}