using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an Event section
                    if (shape.Event == null)
                        continue;

                    // Use reflection to find a property named "EventComment"
                    PropertyInfo commentProp = shape.Event.GetType().GetProperty("EventComment");
                    if (commentProp == null)
                        continue; // No EventComment cell on this shape

                    // Get the cell object
                    object commentCell = commentProp.GetValue(shape.Event);
                    if (commentCell == null)
                        continue;

                    // Access the Ufe property of the cell
                    PropertyInfo ufeProp = commentCell.GetType().GetProperty("Ufe");
                    if (ufeProp == null)
                        continue;

                    object ufeObj = ufeProp.GetValue(commentCell);
                    if (ufeObj == null)
                        continue;

                    // Set the formula (F) to an empty string, effectively clearing the comment
                    PropertyInfo fProp = ufeObj.GetType().GetProperty("F");
                    if (fProp != null && fProp.CanWrite)
                    {
                        fProp.SetValue(ufeObj, string.Empty);
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
