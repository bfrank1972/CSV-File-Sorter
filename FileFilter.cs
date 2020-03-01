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
        Boolean HasHeader;

        public FileFilter(string sourceFile)
        {

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
            
            var myList = new List<string>();
            //Create an empty list to add all lines from file.
            //Once all elements are added, cast list into a string array.
            var lines = File.ReadLines(sourceFile);
            foreach (string line in lines)
            {
                myList.Add(line);
                //myList will be a list of strings holding all lines of data.
            }
            originalData = myList.ToArray();
            //Cast myList into an array of strings called originalData.
            
        }

        
        public void Filter(string value, int idx)
        {
            
            var list = new List<string>();
            //Create an empty list of strings called List.
            foreach (string s in originalData)
            {
                var elemList = s.Split(',');
                //Split each row of data on commas in order to differentiate column locations.
                bool contains = elemList[idx].Contains(value, StringComparison.OrdinalIgnoreCase);
                //If value is a substring of elemList at index position idx, set boolean contains to True.
                if (contains == true)
                {
                    list.Add(s);
                    //Add all elements to list where value is a substring of a row value from column idx.
                }
            }
            filteredData = list.ToArray();
            //Cast list to an array of strings called filteredData.
            if (filteredData.Length == 0)
            {
                //Print below sentence if there are no matches of a particular value in a chosen column, idx.
                Console.WriteLine("No matches were found in index " + idx + " for value " + value + ".");
            }
        }

        //Overload of Filter method to accomodate string parameters for index.
        public void Filter(string value, string idx)
        {
            
            int location = getHeaderIndex(idx);
            //Call getHeaderIndex function in order to get the int index value of a string column name, idx.
            Console.WriteLine("This is the location of idx: " + location);
            //Print out integer index location.
            var list = new List<string>();
            //Create empty list of strings.
            foreach (string s in originalData)
            {
                var elemList = s.Split(',');
                //Split each row of data on commas in order to differentiate column locations.
                bool contains = elemList[location].Contains(value, StringComparison.OrdinalIgnoreCase);
                //If value is a substring of elemList at index position idx, set boolean contains to True.
                if (contains == true)
                {
                    list.Add(s);
                    //Add all elements to list where value is a substring of a row value from column idx.
                }
            }
            filteredData = list.ToArray();
            //Cast list to an array of strings called filteredData.
            if (filteredData.Length == 0)
            {
                Console.WriteLine("No matches were found in index " + idx + " for value " + value + ".");
                //Print below sentence if there are no matches of a particular value in a chosen column, idx.
            }
        }

        //Method to take in string of a column name and return the integer index value of its column location.
        int getHeaderIndex(string columnName)
        {
            var headerList = new List<string>();
            //Creates new list to store only headers of columns.
            string headers = originalData[0];
            //Saves unsplit headers as variable headers.
            var headerSplit = headers.Split(',');
            //Splits headers on commas to differentiate column locations.
            foreach (string s in headerSplit)
            {
                headerList.Add(s);
                //Add header values to a new list in order to search for the location of string columnName.
            }
            int location = headerList.IndexOf(columnName);
            //Save integer index value for the location of columnName.
            return location;
        }


        public void SortAscend(int idx)
        {
            var list = new List<string>();
            //Create new empty list of strings called list.

            var sortQuery = from line in filteredData
                            let fields = line.Split(',')
                            orderby fields[idx] ascending
                            select line;
            //For each line of filteredData, split based on commas and order the lines based on integer column index in ascending order.
            //Select all lines and save to variable sortQuery.

            foreach (string line in sortQuery)
            {
                list.Add(line);
                //Append all elements of sortQuery to list. This list will be sorted.
            }
            filteredData = list.ToArray();
            //Cast list to filteredData array of strings.

        }

        //overload function with string as parameter
        public void SortAscend(string idx)
        {
            
            int location = getHeaderIndex(idx);
            //Call getHeaderIndex function in order to get the int index value of a string column name, idx.

            // Demonstrates how to return query from a method.  
            // The query is executed here.
            var list = new List<string>();
            //Create new empty list of strings called list.
            var sortQuery = from line in filteredData
                           let fields = line.Split(',')
                           orderby fields[location] ascending
                           select line;
            //For each line of filteredData, split based on commas and order the lines based on integer column index in ascending order.
            //Select all lines and save to variable sortQuery.

            foreach (string line in sortQuery)
            {
                list.Add(line);
                //Append all elements of sortQuery to list. This list will be sorted.
            }
            filteredData = list.ToArray();
            //Cast list to filteredData array of strings.
        }

        public void Save(string fileName)
        {
            
            
            File.WriteAllLines(fileName, filteredData);
            //Write all lines from filteredData into the path of fileName.
            
            
        }
    }
}
