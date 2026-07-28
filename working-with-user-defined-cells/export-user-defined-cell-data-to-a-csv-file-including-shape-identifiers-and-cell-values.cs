using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (adjust the path as needed)
                string inputPath = "input.vsdx";

                // Output CSV file
                string outputCsv = "UserDefinedCells.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create CSV and write header
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    writer.WriteLine("ShapeID,ShapeName,UserName,UserValue");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Iterate through user‑defined cells of the shape
                            foreach (User user in shape.Users)
                            {
                                // Prepare values
                                string shapeId = shape.ID.ToString();
                                string shapeName = shape.NameU ?? string.Empty;
                                string userName = user.NameU ?? string.Empty;
                                string userValue = user.Value?.Val ?? string.Empty;

                                // Simple CSV escaping for commas and quotes
                                string Escape(string s)
                                {
                                    if (s.Contains("\"") || s.Contains(","))
                                    {
                                        s = s.Replace("\"", "\"\"");
                                        return $"\"{s}\"";
                                    }
                                    return s;
                                }

                                // Write a CSV line
                                writer.WriteLine($"{Escape(shapeId)},{Escape(shapeName)},{Escape(userName)},{Escape(userValue)}");
                            }
                        }
                    }
                }

                Console.WriteLine($"User‑defined cell data exported to '{outputCsv}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }