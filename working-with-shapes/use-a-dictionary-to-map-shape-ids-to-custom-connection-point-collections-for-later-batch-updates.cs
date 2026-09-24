using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram from a file stream
                using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Dictionary to map each shape ID to its collection of connection points
                    Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

                    // Iterate through all pages and shapes to populate the dictionary
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Copy existing connections into a list
                            List<Connection> connections = new List<Connection>();
                            foreach (Connection conn in shape.Connections)
                            {
                                connections.Add(conn);
                            }

                            // Store the list in the dictionary using the shape's ID as the key
                            shapeConnectionMap[shape.ID] = connections;
                        }
                    }

                    // Example batch update: shift all custom connection points 0.5 inches to the right
                    foreach (KeyValuePair<long, List<Connection>> entry in shapeConnectionMap)
                    {
                        // Retrieve the shape by ID
                        Shape shape = diagram.Pages[0].Shapes.GetShape(entry.Key);
                        if (shape == null)
                            continue;

                        foreach (Connection conn in entry.Value)
                        {
                            // Update the X coordinate formula to add 0.5 inches
                            // Using a simple offset; more complex formulas can be applied as needed
                            conn.X.Ufe.F = $"({conn.X.Ufe.F})+0.5";
                            // Y coordinate remains unchanged; you could modify similarly
                        }

                        // Apply the modified connections back to the shape
                        shape.Connections.Clear();
                        foreach (Connection conn in entry.Value)
                        {
                            shape.Connections.Add(conn);
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }