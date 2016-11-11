using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace UISort
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer myTimer;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            Color[] colours = new Color[40];
            float[] brightness = new float[40];
            Random random = new Random();
            float[] arrayForIndex = new float[40];
            int[] backIndex = new int[40];
            
            for (int j = 0; j < 160; j++)
            {
                Thread.Sleep(1);
                list.Add(random.Next(0, 256));
            }

            for (int i = 0; i < 40; i++)
            {
                colours[i] = Color.FromArgb(list[i * 4], list[i * 4 + 1], list[i * 4 + 2], list[i * 4 + 3]);
                brightness[i] = colours[i].GetBrightness();
                backIndex[i] = i;
            }
            for (int k = 0; k < 40; k++)
            {
                arrayForIndex[k] = brightness[k];
            }
            int width, height;
            width = this.Size.Width / 40;
            height = this.Size.Height / 2;
            Color tempColor = this.BackColor;
            this.BackColor = Color.Beige;
            if (radioButton1.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                bubbleSort(brightness, 40, colours, width, arrayForIndex);
            }
            else if (radioButton2.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                selectionSort(brightness, 40, colours, width, arrayForIndex);
            }
            else if (radioButton3.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                insertionSort(brightness, 40, colours, width, arrayForIndex);
            }
            else if (radioButton4.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                quickSort(brightness, 0, 39, colours, width, arrayForIndex);
            }
            else if (radioButton5.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                mergeSort(brightness, 0, 39, colours, width, arrayForIndex);
            }
            else if (radioButton6.Checked == true)
            {
                button1.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;
                radioButton6.Visible = false;
                shellSort(brightness, 40, colours, width, arrayForIndex);
            }
            else
            {
                MessageBox.Show("Please choose one algorithm!");
            }
            
            button1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = true;
            radioButton6.Visible = true;
            this.BackColor = tempColor;
            Thread.Sleep(5000);
            DrawRect(colours, width, backIndex);
            /*Graphics g = this.CreateGraphics();
            
            for (int i = 0; i < 20; i++)
            {
                Rectangle rect = new Rectangle(width * i, 0, width, height);
                g.DrawRectangle(new Pen(colours[i], 10.0f), rect);
            }*/
        }

        private void DrawRect(Color[] colour, int w, int[] indexs)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            TimeSpan newInterval = TimeSpan.FromMilliseconds(150);
            Waiter waiter = new Waiter(newInterval);

            for (int i = 0; i < 40; i++)
            {
                //g.Clear(Color.White);
                Rectangle rect = new Rectangle(w * i, 0, w/3, (int)Math.Ceiling(colour[indexs[i]].GetBrightness() * 350));
                g.DrawRectangle(new Pen(colour[indexs[i]], 10.0f), rect);
            }
            //Thread.Sleep(150);
            waiter.Wait();
        }

        private int partition(float[] array, int l, int h, Color[] colour, int wd, float[] arrayForIndex)
        {
            int[] index;
            float x = array[h]; //pivot
            int i = (l - 1);

            for (int j = l; j <= h - 1; j++)
            {
                // If current element is smaller than or equal to pivot
                if (array[j] <= x)
                {
                    i++;
                    swap(ref array[i], ref array[j]);
                }
            }
            swap(ref array[i + 1], ref array[h]);
            index = checkForIndex(arrayForIndex, array);
            DrawRect(colour, wd, index);
            return i + 1;
        }

        private void quickSort(float[] array, int l, int h, Color[] colour, int wd, float[] arrayForIndex)
        {
            if (l < h)
            {
                
                int p = partition(array, l, h, colour, wd, arrayForIndex);
                quickSort(array, l, p - 1, colour, wd, arrayForIndex);
                quickSort(array, p + 1, h, colour, wd, arrayForIndex);
                
            }
        }

        private void insertionSort(float[] array, int n, Color[] colour, int wd, float[] arrayForIndex)
        {
            int i, j;
            float key;

            for (i = 1; i < n; i++)
            {
                key = array[i];
                j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j+1] = key;
                int[] index = checkForIndex(arrayForIndex, array);
                DrawRect(colour, wd, index);
            }
        }

        private void selectionSort(float[] array, int n, Color[] colour, int wd, float[] arrayForIndex)
        {
            int i, min_idx, j;

            for (i = 0; i < n - 1; i++)
            {
                min_idx = i;
                j = i;
                while(j < n)
                {
                    if (array[j] < array[min_idx])
                    {
                        min_idx = j;
                    }
                    j++;
                }
                swap(ref array[i], ref array[min_idx]);
                int[] index = checkForIndex(arrayForIndex, array);
                DrawRect(colour, wd, index);
            }
        }

        private void bubbleSort(float[] array, int n, Color[] colour, int wd, float[] arrayForIndex)
        {
            int i, j;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                        swap(ref array[j], ref array[j + 1]);
                }
                int[] index = checkForIndex(arrayForIndex, array);
                DrawRect(colour, wd, index);
            }
        }

        private void shellSort(float[] array, int n, Color[] colour, int wd, float[] arrayForIndex)
        {
            int i, gap, j;
            for (gap = n / 2; gap > 0; gap /= 2)
            {
                for (i = gap; i < n; i++)
                {
                    float temp = array[i];
                    for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
                int[] index = checkForIndex(arrayForIndex, array);
                DrawRect(colour, wd, index);
            }
        }

        private void merge(float[] array, int l, int m, int r, Color[] colour, int wd, float[] arrayForIndex)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            float[] L = new float[n1];
            float[] R = new float[n2];

            /* Copy data to temp arrays L[] and R[] */
            for (i = 0; i < n1; i++)
                L[i] = array[l + i];
            for (j = 0; j < n2; j++)
                R[j] = array[m + 1 + j];

            /* Merge the temp arrays back into arr[l..r]*/
            i = 0;
            j = 0;
            k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    array[k] = L[i];
                    i++;
                }
                else
                {
                    array[k] = R[j];
                    j++;
                }
                k++;
            }

            /* Copy the remaining elements of L[], if there are any */
            while (i < n1)
            {
                array[k] = L[i];
                i++;
                k++;
            }

            /* Copy the remaining elements of R[], if there are any */
            while (j < n2)
            {
                array[k] = R[j];
                j++;
                k++;
            }
            int[] index = checkForIndex(arrayForIndex, array);
            DrawRect(colour, wd, index);
        }

        private void mergeSort(float[] array, int l, int r, Color[] colour, int wd, float[] arrayForIndex)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;
                mergeSort(array, l, m, colour, wd, arrayForIndex);
                mergeSort(array, m + 1, r, colour, wd, arrayForIndex);
                merge(array, l, m, r, colour, wd, arrayForIndex);
            }
        }
        private void swap(ref float a, ref float b)
        {
            float temp = b;
            b = a;
            a = temp;
        }

        private int[] checkForIndex(float[] arrayOrigin, float[] arrayNow)
        {
            int[] index = new int[40];
            int times;
            for (int i = 0; i < 40; i++)
            {
                times = 0;
                for (int j = 0; j < 40; j++)
                {
                    if (arrayOrigin[j] == arrayNow[i])
                    {
                        times++;
                        if (times < 2)
                        {
                            index[i] = j;
                        }
                    }
                }
            }

            return index;
        }

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton6.Checked = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
        }
    }

    
}
