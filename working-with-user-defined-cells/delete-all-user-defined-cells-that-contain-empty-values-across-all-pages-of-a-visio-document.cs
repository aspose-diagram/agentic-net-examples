using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Collect indices of user‑defined cells that have empty values.
                        List<int> indicesToRemove = new List<int>();
                        for (int i = 0; i < shape.Users.Count; i++)
                        {
                            User userCell = shape.Users[i];
                            // Treat null or whitespace as empty.
                            if (string.IsNullOrWhiteSpace(userCell?.Value?.Val))
                            {
                                indicesToRemove.Add(i);
                            }
                        }

                        // Remove the identified cells in reverse order to keep indices valid.
                        for (int i = indicesToRemove.Count - 1; i >= 0; i--)
                        {
                            shape.Users.RemoveAt(indicesToRemove[i]);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Error saving diagram: {saveEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading or processing diagram: {ex.Message}");
        }
    }
}