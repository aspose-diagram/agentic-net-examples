using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";

                // Output HTML file path
                string htmlPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Export diagram to HTML (images will be saved as separate files)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);

                // Directory containing the generated HTML and image files
                string htmlDirectory = Path.GetDirectoryName(Path.GetFullPath(htmlPath)) ?? "";

                // Read the generated HTML content
                string htmlContent = File.ReadAllText(htmlPath, Encoding.UTF8);

                // Find all <img src="..."> occurrences
                Regex imgRegex = new Regex("<img[^>]+src=[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase);
                MatchCollection matches = imgRegex.Matches(htmlContent);

                foreach (Match match in matches)
                {
                    string srcValue = match.Groups[1].Value;
                    string imagePath = Path.Combine(htmlDirectory, srcValue);

                    if (!File.Exists(imagePath))
                    {
                        // If the image file does not exist, skip replacement
                        continue;
                    }

                    // Read image bytes and convert to Base64
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    string base64String = Convert.ToBase64String(imageBytes);

                    // Determine image MIME type based on file extension
                    string extension = Path.GetExtension(imagePath).TrimStart('.').ToLowerInvariant();
                    string mimeType = extension switch
                    {
                        "png" => "image/png",
                        "jpg" => "image/jpeg",
                        "jpeg" => "image/jpeg",
                        "gif" => "image/gif",
                        "bmp" => "image/bmp",
                        "svg" => "image/svg+xml",
                        _ => "application/octet-stream"
                    };

                    // Build data URI
                    string dataUri = $"data:{mimeType};base64,{base64String}";

                    // Replace the src attribute value with the data URI
                    htmlContent = htmlContent.Replace(srcValue, dataUri);
                }

                // Write the modified HTML back to the file (overwrites original)
                File.WriteAllText(htmlPath, htmlContent, Encoding.UTF8);

                Console.WriteLine("Diagram exported to HTML with embedded Base64 images.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }