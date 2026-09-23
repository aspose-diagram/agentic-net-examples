using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument or default)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect all existing shape IDs across all pages
            HashSet<long> existingShapeIds = new HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    existingShapeIds.Add(shape.ID);
                }
            }

            // Event cell names to validate (including EventCalc if present)
            string[] eventNames = { "EventXFMod", "EventDblClick", "EventDrop", "EventMultiDrop", "TheText", "TheData", "EventCalc" };

            // Regex to find shape ID references like Sheet.5!
            Regex sheetIdRegex = new Regex(@"Sheet\.(\d+)!");

            bool validationFailed = false;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (string eventName in eventNames)
                    {
                        string formula = GetEventFormula(shape, eventName);
                        if (string.IsNullOrEmpty(formula))
                            continue;

                        // Find all shape ID references in the formula
                        foreach (Match match in sheetIdRegex.Matches(formula))
                        {
                            if (long.TryParse(match.Groups[1].Value, out long referencedId))
                            {
                                if (!existingShapeIds.Contains(referencedId))
                                {
                                    Console.WriteLine($"Invalid reference in shape ID {shape.ID}, event '{eventName}': referenced shape ID {referencedId} does not exist.");
                                    validationFailed = true;
                                }
                            }
                        }
                    }
                }
            }

            if (validationFailed)
            {
                throw new Exception("Validation failed: some EventCalc formulas reference non-existing shape IDs.");
            }
            else
            {
                Console.WriteLine("All EventCalc formulas reference existing shape IDs.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    // Retrieves the formula string from a specific event cell using reflection
    private static string GetEventFormula(Shape shape, string eventName)
    {
        if (shape?.Event == null)
            return null;

        // Get the event property (e.g., EventDblClick) from the Event section
        PropertyInfo eventProp = shape.Event.GetType().GetProperty(eventName);
        if (eventProp == null)
            return null;

        object eventObj = eventProp.GetValue(shape.Event);
        if (eventObj == null)
            return null;

        // Get the Ufe property which holds the formula container
        PropertyInfo ufeProp = eventObj.GetType().GetProperty("Ufe");
        if (ufeProp == null)
            return null;

        object ufeObj = ufeProp.GetValue(eventObj);
        if (ufeObj == null)
            return null;

        // Get the actual formula string from the F property
        PropertyInfo fProp = ufeObj.GetType().GetProperty("F");
        if (fProp == null)
            return null;

        return fProp.GetValue(ufeObj) as string;
    }
}
