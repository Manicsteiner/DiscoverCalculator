using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DiscoverCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int x = (int)numericUpDown1.Value;
            int y = (int)numericUpDown2.Value;
            int z = (int)numericUpDown3.Value;
            int n = (int)numericUpDown4.Value;
            int target = (int)numericUpDown5.Value;
            double probability;
            int total = x + y + z;

            if (radioButton1.Checked)
            {
                probability = Calculator.ProbabilityRandom(x, n, total, target);
            }
            else if (radioButton2.Checked)
            {
                probability = Calculator.ProbabilityRandom(y, n, total, target);
            }
            else if (radioButton3.Checked)
            {
                probability = Calculator.ProbabilityRandom(z, n, total, target);
            }
            else
            {
                probability = 0;
            }

            if (checkBox1.Checked)
            {
                probability = Calculator.ProbabilityRewind(probability, 1);
            }

            label4.Text = $"概率为: {probability * 100:F2}%";
        }

        private void RadioButton_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown_ValueChanged(sender, e);
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

        public static double ProbabilityRandom(long x, long need, long total, int target)
        {
            long totalWays = Combination(total, target);

            long validWays = 0;

            for (long count = need; count <= Math.Min(target, x); count++)
            {
                long remaining = target - count;
                if (remaining <= (total - x))
                {
                    // 计算剩余的组合
                    validWays += Combination(x, count) * Combination(total - x, remaining);
                }
            }

            return (double)validWays / totalWays;
        }

        public static double ProbabilityRewind(double probability, int times)
        {
            return 1.0 - Math.Pow((1.0 - probability), times + 1);
        }
    }
}
