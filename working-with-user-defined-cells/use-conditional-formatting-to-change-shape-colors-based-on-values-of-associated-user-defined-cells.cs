using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram (no LoadOptions needed)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Look for a user-defined cell named "Status"
                    foreach (User userCell in shape.Users)
                    {
                        if (userCell.Name == "Status")
                        {
                            string cellValue = userCell.Value.Val?.Trim();

                            // Apply conditional color based on the cell value
                            if (string.Equals(cellValue, "High", StringComparison.OrdinalIgnoreCase))
                            {
                                // Red for High
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                            else if (string.Equals(cellValue, "Medium", StringComparison.OrdinalIgnoreCase))
                            {
                                // Yellow for Medium
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                            }
                            else
                            {
                                // Green for Low or any other value
                                shape.Fill.FillForegnd.Value = "#00FF00";
                            }

                            // Once the relevant user cell is processed, exit the inner loop
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
