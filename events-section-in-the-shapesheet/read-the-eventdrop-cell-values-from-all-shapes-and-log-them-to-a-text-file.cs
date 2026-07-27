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

            // Open a text file to log EventDrop values
            using (StreamWriter writer = new StreamWriter("EventDropValues.txt"))
            {
                // Iterate through every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through every shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has an Event object and an EventDrop cell
                        if (shape.Event != null && shape.Event.EventDrop != null)
                        {
                            // Retrieve the numeric value of the EventDrop cell
                            double eventDropValue = shape.Event.EventDrop.Value;

                            // Log the page name, shape ID, and EventDrop value
                            writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, EventDrop: {eventDropValue}");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
