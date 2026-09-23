using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Specify the folder containing Visio files.
            // You can change this path as needed.
            string folderPath = @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Process all Visio files in the folder (common extensions).
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                // Skip non-Visio files.
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsdm" && ext != ".vssx")
                {
                    continue;
                }

                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Ensure the diagram has at least one page.
                    if (diagram.Pages.Count > 0)
                    {
                        // Access the first page (index 0) and set its height to 14 inches.
                        Page firstPage = diagram.Pages[0];
                        firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;
                    }
                    else
                    {
                        Console.WriteLine($"No pages found in file: {filePath}");
                        continue;
                    }

                    // Save the diagram back to the same file, overwriting it.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed file: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }