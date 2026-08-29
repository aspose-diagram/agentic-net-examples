using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsv = "UserDefinedCells.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create a StreamWriter for the CSV file
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write CSV header
                    writer.WriteLine("PageIndex,ShapeID,ShapeName,UserName,UserValue");

                    // Iterate through all pages
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Iterate through user-defined cells (Users collection)
                            foreach (User userCell in shape.Users)
                            {
                                // Write a CSV line with shape identifier and user cell data
                                writer.WriteLine($"{pageIndex},{shape.ID},{shape.NameU},{userCell.Name},{userCell.Value.Val}");
                            }
                        }
                    }
                }

                Console.WriteLine($"User-defined cell data exported to '{outputCsv}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }