using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file. Use first command‑line argument if provided.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Prepare a flag to indicate whether any broken links were found.
                bool hasBrokenLinks = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check the three generic data fields (Data1, Data2, Data3).
                        // If any of them contain a non‑empty value, we treat it as a data link.
                        // In a real scenario you would verify the link against diagram.DataRecordSets,
                        // but for this example we simply report the presence of the data value.
                        if (!string.IsNullOrWhiteSpace(shape.Data1) ||
                            !string.IsNullOrWhiteSpace(shape.Data2) ||
                            !string.IsNullOrWhiteSpace(shape.Data3))
                        {
                            // Here we assume the link is broken (e.g., the referenced record set is missing).
                            // Report the shape information.
                            hasBrokenLinks = true;
                            Console.WriteLine("Broken data link detected:");
                            Console.WriteLine($"  Page Name : {page.Name}");
                            Console.WriteLine($"  Shape ID  : {shape.ID}");
                            Console.WriteLine($"  Shape Name: {shape.Name}");
                            Console.WriteLine($"  Data1     : {shape.Data1}");
                            Console.WriteLine($"  Data2     : {shape.Data2}");
                            Console.WriteLine($"  Data3     : {shape.Data3}");
                            Console.WriteLine();
                        }
                    }
                }

                if (!hasBrokenLinks)
                {
                    Console.WriteLine("No shapes with broken data links were found.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }