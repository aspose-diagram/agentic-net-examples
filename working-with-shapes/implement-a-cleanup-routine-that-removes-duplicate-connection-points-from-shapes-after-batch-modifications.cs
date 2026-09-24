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
                string outputPath = "output_cleaned.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Connections collection with more than one point
                        if (shape.Connections != null && shape.Connections.Count > 1)
                        {
                            // Track unique connection point definitions (X and Y formulas)
                            var seen = new HashSet<string>();

                            // Iterate backwards so we can safely remove items by index
                            for (int i = shape.Connections.Count - 1; i >= 0; i--)
                            {
                                Connection conn = shape.Connections[i];
                                // Build a key that represents the connection point location
                                string key = $"{conn.X.Ufe.F}|{conn.Y.Ufe.F}";

                                // If the key already exists, this is a duplicate – remove it
                                if (!seen.Add(key))
                                {
                                    shape.Connections.RemoveAt(i);
                                }
                            }
                        }
                    }
                }

                // Save the cleaned diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }