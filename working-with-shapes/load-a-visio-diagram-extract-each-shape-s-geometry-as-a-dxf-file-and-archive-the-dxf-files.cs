using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = @"C:\Input\diagram.vsdx";

                // Temporary folder to store generated DXF files
                string tempDxfFolder = Path.Combine(Path.GetTempPath(), "VisioDxfExport_" + Guid.NewGuid());
                Directory.CreateDirectory(tempDxfFolder);

                // Load the Visio diagram using the provided constructor (lifecycle rule)
                using (Diagram diagram = new Diagram(visioPath))
                {
                    int shapeIndex = 0;

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Build a unique DXF file name for each shape
                            string dxfFileName = Path.Combine(tempDxfFolder, $"Page{page.ID}_Shape{shape.ID}_{shapeIndex}.dxf");
                            shapeIndex++;

                            // ----- BEGIN CUSTOM DXF EXPORT LOGIC -----
                            // Aspose.Diagram does not provide a direct DXF export method in the documented API.
                            // The following placeholder demonstrates how one might extract the shape's geometry
                            // and write it to a DXF file. Replace this block with actual DXF generation code
                            // using the shape's Geometry collection or any other available API.
                            using (StreamWriter writer = new StreamWriter(dxfFileName))
                            {
                                // Write minimal DXF header
                                writer.WriteLine("0");
                                writer.WriteLine("SECTION");
                                writer.WriteLine("2");
                                writer.WriteLine("ENTITIES");

                                // Example: write a simple LINE entity using shape's geometry data.
                                // Replace the dummy coordinates with real values extracted from the shape.
                                // The coordinates below are placeholders.
                                writer.WriteLine("0");
                                writer.WriteLine("LINE");
                                writer.WriteLine("8"); // Layer name
                                writer.WriteLine("0");
                                writer.WriteLine("10"); // X1
                                writer.WriteLine("0.0");
                                writer.WriteLine("20"); // Y1
                                writer.WriteLine("0.0");
                                writer.WriteLine("30"); // Z1
                                writer.WriteLine("0.0");
                                writer.WriteLine("11"); // X2
                                writer.WriteLine("1.0");
                                writer.WriteLine("21"); // Y2
                                writer.WriteLine("1.0");
                                writer.WriteLine("31"); // Z2
                                writer.WriteLine("0.0");

                                // End of ENTITIES section
                                writer.WriteLine("0");
                                writer.WriteLine("ENDSEC");
                                writer.WriteLine("0");
                                writer.WriteLine("EOF");
                            }
                            // ----- END CUSTOM DXF EXPORT LOGIC -----
                        }
                    }
                }

                // Create a ZIP archive containing all DXF files
                string zipPath = @"C:\Output\VisioShapesDXF.zip";
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                ZipFile.CreateFromDirectory(tempDxfFolder, zipPath, CompressionLevel.Optimal, false);

                // Clean up temporary DXF files
                Directory.Delete(tempDxfFolder, true);

                Console.WriteLine($"DXF files have been archived to: {zipPath}");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }