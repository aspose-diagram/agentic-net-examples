using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after processing
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Store original user-defined cell values: ShapeId -> (CellName -> OriginalValue)
                var originalValues = new Dictionary<long, Dictionary<string, string>>();

                bool validationFailed = false;

                // Iterate through all pages and shapes to capture original values and validate
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        var shapeUserValues = new Dictionary<string, string>();

                        foreach (User userCell in shape.Users)
                        {
                            // Store the original value
                            shapeUserValues[userCell.Name] = userCell.Value.Val;

                            // Example validation: ensure the value can be parsed as a double and is non‑negative
                            if (!double.TryParse(userCell.Value.Val, out double numericValue) || numericValue < 0)
                            {
                                Console.WriteLine($"Validation failed for Shape ID {shapeId}, User Cell '{userCell.Name}' with value '{userCell.Value.Val}'.");
                                validationFailed = true;
                            }
                        }

                        // Keep the snapshot for possible rollback
                        originalValues[shapeId] = shapeUserValues;
                    }
                }

                // If any validation failed, rollback to original values
                if (validationFailed)
                {
                    Console.WriteLine("Validation failed. Restoring original user-defined cell values...");

                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            long shapeId = shape.ID;

                            if (originalValues.TryGetValue(shapeId, out var savedUserValues))
                            {
                                foreach (User userCell in shape.Users)
                                {
                                    if (savedUserValues.TryGetValue(userCell.Name, out string originalVal))
                                    {
                                        userCell.Value.Val = originalVal;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("All user-defined cells passed validation.");
                }

                // Save the diagram (using Vsdx format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }