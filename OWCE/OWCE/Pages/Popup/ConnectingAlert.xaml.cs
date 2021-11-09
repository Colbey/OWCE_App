﻿using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace OWCE.Pages.Popup
{
    public partial class ConnectingAlert : PopupPage
    {
        public string SuperTitleText { get; set; } = String.Empty;
        public string TitleText { get; set; } = String.Empty;
        public string ConnectingText { get; set; } = String.Empty;
        public string ButtonText { get; set; } = "Cancel";

        private readonly Command _actionButtonCommand;
        public Command ActionButtonCommand => _actionButtonCommand;

        public ConnectingAlert(string boardName, Command cancelCommand, string connectingText = null)
        {
            BindingContext = this;

            _actionButtonCommand = cancelCommand;

            TitleText = boardName;
            ConnectingText = connectingText ?? "Connecting...";
            InitializeComponent();
        }
    }
}
