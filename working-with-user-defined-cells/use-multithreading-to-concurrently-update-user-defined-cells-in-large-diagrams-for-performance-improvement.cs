using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect pages into a list for Parallel.ForEach (type inference does not work directly on diagram.Pages)
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Update user-defined cells concurrently
                Parallel.ForEach(pages, page =>
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate over all user-defined cells of the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Example: update a cell named "MyCell"
                            if (userCell.Name == "MyCell")
                            {
                                // Set the new value; adjust logic as required
                                userCell.Value.Val = "UpdatedValue";
                            }
                        }
                    }
                });

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }