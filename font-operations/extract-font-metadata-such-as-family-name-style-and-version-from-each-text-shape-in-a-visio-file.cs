using System;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file; can be passed as a command‑line argument
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape contains visible text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}");

                            // Iterate over character formatting runs within the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font family name used by this character run
                                string fontFamily = ch.FontName.Value;

                                // Style is a bitmask (StyleValue enum); convert to string for readability
                                string style = ch.Style.Value.ToString();

                                // Attempt to locate the corresponding Font object in the diagram to obtain version info
                                string version = "N/A";
                                foreach (Font f in diagram.Fonts)
                                {
                                    if (f.Name == fontFamily)
                                    {
                                        // Font.Version may not exist in all versions; use reflection to avoid compile errors
                                        PropertyInfo versionProp = typeof(Font).GetProperty("Version");
                                        if (versionProp != null)
                                        {
                                            object val = versionProp.GetValue(f);
                                            version = val?.ToString() ?? "N/A";
                                        }
                                        break;
                                    }
                                }

                                Console.WriteLine($"  Font Family: {fontFamily}, Style: {style}, Version: {version}");
                            }
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }