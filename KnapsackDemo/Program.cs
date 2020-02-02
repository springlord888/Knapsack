using System;
using System.Collections.Generic;


namespace KnapsackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Knapsack knapsack = new Knapsack();
            knapsack.DynamicProgramming();
            knapsack.PrintResult();
            Console.ReadLine();
        }



        
    }
}
