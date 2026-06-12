using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the Visio file path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: UserDefinedCellDiagnostic <visio-file-path>");
                return;
            }

            string filePath = args[0];

            // Load the diagram (no LoadOptions needed)
            Diagram diagram;
            try
            {
                diagram = new Diagram(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Dictionaries to track user-defined cell names within the current shape
                    var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    var missingNames = new List<string>();

                    foreach (User userCell in shape.Users)
                    {
                        // Determine the identifier for the user-defined cell
                        string name = !string.IsNullOrWhiteSpace(userCell.NameU)
                                      ? userCell.NameU
                                      : userCell.Name;

                        // If both Name and NameU are empty, consider it missing
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            missingNames.Add("(Unnamed)");
                            continue;
                        }

                        // Count occurrences to detect duplicates
                        if (nameCounts.ContainsKey(name))
                            nameCounts[name]++;
                        else
                            nameCounts[name] = 1;
                    }

                    // Report missing user-defined cell names
                    if (missingNames.Count > 0)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) has missing user-defined cell names:");
                        foreach (var missing in missingNames)
                        {
                            Console.WriteLine($"  - {missing}");
                        }
                    }

                    // Report duplicate user-defined cell names
                    foreach (var kvp in nameCounts)
                    {
                        if (kvp.Value > 1)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) has duplicate user-defined cell name \"{kvp.Key}\" ({kvp.Value} occurrences).");
                        }
                    }
                }
            }
        }
    }