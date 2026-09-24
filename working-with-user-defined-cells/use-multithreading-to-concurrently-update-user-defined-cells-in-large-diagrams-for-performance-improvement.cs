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

                // Input and output file paths
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

                // Parallel processing of pages
                Parallel.ForEach(pages, page =>
                {
                    // Collect shapes of the current page
                    List<Shape> shapes = new List<Shape>();
                    foreach (Shape s in page.Shapes)
                    {
                        shapes.Add(s);
                    }

                    // Parallel processing of shapes within the page
                    Parallel.ForEach(shapes, shape =>
                    {
                        // Iterate through user-defined cells of the shape
                        foreach (User user in shape.Users)
                        {
                            // Update the cell named "MyCell"
                            if (user.Name == "MyCell")
                            {
                                // Example update: set the value to the current timestamp ticks
                                user.Value.Val = DateTime.Now.Ticks.ToString();
                                break; // Exit after updating the target cell
                            }
                        }
                    });
                });

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }