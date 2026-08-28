using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Access the Event section of the shape
                    var eventSection = shape.Event;
                    if (eventSection == null) continue;

                    // Use reflection to find any property that represents an EventComment cell
                    PropertyInfo[] eventProps = eventSection.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in eventProps)
                    {
                        if (prop.Name.Contains("EventComment"))
                        {
                            // Get the cell object (e.g., shape.Event.EventComment)
                            object cellObj = prop.GetValue(eventSection);
                            if (cellObj == null) continue;

                            // The cell should have a Ufe property with an F (formula) member
                            PropertyInfo ufeProp = cellObj.GetType().GetProperty("Ufe");
                            if (ufeProp == null) continue;

                            object ufeObj = ufeProp.GetValue(cellObj);
                            if (ufeObj == null) continue;

                            PropertyInfo formulaProp = ufeObj.GetType().GetProperty("F");
                            if (formulaProp == null) continue;

                            // Clear the formula to effectively remove the EventComment cell content
                            formulaProp.SetValue(ufeObj, string.Empty);
                        }
                    }
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
