using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input diagram path, output diagram path, new network share base path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: OLEHyperlinkUpdater <inputDiagramPath> <outputDiagramPath> <newNetworkShareBasePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string newBasePath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is an OLE object and has foreign data of type Object
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Verify the Hyperlinks collection is not null
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Update each hyperlink address
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                if (link != null && link.Address != null && link.Address.Value != null)
                                {
                                    // Extract the file name from the existing address
                                    string oldAddress = link.Address.Value;
                                    string fileName = Path.GetFileName(oldAddress);

                                    // Build the new address using the provided network share base path
                                    string newAddress = Path.Combine(newBasePath, fileName);

                                    // Assign the new address back to the hyperlink
                                    link.Address.Value = newAddress;

                                    Console.WriteLine($"Updated hyperlink for shape ID {shape.ID}: {oldAddress} -> {newAddress}");
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram (preserving the original format, here using Vsdx)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }