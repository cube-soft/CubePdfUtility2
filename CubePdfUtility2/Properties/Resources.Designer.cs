﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.296
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CubePdfUtility.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CubePdfUtility.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   PDFの内容が編集されています。上書き保存しますか？ に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string IsOverwrite {
            get {
                return ResourceManager.GetString("IsOverwrite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   「{0}」はパスワードで保護されています。パスワードを入力して下さい。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PasswordPrompt {
            get {
                return ResourceManager.GetString("PasswordPrompt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   .pdf に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PdfExtension {
            get {
                return ResourceManager.GetString("PdfExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   PDF ファイル(*.pdf)|*.pdf|すべてのファイル(*.*)|*.* に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string PdfFilter {
            get {
                return ResourceManager.GetString("PdfFilter", resourceCulture);
            }
        }
    }
}
