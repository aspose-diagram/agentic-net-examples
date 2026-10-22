using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagrams_folder_path>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all Visio diagram files (VSDX) in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        foreach (string diagramPath in diagramFiles)
        {
            // Guard: ensure the file exists (redundant after GetFiles, but required by rules).
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // ---------- Remove document-level custom property named "DeprecatedFlag" ----------
                var docCustomProps = diagram.DocumentProps.CustomProps;
                List<CustomProp> docPropsToRemove = new List<CustomProp>();

                foreach (CustomProp cp in docCustomProps)
                {
                    if (cp.Name == "DeprecatedFlag")
                    {
                        docPropsToRemove.Add(cp);
                    }
                }

                foreach (CustomProp cp in docPropsToRemove)
                {
                    docCustomProps.Remove(cp);
                }

                // ---------- Remove shape-level custom properties named "DeprecatedFlag" ----------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection.
                        if (shape.Props == null)
                            continue;

                        List<Prop> shapePropsToRemove = new List<Prop>();

                        foreach (Prop prop in shape.Props)
                        {
                            // Prop.Name holds the property name.
                            if (prop.Name == "DeprecatedFlag")
                            {
                                shapePropsToRemove.Add(prop);
                            }
                        }

                        foreach (Prop prop in shapePropsToRemove)
                        {
                            shape.Props.Remove(prop);
                        }
                    }
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(diagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {diagramPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }
}