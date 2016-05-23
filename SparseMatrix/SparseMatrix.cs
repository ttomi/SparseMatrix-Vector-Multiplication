using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SparseMatrix
{
    public class SparseMatrix
    {
        List<int> column = new List<int>();
        public List<int> value = new List<int>();
        List<int> row = new List<int>();


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

        public void Multiply(int slice, int noOfThreads, int[] vertex, int[] result)
        {
            int cSlice = slice;
            int cNoOfThreads = noOfThreads;
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("Pocetak " + (cSlice + 1) + ". komada: " + sw.Elapsed);

            for (int i = cSlice * row.Count / cNoOfThreads; i < (cSlice + 1) * row.Count / cNoOfThreads; i++)
            {
                var ci = i;
                int j = row[ci];
                if (j < row[(row.Count) - 1])
                    for (j = row[ci]; j < row[ci + 1]; j++)
                    {
                        if (vertex[column[j]] != 0) 
                            result[ci] += value[j] * vertex[column[j]];
                    }
                else
                    for (j = row[ci]; j < (value.Count); j++)
                    {
                        if (vertex[column[j]] != 0)
                            result[ci] += value[j] * vertex[column[j]];
                    }
            }
            sw.Stop();
            Console.WriteLine("Vrijeme izvršavanja " + (cSlice + 1) + ". komada: " + sw.Elapsed);
        }

    }
}
