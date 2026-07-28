using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. Use the first argument if provided, otherwise use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Get all Visio files in the folder (common extensions). Adjust the pattern list as needed.
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Process only files with known Visio extensions.
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply a uniform header margin of 0.25 inches.
                    diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                    // Save the diagram back to the same file, using the appropriate format.
                    if (extension == ".vsdx")
                    {
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                    }
                    else if (extension == ".vsd")
                    {
                        diagram.Save(filePath, SaveFileFormat.Vsd);
                    }
                    else // .vdx
                    {
                        diagram.Save(filePath, SaveFileFormat.Vdx);
                    }

                    Console.WriteLine($"Successfully updated header margin for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    // Log any failures without stopping the batch process.
                    Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}. Error: {ex.Message}");
                }
            }
        }
    }