using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";
                // Path to the output log file
                string logPath = "EventDropValues.txt";

                // Load the diagram using Aspose.Diagram
                Diagram diagram = new Diagram(diagramPath);

                using (StreamWriter writer = new StreamWriter(logPath, false))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has an Event object
                            if (shape.Event != null && shape.Event.EventDrop != null)
                            {
                                // Retrieve the EventDrop value (DoubleValue)
                                double? eventDropValue = null;

                                // DoubleValue may contain a numeric value or a formula.
                                // Attempt to get the numeric value; if not available, skip.
                                try
                                {
                                    // The Value property holds the numeric representation.
                                    // It may be null if the cell contains a formula.
                                    eventDropValue = shape.Event.EventDrop.Value;
                                }
                                catch
                                {
                                    // Ignore shapes where the value cannot be parsed.
                                }

                                if (eventDropValue.HasValue)
                                {
                                    // Log shape ID, name, and EventDrop value
                                    writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shape.Name}, EventDrop: {eventDropValue.Value}");
                                }
                            }
                        }
                    }
                }

                // Optionally, save the diagram if any modifications were made (not required here)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }