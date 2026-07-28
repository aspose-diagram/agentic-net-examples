using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Specify the folder containing Visio files
            string folderPath = @"C:\VisioFiles";

            // Validate folder existence
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Get all Visio files in the folder (common extensions)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                // Process only known Visio extensions
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vssx" && extension != ".vss" && extension != ".vstx" && extension != ".vst")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Ensure there is at least one page
                    if (diagram.Pages.Count > 0)
                    {
                        // Access the first page (index 0)
                        Page firstPage = diagram.Pages[0];

                        // Set the page height to 14 inches
                        firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;

                        // Save the diagram back to the original file (preserving format)
                        // For simplicity, save as Vsdx; adjust if needed for other formats
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                    }
                    else
                    {
                        Console.WriteLine($"No pages found in: {Path.GetFileName(filePath)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }