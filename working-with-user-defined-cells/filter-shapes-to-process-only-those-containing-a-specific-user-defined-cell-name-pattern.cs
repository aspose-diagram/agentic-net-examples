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
                // Path to the output Visio file after processing
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the user‑defined cell name pattern to look for
                string cellNamePattern = "MyCell";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check each user‑defined cell in the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Match the cell name (Name or NameU) against the pattern
                            if (!string.IsNullOrEmpty(userCell.Name) && userCell.Name.Contains(cellNamePattern) ||
                                !string.IsNullOrEmpty(userCell.NameU) && userCell.NameU.Contains(cellNamePattern))
                            {
                                // Example processing: output shape information
                                Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}, User Cell: {userCell.NameU}");

                                // Example modification: set the shape fill foreground color to red
                                shape.Fill.FillForegnd.Value = "#FF0000";

                                // Once a matching user cell is found, no need to check other cells for this shape
                                break;
                            }
                        }
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