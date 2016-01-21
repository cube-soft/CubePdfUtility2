﻿/* ------------------------------------------------------------------------- */
///
/// PreviewWindow.xaml.cs
///
/// Copyright (c) 2013 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using CubePdf.Data.Extensions;

namespace CubePdfUtility
{
    /* --------------------------------------------------------------------- */
    ///
    /// PreviewWindow
    /// 
    /// <summary>
    /// PreviewWindow.xaml の相互作用ロジック
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public partial class PreviewWindow : Window
    {
        /* ----------------------------------------------------------------- */
        /// PreviewWindow (constructor)
        /* ----------------------------------------------------------------- */
        public PreviewWindow()
        {
            InitializeComponent();
            Cursor = Cursors.Hand;
            SourceInitialized += (sender, e) => {
                if (Top < 0 || Top > SystemParameters.WorkArea.Bottom - Height) Top = 0;
                if (Left < 0 || Left > SystemParameters.WorkArea.Right - Width) Left = 0;
            };
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PreviewWindow (constructor)
        ///
        /// <summary>
        /// プレビュー画面を表示するための初期化を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PreviewWindow(CubePdf.Wpf.ListViewModel viewmodel, int index)
            : this()
        {
            var pagenum = Math.Min(Math.Max(index + 1, 1), viewmodel.Pages.Count);
            var page = viewmodel.GetPage(pagenum);
            Width  = page.ViewSize().Width;
            Height = page.ViewSize().Height + 20;

            _image = viewmodel.GetImage(index, page.ViewSize());
            MainViewer.DataContext = _image;

            var filename = System.IO.Path.GetFileName(page.FilePath);
            Title = String.Format("{0}（{1}/{2} ページ）", filename, pagenum, viewmodel.Pages.Count);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PreviewWindow (constructor)
        ///
        /// <summary>
        /// プレビュー画面を表示するための初期化を行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public PreviewWindow(System.Drawing.Image image, CubePdf.Data.PageBase page)
            : this()
        {
            Width = image.Width;
            Height = image.Height + 20;

            _image = image;
            MainViewer.DataContext = _image;

            var filename = System.IO.Path.GetFileName(page.FilePath);
            Title = String.Format("{0}（{1} ページ）", filename, page.PageNumber);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnClosed
        ///
        /// <summary>
        /// 画面を閉じる際に行う処理です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_image != null) _image.Dispose();
            _image = null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnClick
        ///
        /// <summary>
        /// プレビュー画面がクリックされた時に行う処理です。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Variables
        private System.Drawing.Image _image = null;
        #endregion
    }
}
