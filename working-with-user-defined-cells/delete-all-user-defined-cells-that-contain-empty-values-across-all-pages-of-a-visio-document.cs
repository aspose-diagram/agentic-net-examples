using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioUserCellCleaner <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Guard against a null Users collection.
                        if (shape.Users == null)
                            continue;

                        // Collect user‑defined cells that have empty or whitespace values.
                        var toRemove = new System.Collections.Generic.List<User>();
                        foreach (User userCell in shape.Users)
                        {
                            // userCell.Value may be null; protect against it.
                            string cellValue = userCell?.Value?.Val;
                            if (string.IsNullOrWhiteSpace(cellValue))
                            {
                                toRemove.Add(userCell);
                            }
                        }

                        // Remove the identified empty user‑defined cells.
                        foreach (User userCell in toRemove)
                        {
                            shape.Users.Remove(userCell);
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully cleaned user‑defined cells and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the diagram:");
                Console.WriteLine(ex.Message);
            }
        }
    }