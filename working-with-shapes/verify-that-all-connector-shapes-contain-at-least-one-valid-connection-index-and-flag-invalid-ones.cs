using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            bool hasConnection = false;

                            // Check the page's Connect collection for any link involving this connector
                            foreach (Connect conn in page.Connects)
                            {
                                if (conn.FromSheet == shape.ID || conn.ToSheet == shape.ID)
                                {
                                    hasConnection = true;
                                    break;
                                }
                            }

                            // Flag connectors without any connections
                            if (!hasConnection)
                            {
                                Console.WriteLine($"Invalid connector found: ID={shape.ID}, NameU=\"{shape.NameU}\"");
                            }
                        }
                    }
                }

                // Optionally save the diagram (unchanged) to demonstrate lifecycle compliance
                diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }