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
            radioButton1.Checked = true;
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
            int t = (int)numericUpDown4.Value;
            double probability;
            if (radioButton1.Checked)
            {
                probability = Calculator.ProbabilityDiscover(x, y, z);
            }
            else if (radioButton2.Checked)
            {
                probability = Calculator.ProbabilityRandomAny(x, y, z);
            }
            else if (radioButton3.Checked)
            {
                probability = Calculator.ProbabilityRandomAll(x, y, z);
            }
            else
            {
                probability = 0;
            }

            if (checkBox1.Checked)
            {
                probability = Calculator.ProbabilityRewind(probability, t);
            }

            label4.Text = $"概率为: {probability * 100:F2}%";
        }

        private void RadioButton_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label3.Text = "共      个选项";
            }
            else
            {
                label3.Text = "共      次";
            }
            
            NumericUpDown_ValueChanged(sender, e);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Visible = checkBox1.Checked;
            label5.Visible = checkBox1.Checked;
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

        // 计算至少抽到一张有效卡的概率
        public static double ProbabilityDiscover(long x, long y, long z)
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

        public static double ProbabilityRandomAny(long x, long y, long z)
        {
            if (y > x)
            {
                return Double.NaN;
            }

            long totalWays = Combination(x, 1);

            long invalidWays = Combination(x - y, 1);

            double invalidProbablity = (double)invalidWays / totalWays;

            double probability = 1.0 - Math.Pow(invalidProbablity, z);
            return probability;
        }

        public static double ProbabilityRandomAll(long x, long y, long z)
        {
            if (y > x)
            {
                return Double.NaN;
            }

            long totalWays = Combination(x, 1);

            long invalidWays = Combination(x - y, 1);

            double validProbablity = 1.0 - (double)invalidWays / totalWays;

            double probability = Math.Pow(validProbablity, z);
            return probability;
        }

        public static double ProbabilityRewind(double probability, int times)
        {
            return 1.0 - Math.Pow((1.0 - probability), times + 1);
        }
    }
}
