using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Create a new field to hold the timestamp
                    Field timestampField = new Field();

                    // Assign the current date and time as the field value
                    timestampField.Value.Val = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // Set the field type to a safe default (Undefined)
                    timestampField.Type.Value = TypeFieldValue.Undefined;

                    // Add the timestamp field to the shape's Fields collection
                    shape.Fields.Add(timestampField);
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
