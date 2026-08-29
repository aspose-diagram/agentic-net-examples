using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Path of the text file where the log will be written
            string logFilePath = "UserCellsLog.txt";

            // Open a StreamWriter for the log file
            using (StreamWriter writer = new StreamWriter(logFilePath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user‑defined cells of the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Write the page name, shape name, user cell name and its current value
                            writer.WriteLine(
                                $"Page: {page.Name}, Shape: {shape.Name}, UserCell: {userCell.NameU}, Value: {userCell.Value}");
                        }
                    }
                }
            }

            // No changes are made to the diagram, so saving is optional.
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
