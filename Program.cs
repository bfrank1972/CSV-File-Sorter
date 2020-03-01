using System;

namespace CSV_File_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "/Users/mikebodie/SoftwareDevelopment/MikeSampleData/superbowl.csv";
            string targetFile = "/Users/mikebodie/SoftwareDevelopment/MikeSampleData/OutputData.csv";
            //Test 1
            FileFilter fileter = new FileFilter(sourceFile, true);
            //fileter.Filter("Qa", 2);
            //fileter.SortAscend(1);
            //fileter.Save(targetFile);

            ////Test 2
            fileter = new FileFilter(sourceFile, true);
            //fileter.HasHeader = true;
            fileter.Filter("Patriots", 2);
            fileter.SortAscend(3);
            fileter.Save(targetFile);

            ////Test 3
            //fileter.Filter("florida", "State");
            //fileter.SortAscend("Date");
            //fileter.Save(targetFile);
        }
    }
}
