using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace hw_8_WPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ColorItem> ColorList { get; set; } = new ObservableCollection<ColorItem>();

        private byte _alpha = 255;
        private byte _red = 1;
        private byte _green = 1;
        private byte _blue = 1;

        private bool _enableAlpha = true;
        private bool _enableRed = true;
        private bool _enableGreen = true;
        private bool _enableBlue = true;

        public byte Alpha { get => _alpha; set { _alpha = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentColor)); } }
        public byte Red { get => _red; set { _red = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentColor)); } }
        public byte Green { get => _green; set { _green = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentColor)); } }
        public byte Blue { get => _blue; set { _blue = value; OnPropertyChanged(); OnPropertyChanged(nameof(CurrentColor)); } }

        public bool EnableAlpha { get => _enableAlpha; set { _enableAlpha = value; OnPropertyChanged(); } }
        public bool EnableRed { get => _enableRed; set { _enableRed = value; OnPropertyChanged(); } }
        public bool EnableGreen { get => _enableGreen; set { _enableGreen = value; OnPropertyChanged(); } }
        public bool EnableBlue { get => _enableBlue; set { _enableBlue = value; OnPropertyChanged(); } }

        public SolidColorBrush CurrentColor => new SolidColorBrush(Color.FromArgb(Alpha, Red, Green, Blue));

        public ICommand AddColorCommand { get; }
        public ICommand RemoveColorCommand { get; }

        public MainViewModel()
        {
            AddColorCommand = new RelayCommand(_ => AddColor(), _ => CanAddColor());
            RemoveColorCommand = new RelayCommand(RemoveColor);
        }

        private void AddColor()
        {
            var hex = $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";
            if (!ColorList.Any(c => c.Hex == hex))
            {
                ColorList.Add(new ColorItem { Brush = CurrentColor, Hex = hex });
            }
        }

        private bool CanAddColor()
        {
            return !ColorList.Any(c => c.Hex == $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}");
        }

        private void RemoveColor(object parameter)
        {
            if (parameter is ColorItem colorItem)
            {
                ColorList.Remove(colorItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
