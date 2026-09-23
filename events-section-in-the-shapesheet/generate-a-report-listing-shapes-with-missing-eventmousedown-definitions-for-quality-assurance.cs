using System.IO;
using System;
using Aspose.Diagram;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be inspected
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use reflection to safely access the EventMouseDown cell (if it exists)
                    PropertyInfo mouseDownProp = shape.Event?.GetType().GetProperty("EventMouseDown");
                    bool missingEvent = false;

                    if (mouseDownProp != null)
                    {
                        // Retrieve the event cell object
                        object eventCell = mouseDownProp.GetValue(shape.Event);
                        if (eventCell != null)
                        {
                            // Access the Ufe.F property which holds the formula/value
                            PropertyInfo ufeProp = eventCell.GetType().GetProperty("Ufe");
                            if (ufeProp != null)
                            {
                                object ufeObj = ufeProp.GetValue(eventCell);
                                if (ufeObj != null)
                                {
                                    PropertyInfo fProp = ufeObj.GetType().GetProperty("F");
                                    if (fProp != null)
                                    {
                                        string formula = fProp.GetValue(ufeObj) as string;
                                        if (string.IsNullOrWhiteSpace(formula))
                                        {
                                            missingEvent = true;
                                        }
                                    }
                                    else
                                    {
                                        // If the 'F' property is missing, treat as missing definition
                                        missingEvent = true;
                                    }
                                }
                                else
                                {
                                    missingEvent = true;
                                }
                            }
                            else
                            {
                                missingEvent = true;
                            }
                        }
                        else
                        {
                            missingEvent = true;
                        }
                    }
                    else
                    {
                        // If the EventMouseDown cell does not exist at all, consider it missing
                        missingEvent = true;
                    }

                    if (missingEvent)
                    {
                        Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape.ID}, NameU \"{shape.NameU}\" is missing EventMouseDown definition.");
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
