using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Guard: create the output directory if it does not exist.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Could not create output directory \"{outputDir}\": {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // CSS that will be embedded into each generated HTML page.
            string embeddedCss = @"
<style>
/* Embedded CSS for interactive navigation */
.nav { margin: 10px; }
.nav a { margin-right: 5px; text-decoration: none; color: #0066cc; }
.nav a:hover { text-decoration: underline; }
</style>
";

            // Export each page as a separate HTML file and inject the CSS.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure HTML save options for a single page.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.PageIndex = i;   // zero‑based page index
                htmlOptions.PageCount = 1;   // export only this page

                // Build the output file name for the current page.
                string pageFileName = Path.Combine(outputDir, $"page_{i + 1}.html");

                // Save the page as HTML.
                diagram.Save(pageFileName, htmlOptions);

                // Read the generated HTML, inject the CSS after the <head> tag, and overwrite the file.
                string htmlContent = File.ReadAllText(pageFileName);
                int headCloseIndex = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
                if (headCloseIndex >= 0)
                {
                    htmlContent = htmlContent.Insert(headCloseIndex, embeddedCss);
                    File.WriteAllText(pageFileName, htmlContent);
                }
                else
                {
                    // If </head> is not found, prepend the CSS at the beginning.
                    htmlContent = embeddedCss + htmlContent;
                    File.WriteAllText(pageFileName, htmlContent);
                }
            }

            // Create a simple index HTML that provides navigation between pages.
            string indexPath = Path.Combine(outputDir, "index.html");
            using (StreamWriter writer = new StreamWriter(indexPath))
            {
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta charset=\"UTF-8\">");
                writer.WriteLine("<title>Diagram HTML Navigation</title>");
                writer.WriteLine(embeddedCss); // reuse the same CSS
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<h1>Diagram Pages</h1>");
                writer.WriteLine("<div class=\"nav\">");
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    string pageLink = $"page_{i + 1}.html";
                    writer.WriteLine($"<a href=\"{pageLink}\">Page {i + 1}</a>");
                }
                writer.WriteLine("</div>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }

            Console.WriteLine("HTML export completed successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}