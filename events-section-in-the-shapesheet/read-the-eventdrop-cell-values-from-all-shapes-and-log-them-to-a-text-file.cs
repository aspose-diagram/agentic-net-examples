using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Open a text file to log EventDrop values
            using (StreamWriter logWriter = new StreamWriter("EventDropLog.txt"))
            {
                // Iterate through every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through every shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an Event collection and the EventDrop cell is present
                        if (shape.Event != null && shape.Event.EventDrop != null)
                        {
                            // Retrieve the numeric value of the EventDrop cell
                            double eventDropValue = shape.Event.EventDrop.Value;

                            // Write the page ID, shape ID, and EventDrop value to the log file
                            logWriter.WriteLine(
                                $"Page ID: {page.ID}, Shape ID: {shape.ID}, EventDrop: {eventDropValue}");
                        }
                    }
                }
            }

            // Optional: inform that logging is complete
            Console.WriteLine("EventDrop values have been logged to EventDropLog.txt");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
