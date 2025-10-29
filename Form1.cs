using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscoverCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Maximum = numericUpDown1.Value;
            NumericUpDown_ValueChanged(sender, e);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            long x = (long)numericUpDown1.Value;
            long y = (long)numericUpDown2.Value;
            long z = (long)numericUpDown3.Value;
            label4.Text = $"概率为: {Calculator.CalculateProbability(x, y, z) * 100:F2}%";
        }
    }
    class Calculator
    {
        // 计算组合数 C(n, k) = n! / (k! * (n-k)!)
        static long Combination(long n, long k)
        {
            if (k > n)
                return 0;
            if (k == 0 || k == n)
                return 1;

            k = Math.Min(k, n - k); // 计算C(n, k)时，k应该取最小的
            long result = 1;
            for (int i = 1; i <= k; i++)
            {
                result = result * (n - (k - i)) / i;
            }
            return result;
        }

        // 计算至少抽到一张有效卡的概率
        public static double CalculateProbability(long x, long y, long z)
        {
            if (y > x)
            {
                return Double.NaN;
            }
            if (z > x)
            {
                return 1;
            }

            // 计算所有可能的组合
            long totalWays = Combination(x, z);

            // 计算抽不到有效卡的组合（即抽到的卡都是无效卡）
            long invalidWays = Combination(x - y, z);

            // 计算至少抽到一张有效卡的概率
            double probability = 1.0 - (double)invalidWays / totalWays;
            return probability;
        }

        //static void Main()
        //{
        //    int x = 50; // 总卡数
        //    int y = 10; // 有效卡数
        //    int z = 5;  // 抽取的次数

        //    double probability = CalculateProbability(x, y, z);
        //    Console.WriteLine($"抽到至少一张有效卡的概率是: {probability * 100:F2}%");
        //}
    }
}
