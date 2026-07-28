using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare a typed list of pages for parallel processing
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                    pages.Add(p);

                // Parallel update of user-defined cells
                Parallel.ForEach(pages, page =>
                {
                    // Collect shapes of the current page
                    List<Shape> shapes = new List<Shape>();
                    foreach (Shape s in page.Shapes)
                        shapes.Add(s);

                    // Update each shape's user-defined cells concurrently
                    Parallel.ForEach(shapes, shape =>
                    {
                        // Example: update a user-defined cell named "CustomValue"
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name == "CustomValue")
                            {
                                // Set new value based on shape ID (as a string)
                                userCell.Value.Val = (shape.ID * 10).ToString();
                            }
                        }
                    });
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