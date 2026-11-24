using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for HTML files (second argument or default)
        string outputDir = args.Length > 1 ? args[1] : "output_html";

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Check if the diagram contains any pages
                if (diagram.Pages == null || diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages to export.");
                    return;
                }

                // Export each page as a separate HTML file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure HTML save options for a single page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.PageIndex = i;
                    htmlOptions.PageCount = 1;

                    // Build the output file name for the current page
                    string pageFileName = $"page_{i + 1}.html";
                    string pageFilePath = Path.Combine(outputDir, pageFileName);

                    // Save the page to HTML
                    try
                    {
                        diagram.Save(pageFilePath, htmlOptions);
                        Console.WriteLine($"Exported page {i + 1} to {pageFilePath}");
                    }
                    catch (Exception saveEx)
                    {
                        Console.Error.WriteLine($"Failed to export page {i + 1}: {saveEx.Message}");
                    }
                }

                // Generate an index HTML file with embedded CSS for navigation
                try
                {
                    StringBuilder indexBuilder = new StringBuilder();

                    // Basic HTML structure with embedded CSS
                    indexBuilder.AppendLine("<!DOCTYPE html>");
                    indexBuilder.AppendLine("<html lang=\"en\">");
                    indexBuilder.AppendLine("<head>");
                    indexBuilder.AppendLine("    <meta charset=\"UTF-8\">");
                    indexBuilder.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                    indexBuilder.AppendLine("    <title>Diagram HTML Navigation</title>");
                    indexBuilder.AppendLine("    <style>");
                    indexBuilder.AppendLine("        body { font-family: Arial, Helvetica, sans-serif; margin: 0; padding: 0; }");
                    indexBuilder.AppendLine("        .navbar { background-color: #333; overflow: hidden; }");
                    indexBuilder.AppendLine("        .navbar a { float: left; display: block; color: #f2f2f2;");
                    indexBuilder.AppendLine("                     text-align: center; padding: 14px 16px;");
                    indexBuilder.AppendLine("                     text-decoration: none; }");
                    indexBuilder.AppendLine("        .navbar a:hover { background-color: #ddd; color: black; }");
                    indexBuilder.AppendLine("        .content { padding: 20px; }");
                    indexBuilder.AppendLine("    </style>");
                    indexBuilder.AppendLine("</head>");
                    indexBuilder.AppendLine("<body>");
                    indexBuilder.AppendLine("    <div class=\"navbar\">");

                    // Add navigation links for each page
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        string pageFileName = $"page_{i + 1}.html";
                        indexBuilder.AppendLine($"        <a href=\"{pageFileName}\">Page {i + 1}</a>");
                    }

                    indexBuilder.AppendLine("    </div>");
                    indexBuilder.AppendLine("    <div class=\"content\">");
                    indexBuilder.AppendLine("        <h1>Diagram HTML Export</h1>");
                    indexBuilder.AppendLine("        <p>Select a page from the navigation bar to view the diagram.</p>");
                    indexBuilder.AppendLine("    </div>");
                    indexBuilder.AppendLine("</body>");
                    indexBuilder.AppendLine("</html>");

                    // Write the index.html file
                    string indexPath = Path.Combine(outputDir, "index.html");
                    File.WriteAllText(indexPath, indexBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine($"Generated navigation index at {indexPath}");
                }
                catch (Exception indexEx)
                {
                    Console.Error.WriteLine($"Failed to generate index.html: {indexEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}