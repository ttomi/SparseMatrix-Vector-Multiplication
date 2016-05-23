using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SparseMatrix
{
    public class SparseMatrix
    {
        List<int> column = new List<int>();
        List<int> value = new List<int>();
        List<int> row = new List<int>();
        public List<int> Result = new List<int>();


        public SparseMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = false;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        value.Add(matrix[i, j]);
                        if (flag == false)
                        {
                            row.Add(value.Count - 1);
                            flag = true;
                        }
                        column.Add(j);
                    }
                }
            }
            //////////////////////////////
            //// Ispis
            //////////////////////////////

            //Console.Write("Svi NNZ: \n");
            //foreach (int v in value)
            //{
            //    Console.Write(v + " ");
            //}

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.Write("Redom stupci NNZ-ova: \n");

            //foreach (int c in column)
            //{
            //    Console.Write(c + " ");
            //}

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.Write("Indeksi prvih NNZ u svakom retku: \n");

            //foreach (int r in row)
            //{
            //    Console.Write(r + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine();
        }

        public void Multiply(int slice, int noOfThreads, int[] vertex)
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("Pocetak " + slice + ". komada: " + sw.Elapsed);

            for (int i = slice * row.Count / noOfThreads; i < (slice + 1) * row.Count / noOfThreads; i++)
            {
                Result.Add(0);
                int j = row[i];
                if (j < row[(row.Count) - 1])
                    for (j = row[i];
                         j < row[i + 1]; j++)
                    {
                        Result[i] += value[j] * vertex[column[j]];
                    }
                else
                    for (j = row[i];
                             j < (value.Count); j++)
                    {
                        Result[i] += value[j] * vertex[column[j]];
                    }
            }
            sw.Stop();
            Console.WriteLine("Vrijeme izvršavanja " + (slice + 1) + ". komada: " + sw.Elapsed);
        }

    }
}
