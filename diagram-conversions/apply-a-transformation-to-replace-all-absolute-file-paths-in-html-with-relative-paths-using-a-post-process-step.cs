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

                // Input Visio file path
                string visioPath = @"C:\Input\sample.vsdx";

                // Output HTML file path
                string htmlOutputPath = @"C:\Output\sample.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Configure HTML save options (default settings)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Save the diagram as HTML
                diagram.Save(htmlOutputPath, htmlOptions);

                // Post‑process the generated HTML to replace absolute file paths with relative paths
                try
                {
                    string htmlContent = File.ReadAllText(htmlOutputPath);

                    // Replace Windows absolute paths (e.g., C:\folder\file.png) with just the file name
                    string processedContent = ReplaceAbsolutePaths(htmlContent);

                    // Overwrite the HTML file with the processed content
                    File.WriteAllText(htmlOutputPath, processedContent);
                }
                catch (Exception ex)
                {
                    // Propagate any errors
                    throw new Exception("Error processing HTML file: " + ex.Message, ex);
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Replaces absolute Windows file paths in the HTML content with relative file names.
        /// </summary>
        /// <param name="html">The original HTML content.</param>
        /// <returns>HTML content with absolute paths replaced by relative file names.</returns>
        private static string ReplaceAbsolutePaths(string html)
        {
            // Pattern matches paths like C:\folder\subfolder\file.ext
            string pattern = @"[A-Za-z]:\\[^\s""']+";

            return Regex.Replace(html, pattern, match =>
            {
                // Extract only the file name from the matched absolute path
                string fileName = Path.GetFileName(match.Value);
                return fileName ?? match.Value;
            }, RegexOptions.IgnoreCase);
        }
    }