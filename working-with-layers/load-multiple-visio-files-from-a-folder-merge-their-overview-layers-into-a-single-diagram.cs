using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing the Visio files to process
            string folderPath = @"C:\VisioFiles";

            // Get all Visio files (VSDX format) in the folder
            string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx");
            if (visioFiles.Length == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            // Create an empty diagram that will hold the merged result
            Diagram mergedDiagram = new Diagram();

            // Load each file and combine it into the merged diagram
            foreach (string filePath in visioFiles)
            {
                Console.WriteLine($"Loading and combining: {Path.GetFileName(filePath)}");
                Diagram sourceDiagram = new Diagram(filePath);
                mergedDiagram.Combine(sourceDiagram);
            }

            // After combining all diagrams, hide every layer that is not named "Overview"
            foreach (Page page in mergedDiagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Set visibility based on layer name
                    if (layer.Name.Value == "Overview")
                    {
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Save the merged diagram with only the Overview layers visible
            string outputPath = Path.Combine(folderPath, "MergedOverview.vsdx");
            mergedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
    }