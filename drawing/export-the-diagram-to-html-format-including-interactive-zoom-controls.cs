using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (command‑line args or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.html";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.ExportHiddenPage = false;   // Do not export hidden pages (use C# bool)
            htmlOptions.IsExportComments = false;   // Do not export comments (use C# bool)

            // Export the diagram to a single HTML file
            diagram.Save(outputPath, htmlOptions);

            // -----------------------------------------------------------------
            // Inject simple interactive zoom controls into the generated HTML
            // -----------------------------------------------------------------
            string htmlContent = File.ReadAllText(outputPath, Encoding.UTF8);

            // JavaScript and HTML for zoom in/out buttons
            string zoomControls = @"
<div style='margin:10px;'>
    <button onclick='zoomIn()'>Zoom In</button>
    <button onclick='zoomOut()'>Zoom Out</button>
</div>
<script>
    var currentScale = 1.0;
    function applyZoom() {
        var imgs = document.getElementsByTagName('img');
        for (var i = 0; i < imgs.length; i++) {
            imgs[i].style.transform = 'scale(' + currentScale + ')';
            imgs[i].style.transformOrigin = '0 0';
        }
    }
    function zoomIn() {
        currentScale += 0.1;
        applyZoom();
    }
    function zoomOut() {
        if (currentScale > 0.2) {
            currentScale -= 0.1;
            applyZoom();
        }
    }
</script>
";

            // Insert the zoom controls right after the opening <body> tag
            int bodyTagPos = htmlContent.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
            if (bodyTagPos >= 0)
            {
                int insertPos = bodyTagPos + "<body>".Length;
                htmlContent = htmlContent.Insert(insertPos, zoomControls);
            }
            else
            {
                // Fallback: prepend controls if <body> tag is not found
                htmlContent = zoomControls + htmlContent;
            }

            // Write the modified HTML back to the output file
            File.WriteAllText(outputPath, htmlContent, Encoding.UTF8);

            Console.WriteLine($"Diagram successfully exported to HTML with zoom controls: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
