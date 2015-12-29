﻿/* ------------------------------------------------------------------------- */
///
/// EncryptionWindow.xaml.cs
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
using System.Windows.Controls;
using System.Windows.Input;

namespace CubePdfUtility
{
    /* --------------------------------------------------------------------- */
    ///
    /// EncryptionMethodToIndex
    /// 
    /// <summary>
    /// EncryptionMethod と ComboBox のインデックスの相互変換を行うための
    /// コンバータクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal class EncryptionMethodToIndex : CubePdf.Wpf.EnumToIntConverter<CubePdf.Data.EncryptionMethod>{ }

    /* --------------------------------------------------------------------- */
    ///
    /// EncryptionWindow
    /// 
    /// <summary>
    /// EncryptionWindow.xaml の相互作用ロジック
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class EncryptionWindow : Window
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        /// EncryptionWindow (constructor)
        /* ----------------------------------------------------------------- */
        public EncryptionWindow()
        {
            InitializeComponent();
        }

        /* ----------------------------------------------------------------- */
        /// EncryptionWindow (constructor)
        /* ----------------------------------------------------------------- */
        public EncryptionWindow(CubePdf.Wpf.ListViewModel viewmodel)
            : this()
        {
            RootGroupBox.IsEnabled = (viewmodel.EncryptionStatus != CubePdf.Data.EncryptionStatus.RestrictedAccess);
            _crypt = new CubePdf.Data.Encryption(viewmodel.Encryption);
            if (_crypt.Method == CubePdf.Data.EncryptionMethod.Unknown) _crypt.Method = CubePdf.Data.EncryptionMethod.Standard128;
            DataContext = _crypt;
            if (_crypt.OwnerPassword.Length > 0)
            {
                OwnerPasswordBox.Password = _crypt.OwnerPassword;
                ConfirmOwnerPasswordBox.Password = _crypt.OwnerPassword;
            }

            if (_crypt.UserPassword.Length > 0)
            {
                UserPasswordCheckBox.IsChecked = true;
                UserPasswordBox.Password = _crypt.UserPassword;
                ConfirmUserPasswordBox.Password = _crypt.UserPassword;
            }
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Encryption
        ///
        /// <summary>
        /// 暗号化に関連する情報を保持するオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CubePdf.Data.Encryption Encryption
        {
            get { return _crypt; }
        }

        #endregion

        #region Commands

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// OK ボタンをに該当するコマンドです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        #region Save

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Encryption == null) ||
                           (!Encryption.IsEnabled || IsValidPassword(OwnerPasswordBox, ConfirmOwnerPasswordBox)) &&
                           (!Encryption.IsUserPasswordEnabled || UserPasswordCheckBox.IsChecked == false || IsValidPassword(UserPasswordBox, ConfirmUserPasswordBox));
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (IsUserPasswordRequired())
            {
                ShowErrorMessage(Properties.Resources.NeedUserPassword);
                return;
            }

            DialogResult = true;
            Encryption.OwnerPassword = Encryption.IsEnabled ? OwnerPasswordBox.Password : "";
            Encryption.UserPassword = (Encryption.IsEnabled && UserPasswordCheckBox.IsChecked == true) ? UserPasswordBox.Password : "";
            Close();
        }

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// Close
        /// 
        /// <summary>
        /// キャンセルボタンをに該当するコマンドです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
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

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidPassword
        ///
        /// <summary>
        /// 確認用のテキストボックスに入力されたパスワードが元のパスワードと
        /// 一致するかどうかを判断します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsValidPassword(PasswordBox password, PasswordBox confirm)
        {
            if (String.IsNullOrEmpty(password.Password)) return false;
            return password.Password == confirm.Password;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsValidUserPassword
        ///
        /// <summary>
        /// ユーザパスワードの設定が正しいかどうかを判断します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsUserPasswordRequired()
        {
            return Encryption.IsEnabled &&
                  !Encryption.Permission.FullAccess &&
                   Encryption.IsUserPasswordEnabled &&
                  (UserPasswordCheckBox.IsChecked != true ||
                   OwnerPasswordBox.Password == UserPasswordBox.Password);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ShowErrorMessage
        ///
        /// <summary>
        /// エラーメッセージを表示します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ShowErrorMessage(string message, Exception inner = null)
        {
            var s = (inner != null) ? string.Format("{0}({1})", message, inner.Message) : message;
            MessageBox.Show(s, Properties.Resources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            if (inner != null) Trace.TraceError(inner.ToString());
        }

        #endregion

        #region Variables
        CubePdf.Data.Encryption _crypt = null;
        #endregion
    }
}
