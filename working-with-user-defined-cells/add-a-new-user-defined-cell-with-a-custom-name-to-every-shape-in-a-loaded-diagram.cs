using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new user‑defined cell
                        User customCell = new User();
                        customCell.Name = "MyCustomCell";          // Custom cell name
                        customCell.Value.Val = "CustomValue";      // Cell value (string)

                        // Add the custom cell to the shape's Users collection
                        shape.Users.Add(customCell);
                    }
                }

                // Save the modified diagram back to a file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }