using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            bool allValid = true;

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    bool hasEventValidate = false;

                    // Examine user‑defined cells for a cell named "EventValidate".
                    foreach (User userCell in shape.Users)
                    {
                        // Compare both Name and NameU (localized and universal names).
                        if (string.Equals(userCell.Name, "EventValidate", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(userCell.NameU, "EventValidate", StringComparison.OrdinalIgnoreCase))
                        {
                            hasEventValidate = true;
                            string formula = userCell.Value.Val;

                            // Validate that the formula is not empty or whitespace.
                            if (string.IsNullOrWhiteSpace(formula))
                            {
                                Console.Error.WriteLine($"Error: Shape ID {shape.ID} on Page ID {page.ID} has an empty EventValidate formula.");
                                allValid = false;
                            }

                            // No need to check further user cells for this shape.
                            break;
                        }
                    }

                    // If the EventValidate cell was not found, report it.
                    if (!hasEventValidate)
                    {
                        Console.Error.WriteLine($"Error: Shape ID {shape.ID} on Page ID {page.ID} is missing an EventValidate cell.");
                        allValid = false;
                    }
                }
            }

            // If all shapes passed validation, save the diagram.
            if (allValid)
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
            else
            {
                Console.Error.WriteLine("Diagram not saved due to validation errors.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}