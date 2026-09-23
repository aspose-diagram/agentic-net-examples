using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.WriteLine("The specified folder does not exist.");
            return;
        }

        // Collect Visio files with common extensions
        string[] searchPatterns = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vtx", "*.vssx", "*.vstx", "*.vsdm", "*.vssm", "*.vstm" };
        var visioFiles = new List<string>();
        foreach (string pattern in searchPatterns)
        {
            visioFiles.AddRange(Directory.GetFiles(folderPath, pattern));
        }

        if (visioFiles.Count == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Process each file: set all pages to Landscape orientation and save
        foreach (string filePath in visioFiles)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply Landscape orientation to every page
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Save the diagram back to the same file (overwrites original)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully updated: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }
}
