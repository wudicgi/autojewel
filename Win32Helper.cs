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
using System.Runtime.InteropServices;

namespace WudiLabs.AutoJewel
{
    public static class Win32Helper
    {
        public const int MSG_WM_HOTKEY = 0x0312;

        [Flags]
        public enum HotkeyMods : uint
        {
            MOD_ALT = 0x0001,
            MOD_CONTROL = 0x0002,
            MOD_SHIFT = 0x0004,
            MOD_WIN = 0x0008
        }

        public enum HotkeyVk : uint
        {
            VK_F8 = 0x77
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out WindowRect lpRect);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static bool RegisterHotKey(IntPtr hWnd, int id, HotkeyMods mods, HotkeyVk vk)
        {
            return RegisterHotKey(hWnd, id, (uint)mods, (uint)vk);
        }

        public static Rectangle GetWindowRect(IntPtr hWnd)
        {
            WindowRect rect = new WindowRect();

            GetWindowRect(hWnd, out rect);

            return new Rectangle(rect.left, rect.top,
                rect.right - rect.left, rect.bottom - rect.top);
        }

        public static void MouseDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        public static void MouseUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static Bitmap CaptureWindow(IntPtr hWnd)
        {
            if (!Win32Helper.BringWindowToTop(hWnd))
            {
                return null;
            }

            if (!Win32Helper.SetForegroundWindow(hWnd))
            {
                return null;
            }

            return CaptureHandle(hWnd);
        }

        public static Bitmap CaptureHandle(IntPtr hWnd)
        {
            Bitmap bitmap = null;

            try
            {
                Rectangle rect = Win32Helper.GetWindowRect(hWnd);
                Graphics g_hwnd = Graphics.FromHwnd(hWnd);
                bitmap = new Bitmap(rect.Width, rect.Height, g_hwnd);
                Graphics g_bitmap = Graphics.FromImage(bitmap);
                g_bitmap.CopyFromScreen(rect.X, rect.Y, 0, 0,
                    rect.Size, CopyPixelOperation.SourceCopy);
                g_bitmap.Dispose();
                g_hwnd.Dispose();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return bitmap;
        }
    }
}
