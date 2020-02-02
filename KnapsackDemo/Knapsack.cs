using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackDemo
{
    class Knapsack
    {
        public Knapsack()
        {
            _InitValueTable();
            _InitItems();
        }


        //结果分析
        public void PrintResult()
        {
            //1. 能获得的最大价值
            int totalMaxValue = ValueTable[itemTypeNum - 1, BAG_MAX_AVAILABLE_WEIGHT];
            Console.WriteLine("能获得的最大价值为："+ totalMaxValue);
            //2. 取得最大价值时每个物体放入个数（即对应的方案）
            int[] results = new int[itemTypeNum];//每一个元素表示对应标号的物体，应该放入的个数
            int remainWeight = BAG_MAX_AVAILABLE_WEIGHT;
            for (int i = itemTypeNum-1; i >=0; i--)
            {
                int w = WeightRecordTable[i, remainWeight];//第1+1个物体所分配的重量份额
                int count = w / items[i].Weight; //向下取整
                results[i] = count;

                remainWeight = remainWeight - w;
            }
            Console.WriteLine("分配方案为：（每个物体的个数）");
            for (int i = 0; i < itemTypeNum; i++)
            {
                Console.Write(results[i]);
                Console.WriteLine("个  ");
            }

        }

        //动态规划算法
        public void DynamicProgramming()
        {
            //计算ValueTable[itemIndex,weight] 这个元素的含义是前itemIndex+1 (含)个物品，总重不超过weight，能获得的最大价值
            //1.初始值（第一个物体）
            for (int weight = 0; weight <= BAG_MAX_AVAILABLE_WEIGHT; weight++)
            {
                ValueTable[0, weight] = _GetMaxValue(items[0], weight);
                WeightRecordTable[0, weight] = weight;
            }

            //2.第2个物体开始
            for (int itemIndex = 1; itemIndex < itemTypeNum; itemIndex++)
            {
                for (int weight = 0; weight <= BAG_MAX_AVAILABLE_WEIGHT; weight++)
                {
                    
                    int tempMaxValue = -9999;
                    int weightRecord = 0;
                    for (int w = 0; w <= weight; w++)
                    {
                        //w是分给第itemIndex+1个物品的重量份额
                        if (ValueTable[itemIndex - 1, weight - w] == UnInitValue)
                        {
                            Console.WriteLine("出错了，ValueTable的这个元素没有初始化：", itemIndex - 1, weight - w);
                            return;
                        }
                        int value = _GetMaxValue(items[itemIndex], w) + ValueTable[itemIndex - 1, weight - w];
                        if (value> tempMaxValue)
                        {
                            tempMaxValue = value;
                            weightRecord = w;
                        }
                    }
                    ValueTable[itemIndex, weight] = tempMaxValue; //记录最大价值
                    WeightRecordTable[itemIndex, weight] = weightRecord;//记录重量的分配方案，在第itemIndex个物体上分配多少重量
                }
            }
        }

        private void _InitValueTable()
        {
            if (null != ValueTable)
            {
                int m = ValueTable.GetLength(0);
                int n = ValueTable.GetLength(1);
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        ValueTable[i, j] = UnInitValue;
                        WeightRecordTable[i, j] = 0;
                    }
                }
            }
        }

        private void _InitItems()
        {
            if (null== items)
            {
                items = new List<Item>();          
            }
            items.Clear();
          
            items.Add(new Item(2, 1));
            items.Add(new Item(3, 3));
            items.Add(new Item(4, 5));
            items.Add(new Item(7, 9));

            if (items.Count != itemTypeNum)
            {
                Console.WriteLine("物品初始化失败，物品的种类数不一致");
            }
        }

        //不超过weight的情况下，尽可能多的放入item所能得到的最大价值
        private int _GetMaxValue(Item item, int weight)
        {
            int maxValue = 0;
            if (null != item)
            {
                int maxCount = weight / item.Weight;
                maxValue = maxCount * item.Value;
            }
            return maxValue;
        }

        private List<Item> items = new List<Item>
        {
            new Item(2, 1) , new Item(3, 3) , new Item(4, 5), new Item(7, 9)
        }; //可放入背包的物品原型

        private const int BAG_MAX_AVAILABLE_WEIGHT = 10; //背包可容纳最大质量
        private const int itemTypeNum = 4;//物体种类个数
        private const int UnInitValue = -1;

        //ValueTable[i, j] ，表示对于前i+1个物体，总重不超过j，总的背包价值的最大值
        private int[,] ValueTable = new int[itemTypeNum,BAG_MAX_AVAILABLE_WEIGHT+1];
        //WeightRecordTable[i, j] ，表示对于前i+1个物体，总重不超过j，总的背包价值取最大时，第（i+1）个物体应该分配的重量份额
        private int[,] WeightRecordTable = new int[itemTypeNum,BAG_MAX_AVAILABLE_WEIGHT+1];
    }
}
