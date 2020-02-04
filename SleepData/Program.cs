using System;
using System.IO;
using System.Linq;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // specify path for data file
            string file = "/Users/lawl2/Desktop/WCTC/2020/Spring 2020/.NET Database Programming/NETDBProgramming/data.txt";

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                if (File.Exists(file))
                {
                    StreamReader streamReader = new StreamReader(file);
                    while (!streamReader.EndOfStream)
                    {
                        
                        string line = streamReader.ReadLine();
                        string[] arr = line.Split(',');
                        string[] hours = arr[1].Split('|');
                        Console.WriteLine("Week of {0,-20}", DateTime.Parse(arr[0]).ToString("MMM, dd, yyyy"));
                        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
                        Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
                        int total = 0;
                        foreach (var item in hours)
                        {
                            Console.Write("{0,3}", item);
                            total = total + int.Parse(item);
                        }

                        decimal avg = ((decimal) total) / 7;
                        Console.Write(" {0,3} ", total);
                        Console.Write("{0,3}", avg.ToString().Substring(0,3));
                        Console.WriteLine();
                        Console.WriteLine();

                    }
                    streamReader.Close();
                }
                else
                {
                    Console.WriteLine("File does not exist");
                }

            }
        }
    }
}
