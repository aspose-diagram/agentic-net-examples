using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file path, output file path, number of days
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramCommentCleaner <input.vsdx> <output.vsdx> <days>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int days) || days < 0)
            {
                Console.WriteLine("The days argument must be a non‑negative integer.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the cutoff date
            DateTime cutoffDate = DateTime.Now.AddDays(-days);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect annotations that are older than the cutoff
                List<Annotation> toRemove = new List<Annotation>();

                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Annotation.Date is read‑only; its value can be accessed via .Value
                    // If the date is not set, treat it as the minimum value
                    DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                    if (commentDate < cutoffDate)
                    {
                        toRemove.Add(annotation);
                    }
                }

                // Remove the collected annotations
                foreach (Annotation oldAnnotation in toRemove)
                {
                    page.PageSheet.Annotations.Remove(oldAnnotation);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Comments older than {days} days have been removed and saved to '{outputPath}'.");
        }
    }