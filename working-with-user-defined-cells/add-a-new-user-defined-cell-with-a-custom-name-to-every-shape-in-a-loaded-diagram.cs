using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramUserCellAdder <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the existing Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new user‑defined cell.
                        User customCell = new User();
                        customCell.Name = "MyCustomCell";          // Custom name for the cell.
                        customCell.Value.Val = "CustomValue";      // Cell value (string).

                        // Add the custom cell to the shape's Users collection.
                        shape.Users.Add(customCell);
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }