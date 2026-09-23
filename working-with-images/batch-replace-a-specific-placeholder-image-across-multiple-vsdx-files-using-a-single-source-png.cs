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

                // Folder containing the source VSDX files
                string sourceFolder = @"C:\VisioFiles";
                // Folder to save the updated VSDX files (can be the same as sourceFolder)
                string outputFolder = @"C:\VisioFiles\Updated";

                // Path to the replacement PNG image
                string replacementImagePath = @"C:\Images\NewPlaceholder.png";

                // Ensure output folder exists
                Directory.CreateDirectory(outputFolder);

                // Load the replacement image once
                byte[] replacementImageBytes;
                using (FileStream imgStream = new FileStream(replacementImagePath, FileMode.Open, FileAccess.Read))
                using (MemoryStream ms = new MemoryStream())
                {
                    imgStream.CopyTo(ms);
                    replacementImageBytes = ms.ToArray();
                }

                // Process each VSDX file in the source folder
                string[] diagramFiles = Directory.GetFiles(sourceFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
                foreach (string diagramPath in diagramFiles)
                {
                    try
                    {
                        // Load the Visio diagram
                        Diagram diagram = new Diagram(diagramPath);

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify placeholder images:
                                // - Must be a foreign (image) shape
                                // - NameU (master name) matches the placeholder identifier (e.g., "Placeholder")
                                if (shape.Type == TypeValue.Foreign && shape.NameU != null && shape.NameU.Equals("Placeholder", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Replace the embedded image data
                                    if (shape.ForeignData != null)
                                    {
                                        shape.ForeignData.Value = replacementImageBytes;
                                    }
                                }
                            }
                        }

                        // Determine output file path
                        string fileName = Path.GetFileName(diagramPath);
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the updated diagram (overwrite or new file)
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
                    }
                }

                Console.WriteLine("Batch image replacement completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }