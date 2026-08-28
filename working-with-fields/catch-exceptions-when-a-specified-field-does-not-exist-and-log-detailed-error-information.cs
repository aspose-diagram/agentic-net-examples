using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Shape shape = diagram.Pages[0].Shapes[0];

                // Attempt to access a field that may not exist
                try
                {
                    // This will throw if the Fields collection is empty
                    Field targetField = shape.Fields[0];

                    // If no exception, output some details about the field
                    Console.WriteLine($"Field Index: {targetField.IX}");
                    Console.WriteLine($"Field Type Value: {(int)targetField.Type.Value}");
                    Console.WriteLine($"Field Display Value: {targetField.DisplayValue}");
                }
                catch (Exception ex)
                {
                    // Log detailed error information
                    Console.WriteLine("Error: Specified field does not exist or could not be accessed.");
                    Console.WriteLine($"Exception Type: {ex.GetType().FullName}");
                    Console.WriteLine($"Message: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }