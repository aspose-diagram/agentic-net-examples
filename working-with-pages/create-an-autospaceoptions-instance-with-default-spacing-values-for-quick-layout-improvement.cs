using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create an AutoSpaceOptions instance.
        // The default constructor sets the default spacing:
        // DistanceInHorizontal = 0.375 inch, DistanceInVertical = 0.375 inch.
        AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();

        // (Optional) Access the default values.
        double horizontalSpacing = autoSpaceOptions.DistanceInHorizontal;
        double verticalSpacing = autoSpaceOptions.DistanceInVertical;
    }
}
