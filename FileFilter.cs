//February 29, 2019
//Michael Bodie
//CSV file sorter
//Class FileFIilter will read data from a CSV file based on its path location.
//Within the class, Filter method will filter data based on a value being located in a particular column.
//The SortAscend method will sort the filtered data into ascending order.
//The Save method will save the file in a target path location.

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace CSV_File_Reader
{
    public class FileFilter
    {
        public string[] originalData
        {
            get;
            set;
        }
        public string[] filteredData
        {
            get;
            set;
        }

        Boolean hasHeader;

        public FileFilter(string sourceFile, Boolean headerFlag)
        {
            hasHeader = headerFlag;

            //try reading sourceFile (string of path where data is stored).
            try
            {
                String file = File.ReadAllText(sourceFile);
                Console.WriteLine("Read from file: " + sourceFile);
                Console.WriteLine("Here is what I see" + file);

            }
            //Catch exception where the file is not found at sourceFile path location.
            catch (Exception E)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(E.Message);
            }
            //Create an empty list to add all lines from file.
            //Once all elements are added, cast list into a string array.
            var myList = new List<string>();
            var lines = File.ReadLines(sourceFile);
            foreach (string line in lines)
            {
                //myList will be a list of strings holding all lines of data.
                myList.Add(line);
            }
            //Cast myList into an array of strings called originalData.
            originalData = myList.ToArray();
        }

        
        public void Filter(string value, int idx)
        {
            
            var list = new List<string>();
            //Create an empty list of strings called List.
            foreach (string s in originalData)
            {
                //Split each row of data on commas in order to differentiate column locations.
                var elemList = s.Split(',');
                //If value is a substring of elemList at index position idx, set boolean contains to True.
                bool contains = elemList[idx].Contains(value, StringComparison.OrdinalIgnoreCase);

                if (contains == true)
                {
                    list.Add(s);
                    //Add all elements to list where value is a substring of a row value from column idx.
                }
            }
            //Cast list to an array of strings called filteredData.
            filteredData = list.ToArray();
            if (filteredData.Length == 0)
            {
                //Print below sentence if there are no matches of a particular value in a chosen column, idx.
                Console.WriteLine("No matches were found in index " + idx + " for value " + value + ".");
            }
        }

        //Overload of Filter method to accomodate string parameters for index.
        public void Filter(string value, string idx)
        {
            //Initialize location variable required for compilation.
            int location = 0;

            //If the hasHeader is true, there is a text header. Run getHeaderIndex method to get integer location
            //of string value passed. 
            if (hasHeader == true)
            {
                location = getHeaderIndex(idx);
                Console.WriteLine("This is the location of idx: " + location);
            }
            else
            {
                Console.WriteLine("File does not contain a header, invalid calling parameters. Exiting method.");
                return;
            }

            //Create empty list of strings.
            var list = new List<string>();

            foreach (string s in originalData)
            {
                //Split each row of data on commas in order to differentiate column locations.
                var elemList = s.Split(',');
                //If value is a substring of elemList at index position idx, set boolean contains to True.
                bool contains = elemList[location].Contains(value, StringComparison.OrdinalIgnoreCase);

                if (contains == true)
                {
                    //Add all elements to list where value is a substring of a row value from column idx.
                    list.Add(s);
                }
            }
            //Cast list to an array of strings called filteredData.
            filteredData = list.ToArray();

            if (filteredData.Length == 0)
            {
                //Print below sentence if there are no matches of a particular value in a chosen column, idx.
                Console.WriteLine("No matches were found in index " + idx + " for value " + value + ".");
            }
        }

        //Method to take in string of a column name and return the integer index value of its column location.
        int getHeaderIndex(string columnName)
        {
            //Creates new list to store only headers of columns.
            var headerList = new List<string>();
            //Saves unsplit headers as variable headers.
            string headers = originalData[0];
            //Splits headers on commas to differentiate column locations.
            var headerSplit = headers.Split(',');
            
            foreach (string s in headerSplit)
            {
                //Add header values to a new list in order to search for the location of string columnName.
                headerList.Add(s);
            }
            //Save integer index value for the location of columnName.
            int location = headerList.IndexOf(columnName);
            return location;
        }


        public void SortAscend(int idx)
        {
            //Create new empty list of strings called list.
            var list = new List<string>();

            //For each line of filteredData, split based on commas and order the lines based on integer column index in ascending order.
            //Select all lines and save to variable sortQuery.
            var sortQuery = from line in filteredData
                            let fields = line.Split(',')
                            orderby fields[idx] ascending
                            select line;

            foreach (string line in sortQuery)
            {
                //Append all elements of sortQuery to list. This list will be sorted.
                list.Add(line);
            }
            //Cast list to filteredData array of strings.
            filteredData = list.ToArray();
            

        }

        //overload function with string as parameter
        public void SortAscend(string idx)
        {
            //Initialize location variable required for compilation.
            int location = 0;


            //If the hasHeader is true, there is a text header. Run getHeaderIndex method to get integer location
            //of string value passed. 
            if (hasHeader == true)
            {
                //Call getHeaderIndex function in order to get the int index value of a string column name, idx.
                location = getHeaderIndex(idx);
                Console.WriteLine("This is the location of idx: " + location);
            }
            else
            {
                Console.WriteLine("File does not contain a header, invalid calling parameters. Exiting method.");
                return;
            }

            //Create new empty list of strings called list.
            var list = new List<string>();

            //For each line of filteredData, split based on commas and order the lines based on integer column index in ascending order.
            //Select all lines and save to variable sortQuery.
            var sortQuery = from line in filteredData
                           let fields = line.Split(',')
                           orderby fields[location] ascending
                           select line;

            foreach (string line in sortQuery)
            {
                //Append all elements of sortQuery to list. This list will be sorted.
                list.Add(line);
            }
            //Cast list to filteredData array of strings.
            filteredData = list.ToArray();
            
        }

        public void Save(string fileName)
        {
            //If filteredData is null, cannot write filtered data to fileName.
            if (filteredData != null)
            {
                //Write all lines from filteredData into the path of fileName.
                File.WriteAllLines(fileName, filteredData);
            }
            else
            {

                Console.WriteLine("filteredData is null so no file can be written.");
            }
            
        }
    }
}
