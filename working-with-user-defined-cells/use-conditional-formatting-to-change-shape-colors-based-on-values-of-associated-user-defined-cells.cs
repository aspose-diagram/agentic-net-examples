using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Locate a user‑defined cell named "Status"
                        User statusCell = null;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == "Status" || user.NameU == "Status")
                            {
                                statusCell = user;
                                break;
                            }
                        }

                        // If the cell is not present, move to the next shape
                        if (statusCell == null)
                            continue;

                        // Read the cell value (trim to avoid whitespace issues)
                        string value = statusCell.Value.Val?.Trim();

                        // Apply fill color based on the cell value
                        if (string.Equals(value, "High", StringComparison.OrdinalIgnoreCase))
                        {
                            // Red for High
                            shape.Fill.FillForegnd.Value = "#FF0000";
                        }
                        else if (string.Equals(value, "Medium", StringComparison.OrdinalIgnoreCase))
                        {
                            // Orange for Medium
                            shape.Fill.FillForegnd.Value = "#FFA500";
                        }
                        else
                        {
                            // Green for any other value
                            shape.Fill.FillForegnd.Value = "#00FF00";
                        }

                        // Ensure the fill pattern is solid
                        shape.Fill.FillPattern.Value = 1;
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