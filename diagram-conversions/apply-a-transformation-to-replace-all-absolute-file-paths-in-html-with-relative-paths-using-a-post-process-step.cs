using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (absolute or relative)
                string visioPath = @"C:\Diagrams\sample.vsdx";

                // Output HTML file path
                string htmlPath = @"C:\Diagrams\output.html";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Export diagram to HTML
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);

                // Post‑process the generated HTML to replace absolute file paths with relative paths
                string htmlContent = File.ReadAllText(htmlPath);

                // Regex to match Windows absolute paths (e.g., C:\folder\file.png)
                string pattern = @"[A-Za-z]:\\[^\s""']+";

                // Replace each absolute path with just the file name (relative to the HTML file)
                string processedContent = Regex.Replace(htmlContent, pattern, match =>
                {
                    // Extract the file name from the absolute path
                    string fileName = Path.GetFileName(match.Value);
                    // Return the relative path (just the file name)
                    return fileName;
                }, RegexOptions.IgnoreCase);

                // Write the transformed HTML back to the file
                File.WriteAllText(htmlPath, processedContent);

                Console.WriteLine("HTML export completed and absolute paths have been converted to relative paths.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }