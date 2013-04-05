﻿/* ------------------------------------------------------------------------- */
///
/// RemoveWindow.xaml.cs
///
/// Copyright (c) 2013 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see < http://www.gnu.org/licenses/ >.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CubePdfUtility
{
    /* --------------------------------------------------------------------- */
    ///
    /// RemoveWindow
    /// 
    /// <summary>
    /// RemoveWindow.xaml の相互作用ロジック
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class RemoveWindow : Window
    {
        /* ----------------------------------------------------------------- */
        /// RemoveWindow (constructor)
        /* ----------------------------------------------------------------- */
        public RemoveWindow()
        {
            InitializeComponent();
            PageCount = 0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RemoveWindow (constructor)
        ///
        /// <summary>
        /// ページ数を指定して RemoveWindow オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RemoveWindow(int count)
            : this()
        {
            PageCount = count;
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// PageCount
        /// 
        /// <summary>
        /// ページ削除を行おうとしている PDF オブジェクトの総ページ数を
        /// 取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int PageCount
        {
            get { return _count; }
            set
            {
                _count = value;
                PageCountLabel.Content = String.Format("{0}ページ", _count);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PageRange
        /// 
        /// <summary>
        /// 削除対象となったページ番号の一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IList<int> PageRange
        {
            get { return _range; }
        }

        #endregion

        #region Commands

        #region Save

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = PageRangeTextBox.Text.Length > 0;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var pages = CubePdf.Data.StringConverter.ParseRange(PageRangeTextBox.Text);
                foreach (var page in pages)
                {
                    if (page <= PageCount) PageRange.Add(page);
                }
                DialogResult = true;
                Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        #region Close

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #endregion

        #endregion

        #region Variables
        private int _count = 0;
        private List<int> _range = new List<int>();
        #endregion
    }
}
