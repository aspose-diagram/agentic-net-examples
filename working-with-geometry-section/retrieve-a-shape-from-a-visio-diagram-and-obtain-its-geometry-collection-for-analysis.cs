using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape s in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (s.Del == BOOL.False)
                    {
                        targetShape = s;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No visible shape found on the first page.");
                    return;
                }

                // Output basic shape information
                Console.WriteLine($"Shape ID: {targetShape.ID}");
                Console.WriteLine($"Shape NameU: {targetShape.NameU}");
                Console.WriteLine($"Shape Master: {(targetShape.Master != null ? targetShape.Master.Name : "None")}");

                // Retrieve the Geometry collection (Geoms) for analysis
                if (targetShape.Geoms == null || targetShape.Geoms.Count == 0)
                {
                    Console.WriteLine("The selected shape has no geometry sections.");
                }
                else
                {
                    Console.WriteLine($"Geometry sections count: {targetShape.Geoms.Count}");

                    // Enumerate each geometry section
                    int geomIndex = 0;
                    foreach (Geom geom in targetShape.Geoms)
                    {
                        Console.WriteLine($"  Geom #{geomIndex}");

                        // Output the number of coordinate entries in this geometry (if available)
                        if (geom.CoordinateCol != null)
                        {
                            Console.WriteLine($"    Coordinate entries count: {geom.CoordinateCol.Count}");

                            // Example: list the type of each coordinate entry
                            int coordIndex = 0;
                            foreach (object coord in geom.CoordinateCol)
                            {
                                string typeName = coord.GetType().Name;
                                Console.WriteLine($"      [{coordIndex}] {typeName}");
                                coordIndex++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("    No coordinate collection found.");
                        }

                        geomIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}