using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Simple console interaction to get required parameters
            Console.Write("Enter the path to the Visio file: ");
            string inputPath = Console.ReadLine();

            Console.Write("Enter the number of days (comments older than this will be removed): ");
            if (!int.TryParse(Console.ReadLine(), out int days) || days < 0)
            {
                Console.WriteLine("Invalid number of days.");
                return;
            }

            Console.Write("Enter the output file path (will be saved as VSDM to preserve any macros): ");
            string outputPath = Console.ReadLine();

            try
            {
                RemoveOldComments(inputPath, days, outputPath);
                Console.WriteLine("Comments older than {0} days have been removed and the diagram saved to '{1}'.", days, outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Loads a Visio diagram, removes comments older than the specified number of days,
        /// and saves the modified diagram.
        /// </summary>
        /// <param name="filePath">Path to the source Visio file.</param>
        /// <param name="days">Age threshold in days.</param>
        /// <param name="outputPath">Path where the modified diagram will be saved.</param>
        static void RemoveOldComments(string filePath, int days, string outputPath)
        {
            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Determine the cutoff date
            DateTime cutoffDate = DateTime.Now.AddDays(-days);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect annotations that need to be removed
                List<Annotation> toRemove = new List<Annotation>();

                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Attempt to extract a date from the comment text.
                    // This assumes the comment starts with a date in ISO format (yyyy-MM-dd).
                    // Adjust parsing logic as needed for your actual comment format.
                    string commentText = annotation.Comment.Value;
                    if (string.IsNullOrWhiteSpace(commentText) || commentText.Length < 10)
                        continue;

                    if (DateTime.TryParse(commentText.Substring(0, 10), out DateTime commentDate))
                    {
                        if (commentDate < cutoffDate)
                        {
                            toRemove.Add(annotation);
                        }
                    }
                }

                // Remove the identified annotations
                foreach (Annotation ann in toRemove)
                {
                    // AnnotationCollection supports removal via the Remove method.
                    page.PageSheet.Annotations.Remove(ann);
                }
            }

            // Save the modified diagram. Using Vsdm to keep any existing macros intact.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
        }
    }