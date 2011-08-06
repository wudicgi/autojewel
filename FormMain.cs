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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WudiLabs.AutoJewel
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 热键的标识符, 0x0000 - 0xBFFF
        /// </summary>
        private const int HOTKEY_ID = 0x1000;

        private const string PROCESS_NAME_STANDARD = "Bejeweled3";
        private const string PROCESS_NAME_TRIAL = "popcapgame1";

        private const int TIME_INTERVAL_CLASSIC = 1500; // 1500ms
        private const int TIME_MOVE_CLASSIC = 100; // 100ms

        private const int TIME_INTERVAL_ZEN = 500; // 500ms
        private const int TIME_MOVE_ZEN = 50; // 50ms

        private const int TIME_INTERVAL_LIGHTNING = 300; // 300ms
        private const int TIME_MOVE_LIGHTNING = 50; // 50ms

        private Timer m_timer = new Timer();
        private int m_time_move = TIME_MOVE_CLASSIC;

        private JewelMatcher m_matcher = new JewelMatcher();
        private string m_process_name = PROCESS_NAME_STANDARD;
        private IntPtr m_process_whnd = IntPtr.Zero;

        private bool m_running = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_matcher.LoadPatterns("patterns.txt");

            m_timer.Stop();
            m_timer.Interval = TIME_INTERVAL_LIGHTNING;
            m_timer.Tick += new EventHandler(m_timer_Tick);

            bool succeeded = Win32Helper.RegisterHotKey(this.Handle, HOTKEY_ID,
                Win32Helper.HotkeyMods.MOD_CONTROL, Win32Helper.HotkeyVk.VK_F8);
            if (!succeeded)
            {
                ShowErrorMessage("Failed to register the global hotkey.");
            }
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            if (!m_running)
            {
                return;
            }

            try
            {
                Rectangle window_rect = Win32Helper.GetWindowRect(m_process_whnd);

                Bitmap bitmap = Win32Helper.CaptureWindow(m_process_whnd);
                if (bitmap == null)
                {
                    StopRunning();
                    ShowErrorMessage("Failed to capture the window.");
                    return;
                }

                JewelMatcher.Solution solution = m_matcher.GetSolution(bitmap);
                bitmap.Dispose();

                Point point_src = solution.PointSrc;
                point_src.X += window_rect.Left;
                point_src.Y += window_rect.Top;

                Point point_dst = solution.PointDst;
                point_dst.X += window_rect.Left;
                point_dst.Y += window_rect.Top;

                if (point_src.X <= window_rect.Left || point_src.X >= window_rect.Right ||
                    point_src.Y <= window_rect.Top || point_src.Y >= window_rect.Bottom ||
                    point_dst.X <= window_rect.Left || point_dst.X >= window_rect.Right ||
                    point_dst.Y <= window_rect.Top || point_dst.Y >= window_rect.Bottom)
                {
                    StopRunning();
                    ShowErrorMessage("Internal Error.");
                    return;
                }

                Cursor.Position = point_src;
                Win32Helper.MouseDown();
                System.Threading.Thread.Sleep(m_time_move);
                Cursor.Position = point_dst;
                Win32Helper.MouseUp();
            }
            catch (Exception)
            {
                /*
                StopRunning();
                ShowErrorMessage("Exception: " + exp.Message);
                */
                // do nothing
            }
        }

        private bool SetParameters()
        {
            if (radioModeClassic.Checked)
            {
                m_matcher.Mode = JewelMatcher.GameMode.Classic;
                m_timer.Interval = TIME_INTERVAL_CLASSIC;
                m_time_move = TIME_MOVE_CLASSIC;
            }
            else if (radioModeZen.Checked)
            {
                m_matcher.Mode = JewelMatcher.GameMode.Zen;
                m_timer.Interval = TIME_INTERVAL_ZEN;
                m_time_move = TIME_MOVE_ZEN;
            }
            else if (radioModeLightning.Checked)
            {
                m_matcher.Mode = JewelMatcher.GameMode.Lightning;
                m_timer.Interval = TIME_INTERVAL_LIGHTNING;
                m_time_move = TIME_MOVE_LIGHTNING;
            }
            else if (radioModeIceStorm.Checked)
            {
                m_matcher.Mode = JewelMatcher.GameMode.IceStorm;
                m_timer.Interval = TIME_INTERVAL_LIGHTNING;
                m_time_move = TIME_MOVE_LIGHTNING;
            }
            else if (radioModeBalance.Checked)
            {
                m_matcher.Mode = JewelMatcher.GameMode.Balance;
                m_timer.Interval = TIME_INTERVAL_LIGHTNING;
                m_time_move = TIME_MOVE_LIGHTNING;
            }
            else
            {
                return false;
            }

            if (radioProcessStandard.Checked)
            {
                m_process_name = PROCESS_NAME_STANDARD;
            }
            else if (radioProcessTrial.Checked)
            {
                m_process_name = PROCESS_NAME_TRIAL;
            }
            else
            {
                return false;
            }

            return true;
        }

        private void StartRunning()
        {
            if (!SetParameters())
            {
                ShowErrorMessage("Failed to set parameters.");
                return;
            }

            try
            {
                Process[] processes = Process.GetProcessesByName(m_process_name);
                m_process_whnd = processes[0].MainWindowHandle;
            }
            catch (Exception)
            {
                ShowErrorMessage("Failed to get the window handle of Bejeweled.");
                return;
            }

            Win32Helper.BringWindowToTop(this.Handle);
            //Win32Helper.SetForegroundWindow(this.Handle);

            m_running = true;
            m_timer.Start();
            statusBar.Text = "Running under " + m_matcher.Mode.ToString() + " mode";
        }

        private void StopRunning()
        {
            m_running = false;
            m_timer.Stop();
            statusBar.Text = "Stopped";
        }

        private void ShowErrorMessage(string message)
        {
            Win32Helper.BringWindowToTop(this.Handle);
            Win32Helper.SetForegroundWindow(this.Handle);
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == Win32Helper.MSG_WM_HOTKEY) && ((int)m.WParam == HOTKEY_ID))
            {
                if (m_running)
                {
                    StopRunning();
                }
                else
                {
                    StartRunning();
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
