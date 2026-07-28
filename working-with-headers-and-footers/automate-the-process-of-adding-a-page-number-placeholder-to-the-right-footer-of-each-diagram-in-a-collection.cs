using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Prompt user for the folder containing Visio diagram files
            Console.WriteLine("Enter the full path to the folder containing Visio diagrams (e.g., C:\\Diagrams):");
            string folderPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                Console.WriteLine("Invalid folder path. Exiting.");
                return;
            }

            // Get all Visio files with .vsdx extension in the specified folder
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            if (diagramFiles.Length == 0)
            {
                Console.WriteLine("No .vsdx files found in the specified folder.");
                return;
            }

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(filePath);

                    // Set the right footer to display the page number placeholder
                    // '&p' is the Visio field code for the current page number
                    diagram.HeaderFooter.FooterRight = "Page: &p";

                    // Save the diagram back to the same file (overwrites original)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Updated footer for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    // Report any errors but continue processing other files
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Footer update operation completed.");
        }
    }