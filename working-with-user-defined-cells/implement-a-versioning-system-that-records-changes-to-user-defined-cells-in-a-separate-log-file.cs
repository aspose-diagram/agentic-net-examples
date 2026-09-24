using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";
                string logPath = "cell_changes.log";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Capture original user-defined cell values
                var originalValues = new Dictionary<string, string>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users == null) continue;
                        foreach (User userCell in shape.Users)
                        {
                            string key = $"{page.ID}_{shape.ID}_{userCell.NameU}";
                            originalValues[key] = userCell.Value.Val;
                        }
                    }
                }

                // ----- Simulate modifications to user-defined cells -----
                // For demonstration, modify the first user-defined cell found
                bool modificationDone = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Users == null) continue;
                        foreach (User userCell in shape.Users)
                        {
                            // Change the value
                            userCell.Value.Val = "ModifiedValue";
                            modificationDone = true;
                            break;
                        }
                        if (modificationDone) break;
                    }
                    if (modificationDone) break;
                }

                // Capture new values and log differences
                using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
                {
                    logWriter.WriteLine($"--- Change Log: {DateTime.Now} ---");
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Users == null) continue;
                            foreach (User userCell in shape.Users)
                            {
                                string key = $"{page.ID}_{shape.ID}_{userCell.NameU}";
                                string original = originalValues.ContainsKey(key) ? originalValues[key] : null;
                                string current = userCell.Value.Val;

                                if (original != current)
                                {
                                    logWriter.WriteLine($"Page {page.ID}, Shape {shape.ID}, Cell '{userCell.NameU}':");
                                    logWriter.WriteLine($"    Original Value: {(original ?? "N/A")}");
                                    logWriter.WriteLine($"    New Value: {current}");
                                }
                            }
                        }
                    }
                    logWriter.WriteLine(); // Blank line for readability
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing complete. Changes logged to: " + logPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }