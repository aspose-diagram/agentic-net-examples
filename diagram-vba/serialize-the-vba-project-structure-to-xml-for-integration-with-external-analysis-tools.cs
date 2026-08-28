using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioFilePath = "input.vsdx";

                // Output XML file path
                string xmlOutputPath = "VbaProjectStructure.xml";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioFilePath);

                // Get the VBA project from the diagram
                VbaProject vbaProject = diagram.VbaProject;

                if (vbaProject == null)
                {
                    Console.WriteLine("No VBA project found in the diagram.");
                    return;
                }

                // Build XML representation
                XDocument xmlDoc = new XDocument(
                    new XElement("VbaProject",
                        new XAttribute("Name", vbaProject.Name ?? string.Empty),
                        new XAttribute("IsSigned", vbaProject.IsSigned),

                        // Optional: include raw VbProjectData as Base64 (if needed)
                        // new XElement("VbProjectData", Convert.ToBase64String(diagram.VbProjectData ?? new byte[0])),

                        // Modules
                        new XElement("Modules",
                            // Iterate through each module in the VBA project
                            IterateModules(vbaProject)
                        ),

                        // References
                        new XElement("References",
                            // Iterate through each reference in the VBA project
                            IterateReferences(vbaProject)
                        )
                    )
                );

                // Save the XML to file
                xmlDoc.Save(xmlOutputPath);
                Console.WriteLine($"VBA project structure serialized to '{xmlOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to create XML elements for modules
        private static object[] IterateModules(VbaProject vbaProject)
        {
            var moduleElements = new System.Collections.Generic.List<object>();

            // The Modules property returns a collection of VbaModule objects
            foreach (VbaModule module in vbaProject.Modules)
            {
                XElement moduleElement = new XElement("Module",
                    new XAttribute("Name", module.Name ?? string.Empty),
                    new XAttribute("Type", module.Type.ToString()),
                    new XElement("Codes", module.Codes ?? string.Empty)
                );

                moduleElements.Add(moduleElement);
            }

            return moduleElements.ToArray();
        }

        // Helper method to create XML elements for references
        private static object[] IterateReferences(VbaProject vbaProject)
        {
            var referenceElements = new System.Collections.Generic.List<object>();

            // The References property returns a VbaProjectReferenceCollection
            foreach (VbaProjectReference reference in vbaProject.References)
            {
                XElement referenceElement = new XElement("Reference",
                    new XAttribute("Name", reference.Name ?? string.Empty),
                    new XAttribute("Type", reference.Type.ToString()),
                    new XElement("Libid", reference.Libid ?? string.Empty),
                    new XElement("ExtendedLibid", reference.ExtendedLibid ?? string.Empty),
                    new XElement("RelativeLibid", reference.RelativeLibid ?? string.Empty),
                    new XElement("TwiddledLibid", reference.Twiddledlibid ?? string.Empty)
                );

                referenceElements.Add(referenceElement);
            }

            return referenceElements.ToArray();
        }
    }