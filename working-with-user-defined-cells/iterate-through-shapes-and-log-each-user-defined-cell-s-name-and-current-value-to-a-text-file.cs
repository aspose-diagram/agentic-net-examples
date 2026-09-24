using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string diagramPath = "input.vsdx";

                // Path to the output text file
                string outputPath = "UserCellsLog.txt";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Open a StreamWriter to write the log
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Iterate through all user-defined cells of the shape
                            foreach (User userCell in shape.Users)
                            {
                                // Log the cell name and its current value
                                writer.WriteLine($"{userCell.Name}: {userCell.Value.Val}");
                            }
                        }
                    }
                }

                Console.WriteLine($"User-defined cells have been logged to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }