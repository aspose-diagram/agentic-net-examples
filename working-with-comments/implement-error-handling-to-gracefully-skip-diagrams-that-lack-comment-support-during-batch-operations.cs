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

                // Input folder containing Visio files
                string inputFolder = @"C:\Visio\Input";
                // Output folder for processed files
                string outputFolder = @"C:\Visio\Output";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all Visio files (VSDX, VSD, VDX) in the input folder
                string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in files)
                {
                    // Process only supported Visio extensions
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through each page
                        foreach (Page page in diagram.Pages)
                        {
                            // Attempt to add a comment to the first shape on the page
                            try
                            {
                                Shape firstShape = null;
                                foreach (Shape shape in page.Shapes)
                                {
                                    firstShape = shape;
                                    break; // only need the first shape
                                }

                                if (firstShape != null)
                                {
                                    // Add a comment associated with the shape
                                    page.AddComment(firstShape, "Processed by batch operation");
                                    Console.WriteLine($"Added comment to shape ID {firstShape.ID} on page '{page.Name}'.");
                                }
                                else
                                {
                                    // No shapes on this page; add a page-level comment instead
                                    page.AddComment(1.0, 1.0, "Page processed - no shapes");
                                    Console.WriteLine($"Added page-level comment on page '{page.Name}'.");
                                }
                            }
                            catch (Exception exComment)
                            {
                                // Comment operation failed (e.g., diagram format does not support comments)
                                Console.WriteLine($"Comment not supported on page '{page.Name}' of file '{Path.GetFileName(filePath)}'. Skipping this page. Details: {exComment.Message}");
                                // Continue with next page
                            }
                        }

                        // Save the modified diagram to the output folder
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Successfully processed and saved: {Path.GetFileName(outputPath)}");
                    }
                    catch (Exception exLoad)
                    {
                        // Loading the diagram failed; skip this file
                        Console.WriteLine($"Failed to load file '{Path.GetFileName(filePath)}'. Skipping. Details: {exLoad.Message}");
                    }
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }