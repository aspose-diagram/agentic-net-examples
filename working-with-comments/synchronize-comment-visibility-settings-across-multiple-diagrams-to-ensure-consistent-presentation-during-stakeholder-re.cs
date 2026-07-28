using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Entry point
        static void Main(string[] args)
        {
            // Folder containing the Visio files (adjust as needed)
            string diagramsFolder = @"C:\VisioDiagrams";

            // Get all VSDX files in the folder
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

            if (diagramFiles.Length == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            // Load the first diagram as the reference for comment settings
            Diagram referenceDiagram = new Diagram(diagramFiles[0]);

            // Build a lookup of comment settings from the reference diagram
            var referenceComments = BuildCommentLookup(referenceDiagram);

            // Process remaining diagrams
            for (int i = 1; i < diagramFiles.Length; i++)
            {
                string filePath = diagramFiles[i];
                Diagram targetDiagram = new Diagram(filePath);

                SynchronizeComments(targetDiagram, referenceComments);

                // Save the updated diagram (overwrite original)
                targetDiagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Synchronized comments for: {Path.GetFileName(filePath)}");
            }

            Console.WriteLine("Comment synchronization completed.");
        }

        // Builds a dictionary: PageName -> (MarkerIndex -> (CommentText, ReviewerID))
        private static Dictionary<string, Dictionary<int, (string Comment, int ReviewerId)>> BuildCommentLookup(Diagram diagram)
        {
            var lookup = new Dictionary<string, Dictionary<int, (string, int)>>();

            foreach (Page page in diagram.Pages)
            {
                string pageName = page.Name ?? page.NameU ?? $"Page_{page.ID}";
                var pageDict = new Dictionary<int, (string, int)>();

                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    int marker = ann.MarkerIndex.Value;
                    string comment = ann.Comment.Value;
                    int reviewerId = ann.ReviewerID.Value;

                    pageDict[marker] = (comment, reviewerId);
                }

                if (pageDict.Count > 0)
                {
                    lookup[pageName] = pageDict;
                }
            }

            return lookup;
        }

        // Updates comments in the target diagram to match the reference lookup
        private static void SynchronizeComments(Diagram diagram, Dictionary<string, Dictionary<int, (string Comment, int ReviewerId)>> reference)
        {
            foreach (Page page in diagram.Pages)
            {
                string pageName = page.Name ?? page.NameU ?? $"Page_{page.ID}";

                if (!reference.ContainsKey(pageName))
                    continue; // No reference comments for this page

                var refPageComments = reference[pageName];

                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    int marker = ann.MarkerIndex.Value;

                    if (refPageComments.TryGetValue(marker, out var refData))
                    {
                        // Update comment text and reviewer ID to match reference
                        ann.Comment.Value = refData.Comment;
                        ann.ReviewerID.Value = refData.ReviewerId;
                    }
                }
            }
        }
    }