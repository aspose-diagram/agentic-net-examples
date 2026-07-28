using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Backup of original user-defined cell values
                List<(long ShapeId, string UserName, string OriginalValue)> backup = new List<(long, string, string)>();

                // Capture current values
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (User user in shape.Users)
                        {
                            backup.Add((shape.ID, user.Name, user.Value.Val));
                        }
                    }
                }

                // Example modification that may cause validation to fail
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (User user in shape.Users)
                        {
                            // Simulate an invalid change
                            user.Value.Val = "InvalidValue";
                        }
                    }
                }

                // Perform validation: each user-defined cell must be a valid double
                bool isValid = true;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (User user in shape.Users)
                        {
                            double dummy;
                            if (!double.TryParse(user.Value.Val, out dummy))
                            {
                                isValid = false;
                                Console.WriteLine($"Validation failed for Shape ID {shape.ID}, User Cell '{user.Name}'.");
                                break;
                            }
                        }
                        if (!isValid) break;
                    }
                    if (!isValid) break;
                }

                // Rollback if validation failed
                if (!isValid)
                {
                    Console.WriteLine("Rolling back to original values...");

                    foreach (var entry in backup)
                    {
                        Shape shape = FindShapeById(diagram, entry.ShapeId);
                        if (shape == null) continue;

                        // Locate the specific user-defined cell by name
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == entry.UserName)
                            {
                                user.Value.Val = entry.OriginalValue;
                                break;
                            }
                        }
                    }
                }

                // Save the diagram (whether modified or rolled back)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to locate a shape by its ID across all pages
        private static Shape FindShapeById(Diagram diagram, long shapeId)
        {
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                        return shape;
                }
                catch
                {
                    // GetShape throws if not found; ignore and continue searching
                }
            }
            return null;
        }
    }