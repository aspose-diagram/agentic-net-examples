using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = @"C:\Diagrams\input.vsdx";
                // Output Visio file path
                string outputPath = @"C:\Diagrams\output.vsdx";

                // Old and new network share base paths
                string oldBase = @"\\oldshare\documents\";
                string newBase = @"\\newshare\documents\";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE foreign object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure the shape has a Hyperlinks collection
                            if (shape.Hyperlinks != null)
                            {
                                // Update each hyperlink address that starts with the old base path
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    if (link.Address != null && link.Address.Value != null &&
                                        link.Address.Value.StartsWith(oldBase, StringComparison.OrdinalIgnoreCase))
                                    {
                                        string relativePart = link.Address.Value.Substring(oldBase.Length);
                                        link.Address.Value = newBase + relativePart;
                                        Console.WriteLine($"Updated hyperlink for shape ID {shape.ID}: {link.Address.Value}");
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }