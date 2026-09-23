using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output log file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: EventDropLogger <inputVisioFile> <outputLogFile>");
                return;
            }

            string inputPath = args[0];
            string logPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Open a StreamWriter for logging
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Event section exists
                        if (shape.Event != null && shape.Event.EventDrop != null && shape.Event.EventDrop.Ufe != null)
                        {
                            string eventDropFormula = shape.Event.EventDrop.Ufe.F ?? string.Empty;
                            // Log shape ID and its EventDrop formula
                            writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, EventDrop: {eventDropFormula}");
                        }
                        else
                        {
                            // Log that the shape has no EventDrop defined
                            writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, EventDrop: <none>");
                        }
                    }
                }
            }

            Console.WriteLine($"EventDrop values have been logged to: {logPath}");
        }
    }