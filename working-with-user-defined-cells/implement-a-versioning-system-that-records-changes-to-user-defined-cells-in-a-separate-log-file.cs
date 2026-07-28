using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramVersioning <inputDiagramPath> <outputDiagramPath> [logFilePath]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string logPath = args.Length >= 3 ? args[2] : "UserCellChanges.log";

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Open log file for appending
            using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
            {
                // Write header for this session
                logWriter.WriteLine($"--- Change Log Session: {DateTime.Now:yyyy-MM-dd HH:mm:ss} ---");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through user-defined cells (Users collection)
                        foreach (User userCell in shape.Users)
                        {
                            // Example rule: if a user cell named "Version" exists, increment its numeric value
                            if (string.Equals(userCell.Name, "Version", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(userCell.NameU, "Version", StringComparison.OrdinalIgnoreCase))
                            {
                                string oldValue = userCell.Value.Val ?? string.Empty;
                                int numericValue;
                                string newValue = oldValue; // default to old if parsing fails

                                if (int.TryParse(oldValue, out numericValue))
                                {
                                    numericValue++;
                                    newValue = numericValue.ToString();
                                    // Update the cell with the new value
                                    userCell.Value.Val = newValue;
                                }

                                // Log the change (or attempted change)
                                logWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ShapeID: {shape.ID} | Cell: {userCell.Name} | Old: {oldValue} | New: {newValue}");
                            }
                        }
                    }
                }

                logWriter.WriteLine($"--- End of Session ---");
                logWriter.WriteLine();
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
                Console.WriteLine($"Change log written to {logPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }