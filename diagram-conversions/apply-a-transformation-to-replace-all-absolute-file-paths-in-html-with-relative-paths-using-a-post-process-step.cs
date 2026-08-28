using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (absolute)
                string visioPath = @"C:\Diagrams\sample.vsdx";

                // Output HTML file path (absolute)
                string htmlPath = @"C:\Diagrams\output.html";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Export diagram to HTML using default options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);

                // Post‑process the generated HTML to replace absolute file paths with relative paths
                // Read the HTML content
                string htmlContent = File.ReadAllText(htmlPath);

                // Determine the directory part of the HTML file path
                string baseDirectory = Path.GetDirectoryName(Path.GetFullPath(htmlPath));

                if (!string.IsNullOrEmpty(baseDirectory))
                {
                    // Ensure the directory ends with a separator for accurate replacement
                    string baseDirWithSeparator = baseDirectory.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

                    // Replace back‑slash absolute paths with relative paths
                    htmlContent = htmlContent.Replace(baseDirWithSeparator, string.Empty);

                    // Also replace forward‑slash variants (in case HTML uses them)
                    string baseDirWithForwardSlash = baseDirectory.Replace('\\', '/').TrimEnd('/') + "/";
                    htmlContent = htmlContent.Replace(baseDirWithForwardSlash, string.Empty);
                }

                // Write the modified HTML back to the file
                File.WriteAllText(htmlPath, htmlContent);

                Console.WriteLine("HTML export completed and absolute paths have been converted to relative paths.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }