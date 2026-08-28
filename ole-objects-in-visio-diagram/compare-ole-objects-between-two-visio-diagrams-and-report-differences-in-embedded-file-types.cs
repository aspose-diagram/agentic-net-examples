using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string firstDiagramPath = "Diagram1.vsdx";
                string secondDiagramPath = "Diagram2.vsdx";

                // Load the diagrams
                Diagram firstDiagram = new Diagram(firstDiagramPath);
                Diagram secondDiagram = new Diagram(secondDiagramPath);

                // Extract OLE embedded file type information from each diagram
                List<string> firstOleTypes = GetOleFileTypes(firstDiagram);
                List<string> secondOleTypes = GetOleFileTypes(secondDiagram);

                // Report differences
                ReportDifferences(firstOleTypes, secondOleTypes);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves a list of OLE embedded file identifiers (ObjectSourceFullName) from a diagram
        private static List<string> GetOleFileTypes(Diagram diagram)
        {
            List<string> oleTypes = new List<string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE foreign object
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        // Ensure it is an embedded OLE object
                        if (shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            string source = shape.ForeignData.ObjectSourceFullName;
                            if (!string.IsNullOrEmpty(source))
                            {
                                oleTypes.Add(source);
                            }
                        }
                    }
                }
            }

            return oleTypes;
        }

        // Compares two lists of OLE file identifiers and writes the differences to the console
        private static void ReportDifferences(List<string> firstList, List<string> secondList)
        {
            HashSet<string> firstSet = new HashSet<string>(firstList);
            HashSet<string> secondSet = new HashSet<string>(secondList);

            HashSet<string> onlyInFirst = new HashSet<string>(firstSet);
            onlyInFirst.ExceptWith(secondSet);

            HashSet<string> onlyInSecond = new HashSet<string>(secondSet);
            onlyInSecond.ExceptWith(firstSet);

            if (onlyInFirst.Count == 0 && onlyInSecond.Count == 0)
            {
                Console.WriteLine("Both diagrams contain the same OLE embedded file types.");
                return;
            }

            if (onlyInFirst.Count > 0)
            {
                Console.WriteLine("File types present only in the first diagram:");
                foreach (string type in onlyInFirst)
                {
                    Console.WriteLine($"  {type}");
                }
            }

            if (onlyInSecond.Count > 0)
            {
                Console.WriteLine("File types present only in the second diagram:");
                foreach (string type in onlyInSecond)
                {
                    Console.WriteLine($"  {type}");
                }
            }
        }
    }