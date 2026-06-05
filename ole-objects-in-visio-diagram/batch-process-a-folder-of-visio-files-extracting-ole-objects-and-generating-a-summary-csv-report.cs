using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files
                string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";
                // Output CSV file path
                string outputCsv = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "OleSummary.csv");

                var csvBuilder = new StringBuilder();
                // Header for the CSV report
                csvBuilder.AppendLine("FileName,PageName,ShapeID,ShapeName,OleObjectName,OleObjectSize");

                // Process each Visio file in the folder (supports common extensions)
                foreach (string filePath in Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly))
                {
                    string ext = Path.GetExtension(filePath).ToLowerInvariant();
                    if (ext != ".vsd" && ext != ".vsdx" && ext != ".vdx" && ext != ".vss" && ext != ".vssx")
                        continue; // skip non‑Visio files

                    // Load the Visio diagram using the provided constructor (load rule)
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Use reflection to safely access OLE data without depending on a specific API version
                                PropertyInfo oleProp = shape.GetType().GetProperty("OleData");
                                if (oleProp == null) continue; // shape does not contain OLE data

                                object oleData = oleProp.GetValue(shape);
                                if (oleData == null) continue; // no OLE object present

                                // Attempt to read common OLE properties (Name and Size) via reflection
                                string oleName = GetPropertyString(oleData, "Name");
                                string oleSize = GetPropertyString(oleData, "Size");

                                // Append a line to the CSV report
                                csvBuilder.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}",
                                    Path.GetFileName(filePath),
                                    page.Name,
                                    shape.ID,
                                    shape.Name,
                                    oleName,
                                    oleSize));
                            }
                        }
                    }
                }

                // Write the CSV summary (uses standard .NET I/O, no Aspose save rule needed)
                File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        // Helper to retrieve a property value as string safely
        private static string GetPropertyString(object obj, string propertyName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propertyName);
            if (prop == null) return string.Empty;
            object value = prop.GetValue(obj);
            return value != null ? value.ToString() : string.Empty;
        }
    }