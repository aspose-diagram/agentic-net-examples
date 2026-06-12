using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to map each shape ID to its collection of connection points
                Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

                // Populate the dictionary with connection points from all pages
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;
                        List<Connection> connections = new List<Connection>();
                        foreach (Connection conn in shape.Connections)
                        {
                            connections.Add(conn);
                        }
                        shapeConnectionMap[shapeId] = connections;
                    }
                }

                // Example batch update: move every connection point to the shape's center
                foreach (var kvp in shapeConnectionMap)
                {
                    foreach (Connection conn in kvp.Value)
                    {
                        conn.X.Ufe.F = "Width*0.5";
                        conn.Y.Ufe.F = "Height*0.5";
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }