using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new user‑defined cell.
                        User userCell = new User();
                        userCell.Name = "MyCustomCell";               // Custom cell name.
                        userCell.Value.Val = "CustomValue";           // Cell value.
                        userCell.Prompt.Value = "Custom user-defined cell"; // Optional description.

                        // Add the user‑defined cell to the shape's Users collection.
                        shape.Users.Add(userCell);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}