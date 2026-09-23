using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify OLE (foreign) shapes
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Check if the OLE object contains embedded data
                            bool hasEmbeddedData = shape.ForeignData.ObjectData != null &&
                                                   shape.ForeignData.ObjectData.Length > 0;

                            // Retrieve the external source reference, if any
                            string externalPath = shape.ForeignData.ObjectSourceFullName;

                            // Determine missing reference conditions
                            bool missingExternalFile = false;

                            if (!hasEmbeddedData && !string.IsNullOrEmpty(externalPath))
                            {
                                // The OLE object is linked to an external file; verify its existence
                                missingExternalFile = !File.Exists(externalPath);
                            }
                            else if (!hasEmbeddedData && string.IsNullOrEmpty(externalPath))
                            {
                                // No embedded data and no source path – treat as missing
                                missingExternalFile = true;
                            }

                            // Log details for any missing external references
                            if (missingExternalFile)
                            {
                                Console.WriteLine($"Missing OLE reference detected:");
                                Console.WriteLine($"  Page ID   : {page.ID}");
                                Console.WriteLine($"  Shape ID  : {shape.ID}");
                                Console.WriteLine($"  Shape Name: {shape.NameU}");
                                Console.WriteLine($"  Source    : {(string.IsNullOrEmpty(externalPath) ? "(none)" : externalPath)}");
                            }
                        }
                    }
                }

                // Optionally, save the diagram unchanged (if required by workflow)
                // diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }