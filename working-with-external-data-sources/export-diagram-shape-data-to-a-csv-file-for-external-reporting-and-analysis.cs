using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "shape_data.csv";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare the CSV file
                using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine(
                        "\"PageName\",\"ShapeID\",\"ShapeName\",\"MasterName\",\"Text\",\"Data1\",\"Data2\",\"Data3\",\"PinX\",\"PinY\",\"Width\",\"Height\"");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve basic shape information
                            string pageName = page.NameU ?? "";
                            string shapeId = shape.ID.ToString();
                            string shapeName = shape.NameU ?? "";
                            string masterName = shape.Master != null ? shape.Master.Name : "";
                            string text = shape.Text.Value.Text ?? "";
                            string data1 = shape.Data1 ?? "";
                            string data2 = shape.Data2 ?? "";
                            string data3 = shape.Data3 ?? "";
                            string pinX = shape.XForm.PinX.Value.ToString();
                            string pinY = shape.XForm.PinY.Value.ToString();
                            string width = shape.XForm.Width.Value.ToString();
                            string height = shape.XForm.Height.Value.ToString();

                            // Escape fields for CSV (wrap in quotes and double any internal quotes)
                            string Escape(string s) => $"\"{s.Replace("\"", "\"\"")}\"";

                            // Write the CSV line
                            writer.WriteLine(
                                $"{Escape(pageName)},{Escape(shapeId)},{Escape(shapeName)},{Escape(masterName)},{Escape(text)},{Escape(data1)},{Escape(data2)},{Escape(data3)},{Escape(pinX)},{Escape(pinY)},{Escape(width)},{Escape(height)}");
                        }
                    }
                }

                Console.WriteLine($"Shape data exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }