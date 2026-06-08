using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Folder containing the Visio files to process
                string folderPath = @"C:\VisioDiagrams";

                // Get all Visio files (VSDX) in the folder
                string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
                if (diagramFiles.Length == 0)
                {
                    Console.WriteLine("No Visio files found in the specified folder.");
                    return;
                }

                // Load the first diagram as the reference for comment texts
                Diagram referenceDiagram = new Diagram(diagramFiles[0]);

                // Build a dictionary of reference comments:
                // Key = page name + "_" + marker index, Value = comment text
                var referenceComments = new Dictionary<string, string>();

                foreach (Page refPage in referenceDiagram.Pages)
                {
                    foreach (Annotation refAnnotation in refPage.PageSheet.Annotations)
                    {
                        string key = $"{refPage.Name}_{refAnnotation.MarkerIndex.Value}";
                        referenceComments[key] = refAnnotation.Comment.Value;
                    }
                }

                // Process each diagram (including the reference one to ensure it is saved)
                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        Diagram diagram = new Diagram(filePath);

                        foreach (Page page in diagram.Pages)
                        {
                            // Track existing annotation marker indices on this page
                            var existingMarkers = new HashSet<long>();
                            foreach (Annotation annotation in page.PageSheet.Annotations)
                            {
                                existingMarkers.Add(annotation.MarkerIndex.Value);
                                string key = $"{page.Name}_{annotation.MarkerIndex.Value}";
                                if (referenceComments.TryGetValue(key, out string refText))
                                {
                                    // Synchronize comment text
                                    annotation.Comment.Value = refText;
                                }
                            }

                            // Add missing comments from the reference diagram
                            foreach (var kvp in referenceComments)
                            {
                                // Split the key to obtain page name and marker index
                                var parts = kvp.Key.Split('_');
                                if (parts.Length != 2) continue;
                                string refPageName = parts[0];
                                if (!refPageName.Equals(page.Name, StringComparison.OrdinalIgnoreCase))
                                    continue;

                                if (long.TryParse(parts[1], out long markerIdx))
                                {
                                    if (!existingMarkers.Contains(markerIdx))
                                    {
                                        // Add a new comment at a default location (1,1)
                                        page.AddComment(1.0, 1.0, kvp.Value);
                                    }
                                }
                            }
                        }

                        // Save the updated diagram (overwrite original)
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Synchronized comments for: {Path.GetFileName(filePath)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                    }
                }

                Console.WriteLine("Comment synchronization completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }