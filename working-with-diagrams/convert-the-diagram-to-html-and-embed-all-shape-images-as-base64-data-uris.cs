using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-directory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length >= 2 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "HtmlOutput");

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // Iterate over each page and export it to a separate HTML file
                for (int i = 0; i < pageCount; i++)
                {
                    // Configure HTML export options for a single page
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.PageIndex = i;   // zero‑based page index
                    htmlOptions.PageCount = 1;   // export only this page

                    // Define HTML file path
                    string htmlFileName = $"page_{i + 1}.html";
                    string htmlPath = Path.Combine(outputDir, htmlFileName);

                    // Export the page to HTML
                    diagram.Save(htmlPath, htmlOptions);

                    // After export, embed any generated PNG images as base64 data URIs
                    EmbedImagesInHtml(htmlPath, outputDir, i + 1);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Finds PNG images generated for a specific page, converts them to base64,
    /// replaces the src attributes in the HTML file, and optionally deletes the PNG files.
    /// </summary>
    /// <param name="htmlPath">Full path to the generated HTML file.</param>
    /// <param name="outputDir">Directory where the HTML and image files reside.</param>
    /// <param name="pageNumber">One‑based page number used in naming convention.</param>
    private static void EmbedImagesInHtml(string htmlPath, string outputDir, int pageNumber)
    {
        // Read the HTML content
        string htmlContent = File.ReadAllText(htmlPath, Encoding.UTF8);

        // Determine image file naming pattern for this page (e.g., page_1_*.png)
        string imagePrefix = $"page_{pageNumber}_";
        string[] imageFiles = Directory.GetFiles(outputDir, $"{imagePrefix}*.png");

        foreach (string imagePath in imageFiles)
        {
            // Read image bytes and convert to base64
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64 = Convert.ToBase64String(imageBytes);
            string dataUri = $"data:image/png;base64,{base64}";

            // Extract just the file name to match the src attribute in HTML
            string imageFileName = Path.GetFileName(imagePath);

            // Replace src="imageFileName" with src="dataUri"
            // Simple string replace; assumes double quotes around src value
            string srcPattern = $"src=\"{imageFileName}\"";
            string replacement = $"src=\"{dataUri}\"";
            htmlContent = htmlContent.Replace(srcPattern, replacement);
        }

        // Write the modified HTML back to the file
        File.WriteAllText(htmlPath, htmlContent, Encoding.UTF8);

        // Optionally delete the now‑embedded PNG files
        foreach (string imagePath in imageFiles)
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // If deletion fails, ignore and continue
            }
        }
    }
}