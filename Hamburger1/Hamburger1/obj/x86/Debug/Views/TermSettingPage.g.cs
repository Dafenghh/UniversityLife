﻿#pragma checksum "D:\github code\UniversityLife\Hamburger1\Hamburger1\Views\TermSettingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C90271AE484E64F92C3E031C592C9755"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hamburger1.Views
{
    partial class TermSettingPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.addTermDialog = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 2:
                {
                    this.termListView1 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 3:
                {
                    this.gradeListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 4:
                {
                    this.termListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 28 "..\..\..\Views\TermSettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.DelTermBtn_Clicked;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element6 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 13 "..\..\..\Views\TermSettingPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element6).Click += this.AddTermBtn_Clicked;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

