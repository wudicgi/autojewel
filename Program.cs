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
using System.Windows.Forms;

namespace WudiLabs.AutoJewel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
