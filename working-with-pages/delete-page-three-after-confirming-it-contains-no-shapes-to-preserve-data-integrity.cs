using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Verify that the diagram has at least three pages
                    if (diagram.Pages.Count < 3)
                    {
                        throw new Exception("The diagram does not contain a third page to delete.");
                    }

                    // Retrieve the third page (zero‑based index 2)
                    Page pageToDelete = diagram.Pages[2];

                    // Confirm the page contains no shapes
                    if (pageToDelete.Shapes.Count > 0)
                    {
                        throw new Exception("Page three contains shapes and cannot be deleted to preserve data integrity.");
                    }

                    // Remove the page from the diagram
                    diagram.Pages.Remove(pageToDelete);

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page three was successfully removed and the diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }