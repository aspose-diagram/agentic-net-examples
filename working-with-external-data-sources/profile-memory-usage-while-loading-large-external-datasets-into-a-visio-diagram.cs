using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (template or existing diagram)
                string sourceFile = "template.vsdx";

                // Path to the output Visio file after loading data
                string outputFile = "output.vsdx";

                // Measure memory before loading the diagram
                long memoryBeforeLoad = GC.GetTotalMemory(forceFullCollection: true);
                Console.WriteLine($"Memory before loading diagram: {memoryBeforeLoad / 1024 / 1024} MB");

                // Load the diagram using the constructor that accepts a file name
                Diagram diagram = new Diagram(sourceFile);

                // Measure memory after diagram is loaded
                long memoryAfterLoad = GC.GetTotalMemory(forceFullCollection: true);
                Console.WriteLine($"Memory after loading diagram: {memoryAfterLoad / 1024 / 1024} MB");
                Console.WriteLine($"Memory increase for diagram load: {(memoryAfterLoad - memoryBeforeLoad) / 1024 / 1024} MB");

                // Simulate loading a large external dataset
                // Here we create a DataRecordSet and fill its ADOData property with a large XML string.
                DataRecordSet dataRecordSet = new DataRecordSet
                {
                    ID = 1,
                    Name = "LargeDataSet"
                };

                // Generate a large XML payload (e.g., 100 MB of dummy data)
                int rows = 500000; // Adjust to achieve desired size
                StringWriter xmlWriter = new StringWriter();
                xmlWriter.WriteLine(@"<?xml version=""1.0""?>");
                xmlWriter.WriteLine(@"<xml>");
                xmlWriter.WriteLine(@"<data>");
                for (int i = 0; i < rows; i++)
                {
                    xmlWriter.WriteLine($@"<row><Column1>Value{i}</Column1><Column2>{Guid.NewGuid()}</Column2></row>");
                }
                xmlWriter.WriteLine(@"</data>");
                xmlWriter.WriteLine(@"</xml>");
                dataRecordSet.ADOData = xmlWriter.ToString();

                // Measure memory after creating the large dataset
                long memoryAfterDataCreation = GC.GetTotalMemory(forceFullCollection: true);
                Console.WriteLine($"Memory after creating large DataRecordSet: {memoryAfterDataCreation / 1024 / 1024} MB");
                Console.WriteLine($"Memory increase for data creation: {(memoryAfterDataCreation - memoryAfterLoad) / 1024 / 1024} MB");

                // Add the DataRecordSet to the diagram's collection
                diagram.DataRecordSets.Add(dataRecordSet);

                // Measure memory after adding the DataRecordSet to the diagram
                long memoryAfterAdd = GC.GetTotalMemory(forceFullCollection: true);
                Console.WriteLine($"Memory after adding DataRecordSet to diagram: {memoryAfterAdd / 1024 / 1024} MB");
                Console.WriteLine($"Memory increase for adding to diagram: {(memoryAfterAdd - memoryAfterDataCreation) / 1024 / 1024} MB");

                // Optionally, refresh the data record set (no external DB in this example)
                // dataRecordSet.Refresh();

                // Save the diagram using the Save method with a specific format
                diagram.Save(outputFile, SaveFileFormat.Vsdx);

                // Measure memory after saving
                long memoryAfterSave = GC.GetTotalMemory(forceFullCollection: true);
                Console.WriteLine($"Memory after saving diagram: {memoryAfterSave / 1024 / 1024} MB");
                Console.WriteLine($"Total memory increase from start to end: {(memoryAfterSave - memoryBeforeLoad) / 1024 / 1024} MB");

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }