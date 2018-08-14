using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExpenseTracker.Controls
{
    internal sealed class RoundButton : Button
    {
        public static readonly DependencyProperty ButtonBorderThicknessProperty = DependencyProperty.Register(
           "ButtonBorderThickness",
           typeof(Thickness),
           typeof(RoundButton),
           new PropertyMetadata(new Thickness(0)));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
           "CornerRadius",
           typeof(CornerRadius),
           typeof(RoundButton),
           new PropertyMetadata(new CornerRadius(0)));
        public static readonly DependencyProperty EnableBackgroundProperty = DependencyProperty.Register(
           "EnableBackground",
           typeof(SolidColorBrush),
           typeof(RoundButton),
           new PropertyMetadata(null));
        public static readonly DependencyProperty EnableCornerColorProperty = DependencyProperty.Register(
           "EnableCornerColor",
           typeof(SolidColorBrush),
           typeof(RoundButton),
           new PropertyMetadata(null));
        public static readonly DependencyProperty EnableTextColorProperty = DependencyProperty.Register(
           "EnableTextColor",
           typeof(SolidColorBrush),
           typeof(RoundButton),
           new PropertyMetadata(null));
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register(
           "MouseOverBackground",
           typeof(SolidColorBrush),
           typeof(RoundButton),
           new PropertyMetadata(null));
        public static readonly DependencyProperty MousePressedBackgroundProperty = DependencyProperty.Register(
           "MousePressedBackground",
           typeof(SolidColorBrush),
           typeof(RoundButton),
           new PropertyMetadata(null));

        public Thickness ButtonBorderThickness
        {
            get => (Thickness)GetValue(ButtonBorderThicknessProperty);
            set => SetValue(ButtonBorderThicknessProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public SolidColorBrush EnableBackground
        {
            get => (SolidColorBrush)GetValue(EnableBackgroundProperty);
            set => SetValue(EnableBackgroundProperty, value);
        }

        public SolidColorBrush EnableCornerColor
        {
            get => (SolidColorBrush)GetValue(EnableCornerColorProperty);
            set => SetValue(EnableCornerColorProperty, value);
        }

        public SolidColorBrush EnableTextColor
        {
            get => (SolidColorBrush)GetValue(EnableTextColorProperty);
            set => SetValue(EnableTextColorProperty, value);
        }

        public SolidColorBrush MouseOverBackground
        {
            get => (SolidColorBrush)GetValue(MouseOverBackgroundProperty);
            set => SetValue(MouseOverBackgroundProperty, value);
        }

        public SolidColorBrush MousePressedBackground
        {
            get => (SolidColorBrush)GetValue(MousePressedBackgroundProperty);
            set => SetValue(MousePressedBackgroundProperty, value);
        }
    }
}