using System;
using System.Collections.Generic;
using System.Threading;


namespace SparseMatrix
{
    class Program
    {
        private static readonly int SIZE = 5;

        static void Main(string[] args)
        {
            int[,] matrix = new int[SIZE, SIZE];
            int[] vector = new int[SIZE];
            Random rand = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = rand.Next(0, matrix.GetLength(1) / 2); j < matrix.GetLength(1); j = j + rand.Next(1, matrix.GetLength(1)))
                {
                    matrix[i, j] = rand.Next(1, 10);
                }
            }

            for (int a = rand.Next(0, matrix.GetLength(1) / 2); a < matrix.GetLength(1); a = a + rand.Next(1, matrix.GetLength(1)))
            {
                vector[a] = rand.Next(1, 10);
            }

            SparseMatrix sparse = new SparseMatrix(matrix);


            //Console.Write("Vektor: ");
            //for (int a = 0; a < matrix.GetLength(1); a++)
            //{
            //    Console.Write(vector[a] + " ");
            //}

            //Console.WriteLine();
            //Console.Write("Matrica: ");
            //Console.WriteLine();

            //for (int i1 = 0; i1 < matrix.GetLength(0); i1++)
            //{
            //    for (int j1 = 0; j1 < matrix.GetLength(1); j1++)
            //    {
            //        Console.Write(matrix[i1, j1] + " ");
            //    }
            //    Console.WriteLine();
            //}



            /*Console.Write("Value niz: ");
            for (int z = 0; z < value.Count; z++)
            {
                Console.Write(value[z] + " ");
            }

            Console.WriteLine();
            Console.Write("Column niz: ");

            for (int f = 0; f < column.Count; f++)
            {
                Console.Write(column[f] + " ");

            }

            Console.WriteLine();
            Console.Write("Row_id niz: ");

            for (int g = 0; g < row_id.GetLength(0); g++)
            {
                Console.Write(row_id[g] + " ");

            }
            Console.WriteLine();*/
            for (int noOfThreads = 1; noOfThreads < 12; noOfThreads *= 2)
            {
                Console.WriteLine("Izvedba s " + noOfThreads + " threada:");

                List<ThreadStart> start = new List<ThreadStart>(noOfThreads);
                List<Thread> thread = new List<Thread>(noOfThreads);
                for (int threadId = 0; threadId < noOfThreads; threadId++)
                {
                    var id = threadId;
                    var threads = noOfThreads;
                    start.Add(delegate { sparse.Multiply(id, threads, vector); });
                    thread.Add(new Thread(start[threadId]));
                }
                for (int threadId = 0; threadId < noOfThreads; threadId++)
                {
                    thread[threadId].Start();
                }
                for (int threadId = 0; threadId < noOfThreads; threadId++)
                {
                    thread[threadId].Join();
                }
                Console.WriteLine();
                Console.WriteLine("Result: ");
                foreach (int res in sparse.Result)
                {
                    Console.Write(res);
                    Console.Write(" ");
                }
                Console.WriteLine();
                Console.WriteLine();

                sparse.Result.Clear();  //  so it wouldn't add over old values
            }
        }
        /*Console.Write("Result niz: ");
        for (int a = 0; a < result.Count; a++)
        {
            Console.Write(result[a] + " ");
        }*/
    }
}

