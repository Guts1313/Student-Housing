﻿#pragma checksum "..\..\..\..\tmp\MainP.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9ADAAB8D489762379D1E3ADA7B9EC4AA27FAE721"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Oauth_test {
    
    
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\tmp\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ProfileImage;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\tmp\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UidTextBlock;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\tmp\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\tmp\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EmailTextBlock;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\tmp\MainP.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ProviderTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StudentHousing;component/tmp/mainp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\tmp\MainP.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ProfileImage = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.UidTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.NameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.EmailTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.ProviderTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            
            #line 36 "..\..\..\..\tmp\MainP.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SignOutClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

