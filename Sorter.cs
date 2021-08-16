using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Student Name: Yang Beng Ng(Ivan)
//Student ID: 30031552
//Date 16/08/2021
/*Description:You are required to make a list of different annual salaries for payroll
in whole numbers (integers) that will then need to be sorted, you should have
alternate methods of sorting so that payroll can decide on which method they would
like to use.
You need to create an application that creates lists of integer values between 10K
and 10 million. Your application must have the ability to sort in three different styles
with timers to indicate the speed at which this happens you must have at least 1
million items in your list as this the future business strategy to employ at least this
many staff. The current system is only able to handle 12 staff. Only 1 sorting
technique may use the inbuilt sorting the rest you must write yourself.
 */

namespace PayrollSort
{
    class Sorter
    {
        private int[] unsortedPay;
        private int[] pay;
        private int numOfData, numOfRuns;
        private List<long> insertTimes = new List<long>();
        private List<long> mergeTimes = new List<long>();
        private List<long> heapTimes = new List<long>();
        private Stopwatch sw;
        private bool ini = false;
        private int min, max;
        private string fileName;

        public Sorter()
        {
            
        }

        public Sorter(int min, int max)
        {
            Random rand = new Random();
            this.min = min;
            this.max = max;
            sw = new Stopwatch();
        }

        private void ResetUnsortedArray() 
        {
            if (unsortedPay != null)
            {
                Array.Clear(unsortedPay, 0, numOfData);
            }
            Array.Copy(pay, unsortedPay, numOfData);
        }

        private void AddTimer(string type, long time) 
        {
            if (type.Equals("Insertion"))
            {
                insertTimes.Add(time);
            }
            else if (type.Equals("Merge"))
            {
                mergeTimes.Add(time);
            }
            else if (type.Equals("Heap"))
            {
                heapTimes.Add(time);
            }
        }

        private void Write()
        {
            try
            {
                fileName = "SortTimes" + numOfData + ".csv";
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {

                        sw.WriteLine("{0},{1},{2}", "Insertion", "Merge", "Heap");

                        for (int i = 0; i < numOfRuns; i++)
                        {
                            sw.WriteLine("{0},{1},{2}", insertTimes[i], mergeTimes[i], heapTimes[i]);
                        }

                    }
                }
                insertTimes.Clear();
                mergeTimes.Clear();
                heapTimes.Clear();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PopulateArray(int numOfData)
        {
            if (pay != null)
            {
                Array.Clear(pay, 0, pay.Length);
                Array.Clear(unsortedPay, 0, unsortedPay.Length);
            }
            pay = new int[numOfData];
            unsortedPay = new int[numOfData];
            Random rand = new Random();
            for (int i = 0; i < numOfData; i++)
            {
                pay[i] = rand.Next(min, max);
            }

        }

        public void Run(int numOfRuns, int numOfData)
        {
            Console.WriteLine("Starting to sort " + numOfData + " of data " + numOfRuns + " amount of times...");
            this.numOfData = numOfData;
            this.numOfRuns = numOfRuns;
            PopulateArray(numOfData);
            for (int i = 0; i < numOfRuns; i++)
            {
                ResetUnsortedArray();
                Console.WriteLine("Insertion sorting...");
                sw.Reset();
                sw.Start();
                
                InsertionSort();
                sw.Stop();
                AddTimer("Insertion", Convert.ToInt64(sw.Elapsed.TotalMilliseconds * 1000000));

                ResetUnsortedArray();
                Console.WriteLine("Merge sorting...");
                sw.Reset();
                sw.Start();
                
                unsortedPay = MergeSort(unsortedPay);
                sw.Stop();
                AddTimer("Merge", Convert.ToInt64(sw.Elapsed.TotalMilliseconds * 1000000));

                ResetUnsortedArray();
                Console.WriteLine("Heap sorting...");
                sw.Reset();
                sw.Start();
                HeapSort(unsortedPay);
                sw.Stop();
                AddTimer("Heap", Convert.ToInt64(sw.Elapsed.TotalMilliseconds * 1000000));

                Array.Clear(pay, 0, pay.Length);
                Random rand = new Random();
                for (int j = 0; j < numOfData; j++)
                {
                    pay[j] = rand.Next(min, max);
                }
            }
            
            Write();
            Console.WriteLine("Complete!");
        }

        //Insertion sort
        //Code was sourced from: https://www.geeksforgeeks.org/insertion-sort/
        private void InsertionSort() 
        {
            int n = unsortedPay.Length;
            for (int i = 0; i < n; i++)
            {
                int key = unsortedPay[i];
                int j = i - 1;

                while (j >= 0 && unsortedPay[j] > key) 
                {
                    unsortedPay[j + 1] = unsortedPay[j];
                    j = j - 1;
                }
                unsortedPay[j + 1] = key;
            }
        }

        //Merge sort
        //Code was sourced from: https://www.c-sharpcorner.com/blogs/a-simple-merge-sort-implementation-c-sharp
        private int[] Merge (int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];

            int indexLeft = 0, indexRight = 0, indexResult = 0;
            
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                   
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }

        private int[] MergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            
            if (array.Length <= 1)
                return array;
            
            int midPoint = array.Length / 2;
            
            left = new int[midPoint];

            
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            else
                right = new int[midPoint + 1];
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            int x = 0;
            
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            
            left = MergeSort(left);
            
            right = MergeSort(right);
            
            result = Merge(left, right);
            return result;

        }

        //Heap sort
        //Code was sourced from: https://www.tutorialspoint.com/heap-sort-in-chash
        private void HeapSort(int[] array)
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            for (int i = n - 1; i >= 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                Heapify(array, i, 0);
            }
        }

        private void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left] > array[largest])
                largest = left;
            if (right < n && array[right] > array[largest])
                largest = right;
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;
                Heapify(array, n, largest);
            }
        }
    }
}
