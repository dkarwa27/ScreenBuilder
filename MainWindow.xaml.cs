using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ScreenBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        Button btn;
        Label lbl;
        CheckBox chk;
        TextBox txt;
        
        Button button;
        Label label;
        CheckBox checkBox;
        TextBox textBox;

        Point relativeListBoxPoint;
        Point relativeLabelPoint;
        Point relativeTextBoxPoint;
        Point relativeButtonPoint;
        Point relativeCheckBoxPoint;

        ObservableCollection<Control> controlList = new ObservableCollection<Control>();

        public MainWindow()
        {
            InitializeComponent();

            txt = new TextBox
            {
                Text = "TextBox",
                Width = 80,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            lbl = new Label
            {
                Content = "Label",
                Width = 80,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            chk = new CheckBox
            {
                Content = "CheckBox",
                Width = 80,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            btn = new Button
            {
                Content = "Button",
                Width = 80,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            txt.MouseDoubleClick += Txt_MouseDoubleClick;
            lbl.MouseDoubleClick += Lbl_MouseDoubleClick;
            chk.MouseDoubleClick += Chk_MouseDoubleClick;
            btn.MouseDoubleClick += Btn_MouseDoubleClick;

            controlList.Add(txt);
            controlList.Add(lbl);
            controlList.Add(chk);
            controlList.Add(btn);

            //Initialize the controls listbox
            controlLstBox.ItemsSource = controlList;
        }

        /// <summary>
        /// Event handler for textbox mouse double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            relativeTextBoxPoint = txt.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            relativeListBoxPoint = lstBox.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            textBox = new TextBox();
            textBox.Width = txt.Width;
            textBox.Height = txt.Height;
            textBox.Text = txt.Text;
            textBox.HorizontalAlignment = txt.HorizontalAlignment;
            textBox.VerticalAlignment = txt.VerticalAlignment;
            Canvas.SetLeft(textBox, 0);
            Canvas.SetTop(textBox, 0);
            canvas.Children.Add(textBox);

            AnimateControl(textBox, relativeTextBoxPoint);
        }

        /// <summary>
        /// Event handler for label mouse double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            relativeLabelPoint = lbl.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            relativeListBoxPoint = lstBox.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            label = new Label();
            label.Width = lbl.Width;
            label.Height = lbl.Height;
            label.Content = lbl.Content;
            label.HorizontalAlignment = lbl.HorizontalAlignment;
            label.VerticalAlignment = lbl.VerticalAlignment;
            label.VerticalContentAlignment = lbl.VerticalContentAlignment;
            Canvas.SetLeft(label, 0);
            Canvas.SetTop(label, 30);
            canvas.Children.Add(label);

            AnimateControl(label, relativeLabelPoint);
        }

        /// <summary>
        /// Event handler for checkbox mouse double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            relativeCheckBoxPoint = chk.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            relativeListBoxPoint = lstBox.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            checkBox = new CheckBox();
            checkBox.Width = chk.Width;
            checkBox.Height = chk.Height;
            checkBox.Content = chk.Content;
            checkBox.HorizontalAlignment = chk.HorizontalAlignment;
            checkBox.VerticalAlignment = chk.VerticalAlignment;
            checkBox.VerticalContentAlignment = chk.VerticalContentAlignment;
            Canvas.SetLeft(checkBox, 0);
            Canvas.SetTop(checkBox, 60);
            canvas.Children.Add(checkBox);

            AnimateControl(checkBox, relativeCheckBoxPoint);
        }

        /// <summary>
        /// Event handler for button mouse double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            relativeButtonPoint = btn.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            relativeListBoxPoint = lstBox.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

            button = new Button();
            button.Width = btn.Width;
            button.Height = btn.Height;
            button.Content = btn.Content;
            button.HorizontalAlignment = btn.HorizontalAlignment;
            button.VerticalAlignment = btn.VerticalAlignment;
            Canvas.SetLeft(button, 0);
            Canvas.SetTop(button, 90);
            canvas.Children.Add(button);

            AnimateControl(button, relativeButtonPoint);
        }

        /// <summary>
        /// Create animations and storyboards
        /// </summary>
        /// <param name="control"></param>
        /// <param name="relativePoint"></param>
        private void AnimateControl(Control control, Point relativePoint)
        {
            DoubleAnimation xDoubleAnimation = GetDoubleAnimation(relativePoint.X, relativeListBoxPoint.X);
            DoubleAnimation yDoubleAnimation = GetDoubleAnimation(relativePoint.Y, (lstBox.Items.Count * 25) + 25);
            Storyboard xStoryboard = GetStoryboard(xDoubleAnimation, control, Canvas.LeftProperty);
            Storyboard yStoryboard = GetStoryboard(yDoubleAnimation, control, Canvas.TopProperty);

            yStoryboard.Completed += delegate
            {
                canvas.Children.Remove(control);
                lstBox.Items.Add(control);
                lstBox.ScrollIntoView(control);
                lstBox.SelectedItem = control;
            };

            xStoryboard.Begin(this);
            yStoryboard.Begin(this);
        }

        /// <summary>
        /// Initialize double animation
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>DoubleAnimation</returns>
        private DoubleAnimation GetDoubleAnimation(double from, double to)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = from;
            doubleAnimation.To = to;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            return doubleAnimation;
        }

        /// <summary>
        /// Initialize storyboard
        /// </summary>
        /// <param name="doubleAnimation"></param>
        /// <param name="control"></param>
        /// <param name="parameter"></param>
        /// <returns>Storyboard</returns>
        private Storyboard GetStoryboard(DoubleAnimation doubleAnimation, Control control, object parameter)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation, control);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(parameter));
            return storyboard;
        }

        ListBoxItem sourceListBoxItem;

        /// <summary>
        /// PreviewMouseLeftButtonDown Event handler for listboxitem 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sourceListBoxItem = sender as ListBoxItem;
            if (sourceListBoxItem != null)
            {
                DragDrop.DoDragDrop(sourceListBoxItem, sourceListBoxItem.DataContext, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Drop Event handler for listboxitem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstBoxItem_Drop(object sender, DragEventArgs e)
        {
            Control droppedData = sourceListBoxItem.DataContext as Control;
            Control target = ((ListBoxItem)(sender)).DataContext as Control;

            int removedIndex = lstBox.Items.IndexOf(droppedData);
            int targetIndex = lstBox.Items.IndexOf(target);

            if (removedIndex < targetIndex)
            {
                lstBox.Items.RemoveAt(removedIndex);
                lstBox.Items.Insert(targetIndex, droppedData);
            }
            else
            {
                int remIdx = removedIndex;
                if (lstBox.Items.Count > remIdx)
                {
                    lstBox.Items.RemoveAt(remIdx);
                    lstBox.Items.Insert(targetIndex, droppedData);
                }
            }
        }
    }
}
