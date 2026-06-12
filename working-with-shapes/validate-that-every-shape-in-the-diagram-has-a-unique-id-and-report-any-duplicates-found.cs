using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram using Aspose.Diagram
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to map shape IDs to the list of shapes that share the same ID
                Dictionary<long, List<Shape>> idMap = new Dictionary<long, List<Shape>>();

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;

                        if (!idMap.ContainsKey(shapeId))
                        {
                            idMap[shapeId] = new List<Shape>();
                        }

                        idMap[shapeId].Add(shape);
                    }
                }

                // Flag to indicate if any duplicates were found
                bool duplicatesFound = false;

                // Check the dictionary for IDs that appear more than once
                foreach (KeyValuePair<long, List<Shape>> entry in idMap)
                {
                    if (entry.Value.Count > 1)
                    {
                        duplicatesFound = true;
                        Console.WriteLine($"Duplicate Shape ID found: {entry.Key}");
                        foreach (Shape dupShape in entry.Value)
                        {
                            // Report page name and shape name for each duplicate
                            Console.WriteLine($"\tPage: {dupShape.Page.Name}, Shape Name: {dupShape.Name}");
                        }
                    }
                }

                if (!duplicatesFound)
                {
                    Console.WriteLine("All shape IDs are unique.");
                }

                // Optionally, save the diagram (if any modifications were made)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }