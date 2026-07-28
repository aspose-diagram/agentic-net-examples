using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths for input and output diagrams
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page and first shape (if they exist)
                if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                {
                    Shape shape = diagram.Pages[0].Shapes[0];
                    string fieldName = "CustomField";

                    try
                    {
                        // Search for a custom property (Prop) with the specified name
                        bool found = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == fieldName)
                            {
                                found = true;
                                Console.WriteLine($"Property '{fieldName}' found with value: {prop.Value.Val}");
                                break;
                            }
                        }

                        // If not found, throw an exception to be caught below
                        if (!found)
                        {
                            throw new InvalidOperationException($"Property '{fieldName}' does not exist on shape ID {shape.ID}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Detailed error logging
                        Console.WriteLine("=== Error accessing field ===");
                        Console.WriteLine($"Timestamp   : {DateTime.Now}");
                        Console.WriteLine($"Shape ID    : {shape.ID}");
                        Console.WriteLine($"Requested   : {fieldName}");
                        Console.WriteLine($"Exception   : {ex.GetType().FullName}");
                        Console.WriteLine($"Message     : {ex.Message}");
                        Console.WriteLine($"Stack Trace : {ex.StackTrace}");
                    }
                }
                else
                {
                    Console.WriteLine("The diagram does not contain any pages or shapes.");
                }

                // Save the diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }