using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Export the entire diagram to HTML
            diagram.Save(outputPath, SaveFileFormat.Html);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        try
        {
            // Read the generated HTML
            string htmlContent = File.ReadAllText(outputPath);

            // JavaScript to handle shape interactions (click, mouseover, mouseout)
            string script = @"
<script type=""text/javascript"">
    document.addEventListener('DOMContentLoaded', function () {
        // Attach event listeners to all shape elements (assumed to have class 'shape')
        var shapes = document.querySelectorAll('.shape');
        shapes.forEach(function (shape) {
            shape.addEventListener('click', function () {
                alert('Shape clicked: ' + (shape.id || 'unknown'));
            });
            shape.addEventListener('mouseover', function () {
                shape.style.outline = '2px solid red';
            });
            shape.addEventListener('mouseout', function () {
                shape.style.outline = '';
            });
        });
    });
</script>
";

            // Insert the script before the closing </body> tag
            int bodyCloseIndex = htmlContent.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            if (bodyCloseIndex >= 0)
            {
                htmlContent = htmlContent.Insert(bodyCloseIndex, script);
            }
            else
            {
                // If </body> not found, append at the end
                htmlContent += script;
            }

            // Write the modified HTML back to the output file
            File.WriteAllText(outputPath, htmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while injecting JavaScript: {ex.Message}");
        }
    }
}