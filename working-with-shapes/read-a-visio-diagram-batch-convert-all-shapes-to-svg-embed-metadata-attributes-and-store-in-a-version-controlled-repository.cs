using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputRepositoryPath>");
            return;
        }

        string inputPath = args[0];
        string outputRepoPath = args[1];

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists or create it.
        try
        {
            if (!Directory.Exists(outputRepoPath))
                Directory.CreateDirectory(outputRepoPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputRepoPath}': {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare SVG file path for the current shape.
                        string svgFileName = $"shape_{shape.ID}.svg";
                        string svgFullPath = Path.Combine(outputRepoPath, svgFileName);

                        // Export the shape to SVG using ImageSaveOptions.
                        try
                        {
                            ImageSaveOptions svgOptions = new ImageSaveOptions(SaveFileFormat.Svg);
                            shape.ToImage(svgFullPath, svgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} to SVG: {ex.Message}");
                            continue;
                        }

                        // Embed shape custom properties as metadata into the SVG file.
                        try
                        {
                            XmlDocument svgDoc = new XmlDocument();
                            svgDoc.Load(svgFullPath);

                            XmlElement root = svgDoc.DocumentElement; // <svg>
                            if (root == null)
                                throw new Exception("Invalid SVG format: missing root element.");

                            // Create <metadata> element.
                            XmlElement metadataElem = svgDoc.CreateElement("metadata");

                            // Add each custom property as a <customProp> element.
                            foreach (Prop prop in shape.Props)
                            {
                                XmlElement propElem = svgDoc.CreateElement("customProp");
                                propElem.SetAttribute("name", prop.Name);
                                propElem.SetAttribute("value", prop.Value?.Val ?? string.Empty);
                                metadataElem.AppendChild(propElem);
                            }

                            // Append metadata to the SVG root.
                            root.AppendChild(metadataElem);

                            // Save the modified SVG back to disk.
                            svgDoc.Save(svgFullPath);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to embed metadata for shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }

                // Optionally, save the diagram back (e.g., after any modifications).
                // Here we just keep the original diagram unchanged.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}