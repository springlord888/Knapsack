using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//一个背包物品
namespace KnapsackDemo
{
    public class Item
    {
        public Item(int w, int v)
        {
            Weight = w;
            Value = v;
        }

        public int Weight { set; get; } = 0;//重量
        public int Value { set; get; } = 0;//价值
    }
}
