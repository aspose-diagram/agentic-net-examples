using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output diagram path (VDX format)
        string outputPath = "output.vdx";
        // Name of the user-defined cell to update
        string targetCellName = "MyUserCell";
        // New formula to assign to the cell
        string newFormula = "\"Updated Value\"";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool cellFound = false;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through user-defined cells of the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Match by Name or NameU (case-sensitive)
                            if (userCell.Name == targetCellName || userCell.NameU == targetCellName)
                            {
                                // Update the formula/value of the user-defined cell
                                userCell.Value.Val = newFormula;
                                Console.WriteLine($"Updated cell '{targetCellName}' in shape ID {shape.ID} on page {page.ID}.");
                                cellFound = true;
                                // If only one occurrence is expected, break out of loops
                                break;
                            }
                        }

                        if (cellFound) break;
                    }

                    if (cellFound) break;
                }

                if (!cellFound)
                {
                    Console.Error.WriteLine($"User-defined cell '{targetCellName}' not found in the diagram.");
                }

                // Save the modified diagram in VDX format
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}