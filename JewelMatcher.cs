/***********************************************************************
Author:     Wudi <wudicgi@gmail.com>
License:    GPL (GNU General Public License)
Copyright:  2011 Wudi Labs
Link:       http://blog.wudilabs.org/entry/d26e8ee5/
***********************************************************************/

/***********************************************************************
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

The GNU General Public License can be found at
http://www.gnu.org/copyleft/gpl.html
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WudiLabs.AutoJewel
{
    public class JewelMatcher
    {
        private const int BOARD_LEFT_CLASSIC = 338;
        private const int BOARD_TOP_CLASSIC = 77;

        private const int BOARD_LEFT_ZEN = 338;
        private const int BOARD_TOP_ZEN = 77;

        private const int BOARD_LEFT_LIGHTNING = 338;
        private const int BOARD_TOP_LIGHTNING = 115;

        private const int BOARD_LEFT_ICE_STORM = 338;
        private const int BOARD_TOP_ICE_STORM = 105;

        private const int BOARD_LEFT_BALANCE = 293;
        private const int BOARD_TOP_BALANCE = 103;

        /// <summary>
        /// 棋盘中每行的宝石数
        /// </summary>
        private const int ROW_COUNT = 8;
        /// <summary>
        /// 棋盘中每列的宝石数
        /// </summary>
        private const int COL_COUNT = 8;

        /// <summary>
        /// 1024x768 窗口模式下单颗宝石的宽度
        /// </summary>
        private const int JEWEL_WIDTH = 82;
        /// <summary>
        /// 1024x768 窗口模式下单颗宝石的高度
        /// </summary>
        private const int JEWEL_HEIGHT = 82;

        /// <summary>
        /// 当前捕获到的窗口位图中单颗宝石的真实宽度
        /// </summary>
        private int m_jewel_width = JEWEL_WIDTH;
        /// <summary>
        /// 当前捕获到的窗口位图中单颗宝石的真实高度
        /// </summary>
        private int m_jewel_height = JEWEL_HEIGHT;

        /// <summary>
        /// 1024x768 窗口模式下棋盘左侧的位置，作为参考
        /// </summary>
        private int m_board_left_ref = BOARD_LEFT_CLASSIC;
        /// <summary>
        /// 1024x768 窗口模式下棋盘顶部的位置，作为参考
        /// </summary>
        private int m_board_top_ref = BOARD_TOP_CLASSIC;

        /// <summary>
        /// 当前捕获到的窗口位图中棋盘左侧的真实位置
        /// </summary>
        private int m_board_left_real = BOARD_LEFT_CLASSIC;
        /// <summary>
        /// 当前捕获到的窗口位图中棋盘顶部的真实位置
        /// </summary>
        private int m_board_top_real = BOARD_TOP_CLASSIC;

        /// <summary>
        /// 游戏模式
        /// </summary>
        public enum GameMode
        {
            Classic,
            Zen,
            Lightning,
            IceStorm,
            Balance
        }

        // 色相值
        private const int HUE_RED = 350;
        private const int HUE_PURPLE = 300;
        private const int HUE_BLUE = 210;
        private const int HUE_GREEN = 130;
        private const int HUE_YELLOW = 55;
        private const int HUE_ORANGE = 31;
        private const int HUE_WHITE = 10;

        // 饱和度值
        private const int SAT_YELLOW = 950;
        private const int SAT_ORANGE = 875;

        /// <summary>
        /// 宝石颜色
        /// </summary>
        public enum JewelColor
        {
            Red,
            Purple,
            Blue,
            Green,
            Yellow,
            Orange,
            White
        }

        /// <summary>
        /// 设置或获取当前的游戏模式
        /// </summary>
        public GameMode Mode
        {
            get
            {
                return m_mode;
            }
            set
            {
                m_mode = value;
                switch (m_mode)
                {
                    case GameMode.Classic:
                        m_board_left_ref = BOARD_LEFT_CLASSIC;
                        m_board_top_ref = BOARD_TOP_CLASSIC;
                        break;
                    case GameMode.Zen:
                        m_board_left_ref = BOARD_LEFT_ZEN;
                        m_board_top_ref = BOARD_TOP_ZEN;
                        break;
                    case GameMode.Lightning:
                        m_board_left_ref = BOARD_LEFT_LIGHTNING;
                        m_board_top_ref = BOARD_TOP_LIGHTNING;
                        break;
                    case GameMode.IceStorm:
                        m_board_left_ref = BOARD_LEFT_ICE_STORM;
                        m_board_top_ref = BOARD_TOP_ICE_STORM;
                        break;
                    case GameMode.Balance:
                        m_board_left_ref = BOARD_LEFT_BALANCE;
                        m_board_top_ref = BOARD_TOP_BALANCE;
                        break;
                }
            }
        }

        private GameMode m_mode = GameMode.Classic;

        /// <summary>
        /// 匹配模式
        /// </summary>
        public struct MatchPattern
        {
            /// <summary>
            /// 方块的边长
            /// </summary>
            public int SideLength;
            /// <summary>
            /// 用于匹配的二进制值
            /// </summary>
            public int MatchBinary;
            /// <summary>
            /// 移动起点
            /// </summary>
            public int MoveSrc;
            /// <summary>
            /// 移动终点
            /// </summary>
            public int MoveDst;
            /// <summary>
            /// 优先级
            /// </summary>
            public int Priority;

            public MatchPattern(int side_len, int binary, int move_src, int move_dst,
                int priority)
            {
                SideLength = side_len;
                MatchBinary = binary;
                MoveSrc = move_src;
                MoveDst = move_dst;
                Priority = priority;
            }
        }

        /// <summary>
        /// 匹配方法
        /// </summary>
        public class MatchMethod
        {
            /// <summary>
            /// 匹配方法的模型
            /// </summary>
            public MatchPattern MatchPattern;
            /// <summary>
            /// 
            /// </summary>
            public int LeftTopRowIndex;
            /// <summary>
            /// 
            /// </summary>
            public int LeftTopColIndex;

            public MatchMethod(MatchPattern pattern, int row_index, int col_index)
            {
                MatchPattern = pattern;
                LeftTopRowIndex = row_index;
                LeftTopColIndex = col_index;
            }
        }

        public class Solution
        {
            public Point PointSrc;
            public Point PointDst;
            public MatchMethod MatchMethod;

            public Solution(Point src, Point dst, MatchMethod method)
            {
                PointSrc = src;
                PointDst = dst;
                MatchMethod = method;
            }
        }

        private List<MatchPattern> m_match_patterns_3x3 = new List<MatchPattern>();
        private List<MatchPattern> m_match_patterns_4x4 = new List<MatchPattern>();

        private MatchPattern m_solve_none = new MatchPattern(0, 0, 0, 0, 512);

        private IComparer<MatchMethod> m_solve_method_cmp = new MatchMethodComparer();

        private JewelColor[] m_all_colors = new JewelColor[] {
            JewelColor.Red,
            JewelColor.Purple,
            JewelColor.Blue,
            JewelColor.Green,
            JewelColor.Yellow,
            JewelColor.Orange,
            JewelColor.White
        };

        /// <summary>
        /// 加载匹配模式数据
        /// </summary>
        /// <param name="filename">文件名</param>
        public void LoadPatterns(string filename)
        {
            string temp = System.IO.File.ReadAllText(filename, Encoding.ASCII);
            string[] patterns = temp.Trim().Replace("\r\n", "\n").Split(
                new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries
            );

            Dictionary<string, bool> patterns_met = new Dictionary<string, bool>();

            foreach (string pattern in patterns)
            {
                string pattern_sl = pattern.Replace("\n", "");
                string pattern_cur = "";
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        pattern_cur = pattern_sl;
                    }
                    else if (i == 1)
                    {
                        pattern_cur = FlipPatternH(pattern_sl);
                    }
                    else if (i == 2)
                    {
                        pattern_cur = FlipPatternV(pattern_sl);
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        if (patterns_met.ContainsKey(pattern_cur))
                        {
                            continue;
                        }
                        patterns_met[pattern_cur] = true;
                        pattern_cur = RotatePattern(pattern_cur);
                    }
                }
            }

            m_match_patterns_3x3.Clear();
            m_match_patterns_4x4.Clear();

            foreach (string pattern in patterns_met.Keys)
            {
                string binary_str = pattern.Replace('-', '0').Replace('B', '0').Replace('X', '1').Replace('A', '1');

                int binary = Convert.ToInt32(binary_str, 2);
                int priority = binary_str.Replace("0", "").Length;

                int move_src = pattern.IndexOf('A');
                int move_dst = pattern.IndexOf('B');

                if (pattern.Length == 9)
                {
                    m_match_patterns_3x3.Add(new MatchPattern(3, binary, move_src, move_dst, priority));
                }
                else if (pattern.Length == 16)
                {
                    m_match_patterns_4x4.Add(new MatchPattern(4, binary, move_src, move_dst, priority));
                }
                else
                {
                    throw new Exception("Invalid line string: " + pattern);
                }
            }
        }

        /// <summary>
        /// 旋转一个匹配
        /// </summary>
        /// <param name="pattern_sl">单行的匹配描述</param>
        /// <returns>旋转完的匹配</returns>
        private string RotatePattern(string pattern_sl)
        {
            switch (pattern_sl.Length)
            {
                case 9:
                    return new string(new char[] {
                        pattern_sl[2], pattern_sl[5], pattern_sl[8],
                        pattern_sl[1], pattern_sl[4], pattern_sl[7],
                        pattern_sl[0], pattern_sl[3], pattern_sl[6]
                    });
                case 16:
                    return new string(new char[] {
                        pattern_sl[3], pattern_sl[7], pattern_sl[11], pattern_sl[15],
                        pattern_sl[2], pattern_sl[6], pattern_sl[10], pattern_sl[14],
                        pattern_sl[1], pattern_sl[5], pattern_sl[9], pattern_sl[13],
                        pattern_sl[0], pattern_sl[4], pattern_sl[8], pattern_sl[12]
                    });
                default:
                    throw new Exception("Invalid line string: " + pattern_sl);
            }
        }

        /// <summary>
        /// 水平翻转一个匹配
        /// </summary>
        /// <param name="pattern_sl">单行的匹配描述</param>
        /// <returns>翻转完的匹配</returns>
        private string FlipPatternH(string pattern_sl)
        {
            switch (pattern_sl.Length)
            {
                case 9:
                    return new string(new char[] {
                        pattern_sl[6], pattern_sl[7], pattern_sl[8],
                        pattern_sl[3], pattern_sl[4], pattern_sl[5],
                        pattern_sl[0], pattern_sl[1], pattern_sl[2]
                    });
                case 16:
                    return new string(new char[] {
                        pattern_sl[12], pattern_sl[13], pattern_sl[14], pattern_sl[15],
                        pattern_sl[8], pattern_sl[9], pattern_sl[10], pattern_sl[11],
                        pattern_sl[4], pattern_sl[5], pattern_sl[6], pattern_sl[7],
                        pattern_sl[0], pattern_sl[1], pattern_sl[2], pattern_sl[3]
                    });
                default:
                    throw new Exception("Invalid line string: " + pattern_sl);
            }
        }

        /// <summary>
        /// 水平翻转一个匹配
        /// </summary>
        /// <param name="pattern_sl">单行的匹配描述</param>
        /// <returns>翻转完的匹配</returns>
        private string FlipPatternV(string pattern_sl)
        {
            switch (pattern_sl.Length)
            {
                case 9:
                    return new string(new char[] {
                        pattern_sl[2], pattern_sl[1], pattern_sl[0],
                        pattern_sl[5], pattern_sl[4], pattern_sl[3],
                        pattern_sl[8], pattern_sl[7], pattern_sl[6]
                    });
                case 16:
                    return new string(new char[] {
                        pattern_sl[3], pattern_sl[2], pattern_sl[1], pattern_sl[0],
                        pattern_sl[7], pattern_sl[6], pattern_sl[5], pattern_sl[4],
                        pattern_sl[11], pattern_sl[10], pattern_sl[9], pattern_sl[8],
                        pattern_sl[15], pattern_sl[14], pattern_sl[13], pattern_sl[12]
                    });
                default:
                    throw new Exception("Invalid line string: " + pattern_sl);
            }
        }

        /// <summary>
        /// 处理捕捉到的窗口区域位图，获取当前最佳匹配方法
        /// </summary>
        /// <param name="bitmap">捕捉到的位图</param>
        public Solution GetSolution(Bitmap bitmap)
        {
            float factor = (float)(bitmap.Width - 8) / (float)1024;

            m_jewel_width = (int)Math.Round(factor * JEWEL_WIDTH);
            m_jewel_height = m_jewel_width;

            m_board_left_real = (int)Math.Round(factor * (m_board_left_ref - 4)) + 4;
            m_board_top_real = bitmap.Height - ((int)Math.Round(factor * (802 - m_board_top_ref - 4)) + 4);

            JewelColor[,] jewels = new JewelColor[ROW_COUNT, COL_COUNT];

            int a_min = (int)Math.Round(m_jewel_width * 0.366);
            int a_max = (int)Math.Round(m_jewel_width * 0.610);

            for (int i = 0; i < ROW_COUNT; i++)
            {
                int y = m_board_top_real + (m_jewel_height * i);
                for (int j = 0; j < COL_COUNT; j++)
                {
                    int x = m_board_left_real + (m_jewel_width * j);

                    int color_r = 0;
                    int color_g = 0;
                    int color_b = 0;
                    int color_count = 0;
                    for (int a = a_min; a < a_max; a++)
                    {
                        for (int b = a_min; b < a_max; b++)
                        {
                            Color pixel = bitmap.GetPixel(x + a, y + b);
                            color_r += pixel.R;
                            color_g += pixel.G;
                            color_b += pixel.B;
                            color_count++;
                        }
                    }
                    color_r = color_r / color_count;
                    color_g = color_g / color_count;
                    color_b = color_b / color_count;

                    if (Math.Abs(((float)color_g / (float)color_r) - 1) < 0.15 &&
                        Math.Abs(((float)color_b / (float)color_r) - 1) < 0.15)
                    {
                        color_g = color_r;
                        color_b = color_r;
                    }

                    Color color_avg = Color.FromArgb(color_r, color_g, color_b);

                    int hue = (int)color_avg.GetHue();
                    int sat = (int)(color_avg.GetSaturation() * 1000);

                    jewels[i, j] = GetColorFromHueSat(hue, sat);
                }
            }

            return ProcessAllJewels(jewels);
        }

        /// <summary>
        /// 获取宝石颜色
        /// </summary>
        /// <param name="hue">色相值</param>
        /// <param name="sat">饱和度值</param>
        /// <returns>宝石颜色</returns>
        private JewelColor GetColorFromHueSat(int hue, int sat)
        {
            JewelColor jewel_color = JewelColor.White;

            if (sat < 180)
            {
                jewel_color = JewelColor.White;
            }
            else if (hue > ((HUE_RED + HUE_PURPLE) / 2))
            {
                jewel_color = JewelColor.Red;
            }
            else if (hue > ((HUE_PURPLE + HUE_BLUE) / 2))
            {
                jewel_color = JewelColor.Purple;
            }
            else if (hue > ((HUE_BLUE + HUE_GREEN) / 2))
            {
                jewel_color = JewelColor.Blue;
            }
            else if (hue > ((HUE_GREEN + HUE_YELLOW) / 2))
            {
                jewel_color = JewelColor.Green;
            }
            else if (hue > ((HUE_YELLOW + HUE_ORANGE) / 2))
            {
                jewel_color = JewelColor.Yellow;
            }
            else if (hue > ((HUE_ORANGE + HUE_WHITE) / 2))
            {
                jewel_color = JewelColor.Orange;
            }
            else
            {
                jewel_color = JewelColor.White;
            }

            return jewel_color;
        }

        /// <summary>
        /// 处理各种颜色的宝石
        /// </summary>
        /// <param name="jewels">以宝石颜色为项目的二维数组</param>
        private Solution ProcessAllJewels(JewelColor[,] jewels)
        {
            List<MatchMethod> methods = new List<MatchMethod>();

            for (int i = 0; i < m_all_colors.Length; i++)
            {
                ProcessBoolJewels(GetSpecColorJewels(jewels, m_all_colors[i]), ref methods);
            }

            if (methods.Count == 0)
            {
                return null;
            }

            Random random = new Random();
            foreach (MatchMethod method in methods)
            {
                method.MatchPattern.Priority += (int)Math.Round(random.NextDouble() * 1);

                if (m_mode == GameMode.Classic)
                {
                    method.MatchPattern.Priority += 2 * (ROW_COUNT - method.LeftTopRowIndex);
                }
            }

            methods.Sort(m_solve_method_cmp);

            MatchMethod method_0 = methods[0];

            int side_len = method_0.MatchPattern.SideLength;

            Point point_src = new Point(
                (m_board_left_real + (m_jewel_width / 2)
                + ((method_0.LeftTopColIndex + (method_0.MatchPattern.MoveSrc % side_len)) * m_jewel_width)),
                (m_board_top_real + (m_jewel_width / 2)
                + ((method_0.LeftTopRowIndex + (method_0.MatchPattern.MoveSrc / side_len)) * m_jewel_height))
            );

            Point point_dst = new Point(
                (m_board_left_real + (m_jewel_width / 2)
                + ((method_0.LeftTopColIndex + (method_0.MatchPattern.MoveDst % side_len)) * m_jewel_width)),
                (m_board_top_real + (m_jewel_width / 2)
                + ((method_0.LeftTopRowIndex + (method_0.MatchPattern.MoveDst / side_len)) * m_jewel_height))
            );

            return new Solution(point_src, point_dst, method_0);
        }

        /// <summary>
        /// 处理单一颜色的宝石，将得到的匹配方法加入到 methods 中
        /// </summary>
        /// <param name="jewels">宝石分布情况数据</param>
        /// <param name="methods">用于存放匹配方法的列表</param>
        private void ProcessBoolJewels(int[,] jewels, ref List<JewelMatcher.MatchMethod> methods)
        {
            int binary = 0;
            //bool found_4x4 = false;

            for (int i = 0; i < ROW_COUNT; i++)
            {
                for (int j = 0; j < COL_COUNT; j++)
                {
                    if (!(i > (ROW_COUNT - 4) || j > (COL_COUNT - 4)))
                    {
                        binary = (jewels[i, j] << 15) | (jewels[i, j + 1] << 14) | (jewels[i, j + 2] << 13) | (jewels[i, j + 3] << 12)
                            | (jewels[i + 1, j] << 11) | (jewels[i + 1, j + 1] << 10) | (jewels[i + 1, j + 2] << 9) | (jewels[i + 1, j + 3] << 8)
                            | (jewels[i + 2, j] << 7) | (jewels[i + 2, j + 1] << 6) | (jewels[i + 2, j + 2] << 5) | (jewels[i + 2, j + 3] << 4)
                            | (jewels[i + 3, j] << 3) | (jewels[i + 3, j + 1] << 2) | (jewels[i + 3, j + 2] << 1) | (jewels[i + 3, j + 3] << 0);
                        foreach (MatchPattern model in m_match_patterns_4x4)
                        {
                            if ((binary & model.MatchBinary) == model.MatchBinary)
                            {
                                methods.Add(new MatchMethod(model, i, j));
                                //found_4x4 = true;
                            }
                        }
                    }

                    //if (found_4x4)
                    //{
                    //    continue;
                    //}

                    if (!(i > (ROW_COUNT - 3) || j > (COL_COUNT - 3)))
                    {
                        binary = (jewels[i, j] << 8) | (jewels[i, j + 1] << 7) | (jewels[i, j + 2] << 6)
                            | (jewels[i + 1, j] << 5) | (jewels[i + 1, j + 1] << 4) | (jewels[i + 1, j + 2] << 3)
                            | (jewels[i + 2, j] << 2) | (jewels[i + 2, j + 1] << 1) | (jewels[i + 2, j + 2] << 0);
                        foreach (MatchPattern model in m_match_patterns_3x3)
                        {
                            if ((binary & model.MatchBinary) == model.MatchBinary)
                            {
                                methods.Add(new MatchMethod(model, i, j));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定颜色宝石的分布情况数据
        /// </summary>
        /// <param name="jewels">以宝石颜色为项目的二维数组</param>
        /// <param name="color">指定的宝石颜色</param>
        /// <returns>指定颜色宝石分布情况的二维数组</returns>
        private int[,] GetSpecColorJewels(JewelColor[,] jewels, JewelColor color)
        {
            int[,] result = new int[ROW_COUNT, ROW_COUNT];

            for (int i = 0; i < ROW_COUNT; i++)
            {
                for (int j = 0; j < COL_COUNT; j++)
                {
                    result[i, j] = (jewels[i, j] == color) ? 1 : 0;
                }
            }

            return result;
        }
    }

    public class MatchMethodComparer : IComparer<JewelMatcher.MatchMethod>
    {
        public int Compare(JewelMatcher.MatchMethod x, JewelMatcher.MatchMethod y)
        {
            if (x.MatchPattern.Priority > y.MatchPattern.Priority)
            {
                return -1;
            }
            else if (x.MatchPattern.Priority < y.MatchPattern.Priority)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
