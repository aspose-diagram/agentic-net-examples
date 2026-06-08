using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class RemoveHiddenInfoAndCompareSize
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = @"C:\VisioFiles\sample.vsdx";

            // Load the diagram (lifecycle rule: load)
            Diagram diagram = new Diagram(inputPath);

            // Get file size before removing hidden information (using metered consumption)
            decimal sizeBefore = Metered.GetConsumptionQuantity();

            // Check if the diagram contains hidden information
            if (diagram.HasHiddenInfo())
            {
                // Combine all hidden info items to be removed
                int itemsToRemove = (int)(
                    RemoveHiddenInfoItem.PersonalInfo |
                    RemoveHiddenInfoItem.Shapes |
                    RemoveHiddenInfoItem.Masters |
                    RemoveHiddenInfoItem.Styles |
                    RemoveHiddenInfoItem.DataRecordSets);

                // Remove hidden information (feature rule)
                diagram.RemoveHiddenInformation(itemsToRemove);
            }

            // Define a temporary output file path
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath), "sample_cleaned.vsdx");

            // Save the cleaned diagram (lifecycle rule: save)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Load the cleaned diagram to get its consumption size
            Diagram cleanedDiagram = new Diagram(outputPath);
            decimal sizeAfter = Metered.GetConsumptionQuantity();

            // Compare sizes and display the result
            Console.WriteLine($"Size before cleaning: {sizeBefore} bytes");
            Console.WriteLine($"Size after cleaning : {sizeAfter} bytes");

            if (sizeAfter < sizeBefore)
            {
                decimal reduction = sizeBefore - sizeAfter;
                Console.WriteLine($"File size reduced by {reduction} bytes.");
            }
            else if (sizeAfter == sizeBefore)
            {
                Console.WriteLine("No size reduction detected.");
            }
            else
            {
                decimal increase = sizeAfter - sizeBefore;
                Console.WriteLine($"File size increased by {increase} bytes.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
