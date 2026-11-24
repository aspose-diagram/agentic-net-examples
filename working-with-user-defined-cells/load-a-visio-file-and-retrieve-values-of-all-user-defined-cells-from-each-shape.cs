using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path.
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard: ensure the file exists before proceeding.
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU}");

                    // Iterate through all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Basic shape information.
                        Console.WriteLine($"  Shape ID: {shape.ID}, NameU: {shape.NameU}");

                        // Master name (if the shape is based on a master).
                        if (shape.Master != null)
                        {
                            Console.WriteLine($"    Master: {shape.Master.Name}");
                        }

                        // Retrieve user-defined cells via the Users collection.
                        if (shape.Users != null && shape.Users.Count > 0)
                        {
                            Console.WriteLine("    User-defined cells:");
                            foreach (Aspose.Diagram.User userCell in shape.Users)
                            {
                                // NameU: universal name of the user-defined cell.
                                // Value.Val: the value stored in the cell.
                                // Prompt.Value: optional prompt/description.
                                string name = userCell.NameU ?? "(null)";
                                string value = userCell.Value?.Val ?? "(null)";
                                string prompt = userCell.Prompt?.Value ?? "(null)";

                                Console.WriteLine($"      {name} = {value} (Prompt: {prompt})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("    No user-defined cells.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}