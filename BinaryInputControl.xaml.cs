using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace NerdGame
{
public enum DisplayMode
{
    Hex,
    Decimal
}

public partial class BinaryInputControl : UserControl
    {
        public DisplayMode Mode { get; set; } = DisplayMode.Hex;

        private byte dataByte;
        private bool isInternalUpdate = false;

        public delegate void ValueChange(byte newVal);
        public event ValueChange ValueChanged;
        public BinaryInputControl()
        {
            InitializeComponent();
        }
        public byte DataByte
        {
            get => dataByte;
            set
            {
                dataByte = value;
                UpdateToggleButtons();
                UpdateHexDisplay();
            }
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
                UpdateValueFromButtons();
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
                UpdateValueFromButtons();
        }
        private void UpdateValueFromButtons()
        {
            byte val = 0;
            val |= Bit0.IsChecked == true ? (byte)(1 << 0) : (byte)0;
            val |= Bit1.IsChecked == true ? (byte)(1 << 1) : (byte)0;
            val |= Bit2.IsChecked == true ? (byte)(1 << 2) : (byte)0;
            val |= Bit3.IsChecked == true ? (byte)(1 << 3) : (byte)0;
            val |= Bit4.IsChecked == true ? (byte)(1 << 4) : (byte)0;
            val |= Bit5.IsChecked == true ? (byte)(1 << 5) : (byte)0;
            val |= Bit6.IsChecked == true ? (byte)(1 << 6) : (byte)0;
            val |= Bit7.IsChecked == true ? (byte)(1 << 7) : (byte)0;

            dataByte = val;
            UpdateBitLabels();
            UpdateHexDisplay();
            ValueChanged?.Invoke(dataByte);
        }

        private void UpdateBitLabels()
        {
            Bit0.Content = Bit0.IsChecked == true ? "1" : "0";
            Bit1.Content = Bit1.IsChecked == true ? "1" : "0";
            Bit2.Content = Bit2.IsChecked == true ? "1" : "0";
            Bit3.Content = Bit3.IsChecked == true ? "1" : "0";
            Bit4.Content = Bit4.IsChecked == true ? "1" : "0";
            Bit5.Content = Bit5.IsChecked == true ? "1" : "0";
            Bit6.Content = Bit6.IsChecked == true ? "1" : "0";
            Bit7.Content = Bit7.IsChecked == true ? "1" : "0";
        }


        private void UpdateHexDisplay()
        {
            if (HexDisplay == null) return;

            if (Mode == DisplayMode.Hex)
            {
                HexDisplay.Content = $"0x{dataByte.ToString("X2")}";
            }
            else
            {
                HexDisplay.Content = dataByte.ToString();
            }
        }

        private void UpdateToggleButtons()
        {
            isInternalUpdate = true;
            Bit0.IsChecked = (dataByte & (1 << 0)) != 0;
            Bit0.Content = Bit0.IsChecked == true ? "1" : "0";
            Bit1.IsChecked = (dataByte & (1 << 1)) != 0;
            Bit1.Content = Bit1.IsChecked == true ? "1" : "0";
            Bit2.IsChecked = (dataByte & (1 << 2)) != 0;
            Bit2.Content = Bit2.IsChecked == true ? "1" : "0";
            Bit3.IsChecked = (dataByte & (1 << 3)) != 0;
            Bit3.Content = Bit3.IsChecked == true ? "1" : "0";
            Bit4.IsChecked = (dataByte & (1 << 4)) != 0;
            Bit4.Content = Bit4.IsChecked == true ? "1" : "0";
            Bit5.IsChecked = (dataByte & (1 << 5)) != 0;
            Bit5.Content = Bit5.IsChecked == true ? "1" : "0";
            Bit6.IsChecked = (dataByte & (1 << 6)) != 0;
            Bit6.Content = Bit6.IsChecked == true ? "1" : "0";
            Bit7.IsChecked = (dataByte & (1 << 7)) != 0;
            Bit7.Content = Bit7.IsChecked == true ? "1" : "0";
            isInternalUpdate = false;
        }
        public void ClearAllBits()
        {
            Bit0.IsChecked = false;
            Bit1.IsChecked = false;
            Bit2.IsChecked = false;
            Bit3.IsChecked = false;
            Bit4.IsChecked = false;
            Bit5.IsChecked = false;
            Bit6.IsChecked = false;
            Bit7.IsChecked = false;

            dataByte = 0;
            UpdateHexDisplay();

            isInternalUpdate = false;
        }

    }
}
