using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio diagram files (e.g., .vsdx, .vsd, .vdx)
            string diagramsFolder = @"C:\Diagrams";

            if (!Directory.Exists(diagramsFolder))
            {
                Console.WriteLine($"Folder not found: {diagramsFolder}");
                return;
            }

            // Get all supported Visio files in the folder
            string[] supportedExtensions = new[] { "*.vsdx", "*.vsd", "*.vdx", "*.vsx", "*.vtx", "*.vssx", "*.vstx" };
            List<string> diagramFiles = new List<string>();
            foreach (var ext in supportedExtensions)
            {
                diagramFiles.AddRange(Directory.GetFiles(diagramsFolder, ext, SearchOption.TopDirectoryOnly));
            }

            if (diagramFiles.Count == 0)
            {
                Console.WriteLine("No diagram files found in the specified folder.");
                return;
            }

            Console.WriteLine("Diagram Inventory Report");
            Console.WriteLine("------------------------");
            Console.WriteLine($"{"File Name",-40} {"Custom Props Count",15}");
            Console.WriteLine(new string('-', 58));

            foreach (var filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Retrieve the count of custom document properties
                    int customPropCount = diagram.DocumentProps.CustomProps.Count;

                    // Output the result
                    Console.WriteLine($"{Path.GetFileName(filePath),-40} {customPropCount,15}");
                }
                catch (Exception ex)
                {
                    // Report any loading errors but continue processing other files
                    Console.WriteLine($"{Path.GetFileName(filePath),-40} Error: {ex.Message}");
                }
            }

            Console.WriteLine(new string('-', 58));
            Console.WriteLine("Report generation completed.");
        }
    }