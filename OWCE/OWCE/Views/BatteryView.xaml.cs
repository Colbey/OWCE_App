using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OWCE.Converters;
using OWCE.Extensions;
using OWCE.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace OWCE.Views
{
    public partial class BatteryView : ContentView
    {
        //	public delegate void BindingPropertyChangedDelegate<in TPropertyType> (BindableObject bindable, TPropertyType oldValue, TPropertyType newValue);


        public static readonly BindableProperty BatteryPercentProperty = BindableProperty.Create(
          "BatteryPercent",
          typeof(int),
          typeof(BatteryView),
          0,
          BindingMode.OneWay);
        /*
        private int _batteryPercent;
        public int BatteryPercent
        {

            get {
                return _batteryPercent;
            }
            set
            {
                _batteryPercent = value;
                OnPropertyChanged();
            }
        }
        */
        public int BatteryPercent
        {
            get
            {
                return (int)GetValue(BatteryPercentProperty);
            }
            set
            {
                SetValue(BatteryPercentProperty, value);
            }
        }

        public static readonly BindableProperty BatteryPercentVoltageProperty = BindableProperty.Create(
          "BatteryPercentVoltage",
          typeof(float),
          typeof(BatteryView),
          0f,
          BindingMode.OneWay);

        public float BatteryPercentVoltage
        {
            get
            {
                //take voltage minus bottom (if voltage over 50, bottom is 43.5 else bottom is 44 for a safety gap)
                //then divde by 18.75
                //then multiply by 100
                /*
                 62.25 , 100
                60.75 , 93
                60 , 86
                58.8 , 79
                56.7 , 65
                55.5 , 58
                54.75 , 51
                54 , 44
                52.95 , 37
                51.75 , 30
                51 , 23
                48 , 16
                45 , 9
                43.5 , 0
                 */
                //float _voltage = _batteryVoltage;
                //float _bottom = (float)43.5;
                //if (_voltage >= 50) { _bottom = 44; }
                
                ////return (float)((_voltage - _bottom) / 18.75) * 100;
                return (float)GetValue(BatteryPercentVoltageProperty);
            }
            set
            {
                SetValue(BatteryPercentVoltageProperty, value);
            }
        }

        public static readonly BindableProperty BatteryVoltageProperty = BindableProperty.Create(
          "BatteryVoltage",
          typeof(float),
          typeof(BatteryView),
          0f,
          BindingMode.OneWay);
        private float _batteryVoltage = 1;
        public float BatteryVoltage
        {
            get
            {
                return (float)GetValue(BatteryVoltageProperty);
            }
            set
            {
                _batteryVoltage = value;
                SetValue(BatteryVoltageProperty, value);
            }
        }

        public static readonly BindableProperty BatteryCellsProperty = BindableProperty.Create(
          "BatteryCells",
          typeof(BatteryCells),
          typeof(BatteryView),
          null,
          BindingMode.OneWay);

        public BatteryCells BatteryCells
        {
            get
            {
                return (BatteryCells)GetValue(BatteryCellsProperty);
            }
            set
            {
                SetValue(BatteryCellsProperty, value);
            }
        }



        public BatteryView()
        {
            InitializeComponent();
            MainView.BindingContext = this;
        }

        /*
        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

           

            System.Diagnostics.Debug.WriteLine($"OnPropertyChanging: {propertyName}");
        }
        */

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            //System.Diagnostics.Debug.WriteLine($"OnPropertyChanged: {propertyName}");
            if (BatteryPercentProperty.PropertyName.Equals(propertyName))
            {

                BatteryBar.AnimateWidthPercent((float)BatteryPercent * 0.01);
            }
            else if (BatteryCellsProperty.PropertyName.Equals(propertyName))
            {
                //BatteryCellsView.BindingContext = BatteryCells;
            }
            else if (BatteryPercentVoltageProperty.PropertyName.Equals(propertyName))
            {
                System.Diagnostics.Debug.WriteLine($"OnPropertyChanged: {propertyName}");
            }
            else if (BatteryVoltageProperty.PropertyName.Equals(propertyName))
            {
                float _voltage = BatteryVoltage;
                float _bottom = (float)43.5;
                if (_voltage >= 50) { _bottom = 44; }
                //linear conversion
                //BatteryPercentVoltage = (float)((_voltage - _bottom) / 18.75) * 100;

                BatteryPercentVoltage = BatteryVoltageConverter.GetBatteryPercentEstimate(BatteryVoltage);
            }
        }
        /*
        private void BatteryCells_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"BatteryCells_PropertyChanged: {e.PropertyName}");
        }
        */

        void ExpanderView_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Expander.IsExpandedProperty.PropertyName.Equals(e.PropertyName))
            {
                if (ExpanderView.IsExpanded)
                {
                    ExpanderArrow.RotateTo(180, ExpanderView.ExpandAnimationLength, ExpanderView.ExpandAnimationEasing);
                }
                else
                {
                    ExpanderArrow.RotateTo(0, ExpanderView.CollapseAnimationLength, ExpanderView.CollapseAnimationEasing);
                }
            }
        }
    }
}
