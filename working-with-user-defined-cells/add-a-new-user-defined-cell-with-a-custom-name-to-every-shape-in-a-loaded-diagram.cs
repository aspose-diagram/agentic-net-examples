using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the custom user-defined cell name and a default value
                const string customCellName = "MyCustomCell";
                const string defaultCellValue = "CustomValue";

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new User cell
                        User userCell = new User();
                        userCell.Name = customCellName;          // Set the custom name
                        userCell.Value.Val = defaultCellValue;   // Set the cell value

                        // Add the user-defined cell to the shape's Users collection
                        shape.Users.Add(userCell);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }