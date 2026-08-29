using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to map shape IDs (long) to their custom connection point collections
                Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

                // Work with the first page of the diagram
                Page page = diagram.Pages[0];

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip connector shapes (1-D) – we only add custom points to regular shapes
                    if (shape.OneD)
                        continue;

                    // Create a custom connection point at the center of the shape
                    Connection customConn = new Connection();
                    // Position formulas: center horizontally, top vertically
                    customConn.X.Ufe.F = "Width*0.5";
                    customConn.Y.Ufe.F = "Height*0";

                    // Add the connection point to the shape
                    shape.Connections.Add(customConn);

                    // Store the connection point in the dictionary for later batch updates
                    long shapeId = shape.ID;
                    if (!shapeConnectionMap.ContainsKey(shapeId))
                    {
                        shapeConnectionMap[shapeId] = new List<Connection>();
                    }
                    shapeConnectionMap[shapeId].Add(customConn);
                }

                // Example batch update: shift all custom connection points slightly
                foreach (KeyValuePair<long, List<Connection>> entry in shapeConnectionMap)
                {
                    foreach (Connection conn in entry.Value)
                    {
                        // Update formulas – move point 10% to the right and 20% down from the original position
                        conn.X.Ufe.F = "Width*0.6";
                        conn.Y.Ufe.F = "Height*0.2";
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processed and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }