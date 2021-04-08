using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    
    class Stek
    {
        int n;
        public Stek(int n)
        {
            this.n = n;
        }
        Stack<int> stack = new Stack<int>();
        int[][] temp = new int[15][];
        
        int way(int i, int j, int k, int[,] m)
        {
            switch (k)
            {
                case 0:
                    switch (stack.Peek())
                    {
                        case 1:
                            if (j < n - 1&&i>-1)
                            {
                                if (m[i, j + 1] == 1)
                                    return 1;
                                else if (way(i, j, 1, m) != 0)
                                    return way(i, j, 1, m);
                                else if (way(i, j, 3, m) != 0)
                                    return way(i, j, 3, m);
                                else  return 0; 
                            }
                            else return 5;
                        case 2:
                            if (j < n - 1)
                            {
                                if (m[i, j + 1] == 1)
                                    return 1;
                                else if (way(i, j, 1, m) != 0)
                                    return way(i, j, 1, m);
                                else if (way(i, j, 2, m) != 0)
                                    return way(i, j, 2, m);
                                else return 0; 
                            }
                            else return 5;
                        case 3:
                            if (i < n - 1)
                            {
                                if (m[i + 1, j] == 1)
                                    return 2;
                                else if (way(i, j, 2, m) != 0)
                                    return way(i, j, 2, m);
                                else if (way(i, j, 3, m) != 0)
                                    return way(i, j, 3, m);
                                else  return 0; 
                            }
                            else return 0;
                        case 4:
                            if (j < n - 1)
                            {
                                if (m[i, j + 1] == 1)
                                    return 1;
                                else if (way(i, j, 2, m) != 0)
                                    return way(i, j, 2, m);
                                else if (way(i, j, 3, m) != 0)
                                    return way(i, j, 3, m);
                                else return 0; 
                            }
                            else return 5;
                    }break;
                case 1:
                    if (i < n - 1&&j>-1)
                    {
                         if (m[i + 1, j] == 1)
                            return 2;
                        else return 0;
                    }
                    else return 0;
                case 2:
                    if (j != 0&&j>-1)
                    {
                        if (m[i, j - 1] == 1)
                            return 3;
                        else return 0;
                    }
                    else return 0;
                case 3:
                    if (i != 0&&j>-1)
                    {
                        if (m[i - 1, j] == 1)
                            return 4;
                        else return 0;
                    }
                    else return 0;
            }
            return 0;
        }
        public int[][,] ways(int[,] mas)
        {
            int[,] mas1 = new int[n,n];
            for (int t = 0; t < n; t++)
            {
                for (int y = 0; y < n; y++)
                {
                    mas1[t, y] = mas[t, y];
                }
            }
            int[][,] m = new int[n][,], m1 = new int[n*n][,];
            for (int i = 0; i < n; i++)
            {
                m[i] = new int[n, n];
            }
            for (int i = 0; i < n*n; i++)
            {
                m1[i] = new int[n, n];
            }
            for (int i = 0; i < n; i++)
            {
                for(int j=0;j<n;j++)
                    for (int k = 0; k < n; k++)
                    {
                        m[i][j, k] = 0;
                    }
            }
            int e = 0, r = 0;
            for (int i = 0; i < n; i++)
            {
                for (int t = 0; t < n; t++)
                {
                    for (int y = 0; y < n; y++)
                    {
                        mas1[t, y] = mas[t, y];
                    }
                }
                int  a = 0;
            M:
                int j = 0;
                if (mas[i, j] == 1)
                {
                    int i1 = i, j1 = j;
                    stack.Push(1);
                    int k = 0;
                    int s1 = 0,s2=0;
                    while (way(i1, j1, k, mas1) != 5)
                    {
                        switch (way(i1, j1, k, mas1))
                        {
                            case 1: s1 = 1; s2 = 1; break;
                            case 2: 
                                if (stack.Peek() == 1) s1 += 2;
                                if (stack.Peek() == 3 && s2 == 6)
                                {
                                    stack.Pop();
                                    mas1[i1, j1] = 0;
                                    j1++;
                                    k = 0;
                                    s2 = 0;
                                }  break; 
                            case 3: 
                                if (stack.Peek() == 4 && s2 == 3) s2 += 3;
                                if (stack.Peek() == 2 && s1 == 3) s1 += 3; break;
                            case 4: 
                                if (stack.Peek() == 1) s2 += 2;
                                if (stack.Peek() == 3 && s1 == 6)
                                {
                                    stack.Pop();
                                    mas1[i1, j1] = 0;
                                    j1++;
                                    k = 0;
                                    s1 = 0;
                                } break;
                        }
                        while(way(i1, j1, k, mas1) == 0)
                        { 
                         mas1[i1, j1] = 0;
                            switch (stack.Peek())
                            {
                                case 1: j1--; break;
                                case 2: i1--; break;
                                case 3: j1++; break;
                                case 4: i1++; break;
                            }
                            stack.Pop(); 
                            if (stack.Count == 0)
                                break;
                            k = 0;
                        }
                        if (stack.Count == 0)
                            break;
                        stack.Push(way(i1, j1, k, mas1));
                        switch (stack.Peek())
                        {
                            case 1: j1++; break;
                            case 2: i1++; break;
                            case 3: j1--; break;
                            case 4: i1--; break;
                        }
                        k = 0;
                    }
                    if (stack.Count != 0 && way(i1, j1, k, mas1) == 5)
                    {
                        temp[a] = stack.ToArray(); 
                        int q = i1, w = j1;
                        while (stack.Count != 0)
                        {
                            m1[e][q, w] = 1; 
                            switch (stack.Peek())
                            {
                                case 1: w--; break;
                                case 2: q--; break;
                                case 3: w++; break;
                                case 4: q++; break;
                            }
                            stack.Pop();
                        }
                        e++;
                        a++;
                        mas1[i1, j1] = 0;
                        goto M;
                    }
                    else
                    {
                        int Min = int.MaxValue;
                        for (int z = 0; z < a; z++)
                        {
                            if (Min > temp[z].Length)
                            {
                                stack.Clear();
                                Min = temp[z].Length;
                                for (int s = temp[z].Length-1; s >=0; s--)
                                {
                                    stack.Push(temp[z][s]);
                                }
                            }
                        }

                        bool b = true;
                        for (int z = 0; z < a; z++)
                        {
                            if (temp[z]!=null&&Min == temp[z].Length && b==true)
                            {
                                int q=0, w=n-1;
                                for (int x = 0; x < n; x++)
                                {
                                    if(m1[z][x,w]==1)
                                    q = x;
                                }
                                while (stack.Count != 0)
                                {
                                    m[r][q, w] = 1;
                                    switch (stack.Peek())
                                    {
                                        case 1: w--; break;
                                        case 2: q--; break;
                                        case 3: w++; break;
                                        case 4: q++; break;
                                    }
                                    stack.Pop();
                                }
                                b = false;
                                for (int u = 0; u < n * n;u++ )
                                    for (int t = 0; t < n; t++)
                                    {
                                        for (int y = 0; y < n; y++)
                                        {
                                            m1[u][t, y] = 0;
                                        }
                                    }
                                for (int u = 0; u < 15; u++)
                                    temp[u] = null;
                                    e = 0;
                                r++;
                            }
                        }
                    }
                }

            }
                return m;
        }
    }
    

}
