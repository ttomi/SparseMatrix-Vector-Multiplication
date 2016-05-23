using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace SparseMatrix
{
    class Program
    {
        private static readonly int SIZE = 50;

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

            for (int noOfThreads = 1; noOfThreads < 12; noOfThreads *= 2)
            {
                Stopwatch thStopwatch = Stopwatch.StartNew();
                Console.WriteLine("Izvedba s " + noOfThreads + " threada:");
                Console.WriteLine("Pocetno vrijeme: " + thStopwatch.Elapsed);
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
                    thread[threadId].Join();
                }
                
                Console.WriteLine("Krajnje vrijeme: " + thStopwatch.Elapsed);
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
    }
}

