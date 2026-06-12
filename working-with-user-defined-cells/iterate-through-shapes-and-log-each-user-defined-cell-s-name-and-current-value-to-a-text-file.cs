using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = @"C:\Input\diagram.vsdx";

            // Path to the output log file
            string logPath = @"C:\Output\UserCellsLog.txt";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Prepare the writer
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Write shape identifier
                        writer.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Iterate through user-defined cells (Users collection)
                        foreach (User userCell in shape.Users)
                        {
                            // User cell name (universal name) and its current value
                            string cellName = userCell.NameU;
                            string cellValue = userCell.Value?.ToString() ?? string.Empty;

                            writer.WriteLine($"  User Cell: {cellName} = {cellValue}");
                        }

                        writer.WriteLine(); // Blank line for readability
                    }
                }
            }

            // Optional: inform that logging is complete
            Console.WriteLine("User-defined cells have been logged to: " + logPath);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
