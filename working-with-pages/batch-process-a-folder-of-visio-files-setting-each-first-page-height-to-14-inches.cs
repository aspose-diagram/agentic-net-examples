using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Define the folder containing Visio files.
            // Adjust the path as needed or pass it via command line arguments.
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Process each Visio file in the folder (common Visio extensions).
            string[] visioExtensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vssx", "*.vss", "*.vstx", "*.vst" };
            foreach (string extension in visioExtensions)
            {
                string[] files = Directory.GetFiles(folderPath, extension, SearchOption.TopDirectoryOnly);
                foreach (string filePath in files)
                {
                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(filePath);

                        // Ensure there is at least one page.
                        if (diagram.Pages.Count > 0)
                        {
                            // Access the first page (index 0).
                            Page firstPage = diagram.Pages[0];

                            // Set the page height to 14 inches.
                            firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;

                            // Save the diagram back to the original file.
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
            }

            Console.WriteLine("Batch processing completed.");
        }
    }