using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string inputPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Users collection
                        if (shape.Users != null)
                        {
                            // Iterate through all user‑defined cells of the shape
                            foreach (User userCell in shape.Users)
                            {
                                // Output the page name, shape ID, user cell name and its value
                                Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, User Cell: {userCell.NameU}, Value: {userCell.Value.Val}");
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }