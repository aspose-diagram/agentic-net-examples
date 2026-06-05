using System;
using Aspose.Diagram;
using System.Reflection;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                Console.WriteLine("Logging EventMouseDown execution order for all shapes...");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an Event section
                        if (shape.Event == null)
                            continue;

                        // Use reflection to check for a property named "EventMouseDown"
                        PropertyInfo mouseDownProp = shape.Event.GetType().GetProperty("EventMouseDown");
                        if (mouseDownProp == null)
                            continue; // No EventMouseDown cell on this shape

                        // Get the event cell instance
                        object eventCell = mouseDownProp.GetValue(shape.Event);
                        if (eventCell == null)
                            continue;

                        // The event cell contains a Ufe (User-defined formula) property.
                        // Use reflection again to retrieve the formula string.
                        PropertyInfo ufeProp = eventCell.GetType().GetProperty("Ufe");
                        if (ufeProp == null)
                            continue;

                        object ufeObj = ufeProp.GetValue(eventCell);
                        if (ufeObj == null)
                            continue;

                        // The Ufe object has an 'F' property that holds the formula.
                        PropertyInfo formulaProp = ufeObj.GetType().GetProperty("F");
                        string formula = formulaProp?.GetValue(ufeObj) as string ?? "<no formula>";

                        // Log the shape ID and the EventMouseDown formula.
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, EventMouseDown Formula: {formula}");
                    }
                }

                Console.WriteLine("Logging completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }