using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output HTML file path (second argument)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputHtmlPath = args.Length > 1 ? args[1] : "shape.html";

        // Guard: ensure the input file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page
            Shape shape = page.Shapes[0];

            // Export the shape to an HTML file using default HTMLSaveOptions
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            shape.ToHTML(outputHtmlPath, htmlOptions);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // JavaScript to handle shape interaction (e.g., click event)
            string script = @"
<script type=""text/javascript"">
    // Simple click handler that shows an alert
    function onShapeClick() {
        alert('Shape clicked!');
    }

    // Attach the click handler after the DOM is loaded
    document.addEventListener('DOMContentLoaded', function () {
        // Attempt to find the first <svg> element (Visio shapes are rendered as SVG)
        var svg = document.querySelector('svg');
        if (svg) {
            svg.style.cursor = 'pointer';
            svg.addEventListener('click', onShapeClick);
        }
    });
</script>
";

            // Insert the script before the closing </body> tag if present, otherwise append at the end
            string modifiedHtml;
            int bodyCloseIndex = htmlContent.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            if (bodyCloseIndex >= 0)
            {
                modifiedHtml = htmlContent.Insert(bodyCloseIndex, script);
            }
            else
            {
                modifiedHtml = htmlContent + script;
            }

            // Write the modified HTML back to the file
            File.WriteAllText(outputHtmlPath, modifiedHtml);

            Console.WriteLine($"HTML file with JavaScript handlers generated successfully at '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}