using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram from file
                // Replace "input.vsdx" with the path to your diagram file
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate over each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a new field to hold the timestamp
                        Field timestampField = new Field();

                        // Set the field's value to the current date and time
                        timestampField.Value.Val = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        // Add the field to the shape's Fields collection
                        shape.Fields.Add(timestampField);
                    }
                }

                // Save the modified diagram to a new file
                // Ensure the SaveFileFormat enum uses PascalCase
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }