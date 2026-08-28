using System;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with actual path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                int executionOrder = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an Event section
                        if (shape.Event == null)
                            continue;

                        // Use reflection to get the EventMouseDown cell (if it exists)
                        PropertyInfo mouseDownProp = shape.Event.GetType().GetProperty("EventMouseDown");
                        if (mouseDownProp == null)
                            continue; // Cell not present on this shape

                        object eventCell = mouseDownProp.GetValue(shape.Event);
                        if (eventCell == null)
                            continue;

                        // The event cell contains a Ufe property with an F (formula) field
                        PropertyInfo ufeProp = eventCell.GetType().GetProperty("Ufe");
                        if (ufeProp == null)
                            continue;

                        object ufeObj = ufeProp.GetValue(eventCell);
                        if (ufeObj == null)
                            continue;

                        PropertyInfo formulaProp = ufeObj.GetType().GetProperty("F");
                        if (formulaProp == null)
                            continue;

                        string formula = formulaProp.GetValue(ufeObj) as string ?? string.Empty;

                        // Log the execution order, shape ID and the formula
                        executionOrder++;
                        Console.WriteLine($"Order {executionOrder}: Shape ID {shape.ID} - EventMouseDown Formula: {formula}");
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