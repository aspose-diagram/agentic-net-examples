using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                double totalCost = 0.0;
                int shapesWithCost = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Attempt to find a user‑defined cell named "Cost"
                        User costCell = null;
                        foreach (User userCell in shape.Users)
                        {
                            if (string.Equals(userCell.NameU, "Cost", StringComparison.OrdinalIgnoreCase))
                            {
                                costCell = userCell;
                                break;
                            }
                        }

                        // If the cell is missing, report the issue and continue
                        if (costCell == null)
                        {
                            Console.Error.WriteLine($"Warning: Shape ID {shape.ID} on page \"{page.Name}\" does not contain a \"Cost\" user‑defined cell.");
                            continue;
                        }

                        // Try to parse the cell value as a double for calculation
                        if (double.TryParse(costCell.Value.Val, out double costValue))
                        {
                            totalCost += costValue;
                            shapesWithCost++;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Error: Invalid numeric value \"{costCell.Value.Val}\" in \"Cost\" cell of shape ID {shape.ID}.");
                        }
                    }
                }

                // Output the calculation result
                Console.WriteLine($"Processed {shapesWithCost} shapes with a valid \"Cost\" cell.");
                Console.WriteLine($"Total Cost: {totalCost}");

                // Save the (unchanged) diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}