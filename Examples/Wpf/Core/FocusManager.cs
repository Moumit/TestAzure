using Caliburn.Micro;
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
 

namespace Wpf
{
    public static class FocusManager
    {
        public static bool SetFocus(this BaseViewModel screen, Expression<Func<object>> propertyExpression, string ErrorMessage = null)
        {
            return SetFocus(screen, propertyExpression.GetMemberInfo().Name, ErrorMessage);
        }

        public static bool SetFocus(this BaseViewModel screen, string property, string ErrorMessage = null)
        {
            Contract.Requires(property != null, "Property cannot be null.");
            var view = screen.Owner;
            if (view != null)
            {
                var control = FindChild(view, property);
                return SetFocus(control, ErrorMessage);

            }
            return false;
        }

        public static bool SetFocus(FrameworkElement control,string ErrorMessage)
        {
            bool focus = control != null && control.Focus();

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                return false;
            }

            Popup myPopup = new Popup();
            myPopup.PlacementTarget = control; //FrameworkElement where you want to show this tooltip
            myPopup.Placement = PlacementMode.Top;
            myPopup.PopupAnimation = PopupAnimation.Slide;
            myPopup.AllowsTransparency = true;
            myPopup.AllowsTransparency = true;
            TextBlock popupText = new TextBlock();
            popupText.Padding = new Thickness(5);
            popupText.Text = ErrorMessage; //Message you want to show
            popupText.Background = Brushes.Red;
            popupText.Foreground = Brushes.White;
            //popupText.FontSize = 12;
            popupText.TextWrapping = TextWrapping.Wrap;
            myPopup.Child = popupText;
            // popup1.CustomPopupPlacementCallback =
            // new CustomPopupPlacementCallback(placePopup);

            //myPopup.HorizontalOffset = control.ActualWidth - popupText.ActualWidth;
            myPopup.IsOpen = true;
            myPopup.StaysOpen = false;

            control.ToolTip = myPopup;
            TimerCallback ToolTipClosingCallBack = (s) =>
            {
                control.Dispatcher.Invoke(() =>
                {
                    myPopup.IsOpen = false;
                });
            };
            var timer = new Timer(ToolTipClosingCallBack, null, 2000, Timeout.Infinite);

            return true;
        }

        private static FrameworkElement FindChild(UIElement parent, string childName)
        {
            // Confirm parent and childName are valid.
            if (parent == null || string.IsNullOrWhiteSpace(childName)) return null;

            FrameworkElement foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var ctrl = VisualTreeHelper.GetChild(parent, i);
                FrameworkElement child = ctrl as FrameworkElement;
                

               
                if (child != null)
                {
                    BindingExpression bindingExpression = GetBindingExpression(child, childName);
                    if (child.Name == childName)
                    {
                        foundChild = child;
                        break;
                    }
                    if (bindingExpression != null)
                    {
                        if (bindingExpression.ResolvedSourcePropertyName == childName)
                        {
                            foundChild = child;
                            break;
                        }
                    }
                    foundChild = FindChild(child, childName);
                    if (foundChild != null)
                    {
                        if (foundChild.Name == childName)
                            break;
                        BindingExpression foundChildBindingExpression = GetBindingExpression(foundChild, childName);
                        if (foundChildBindingExpression != null &&
                            foundChildBindingExpression.ResolvedSourcePropertyName == childName)
                            break;
                    }                    
                }
            }

            return foundChild;
        }

        private static BindingExpression GetBindingExpression(FrameworkElement control,string PropertyName)
        {
            if (control == null) return null;

            BindingExpression bindingExpression = null;
            var convention = ConventionManager.GetElementConvention(control.GetType());
            if (convention != null)
            {
                var bindablePro = convention.GetBindableProperty(control);
                if (bindablePro != null)
                {
                    bindingExpression = control.GetBindingExpression(bindablePro);
                }
            }
            if (bindingExpression == null)
            {
                if (!control.GetType().ToString().StartsWith("System"))
                {
                    var allProps = control.GetAttachedProperties();

                    foreach (var item in allProps)
                    {
                        var exp = control.GetBindingExpression(item);

                        if (exp?.ResolvedSourcePropertyName == PropertyName)
                        {
                            bindingExpression = exp;
                            break;
                        }

                    }
                }           
                
            }
            return bindingExpression;
        }

        public static List<DependencyProperty> GetAttachedProperties(this DependencyObject obj)
        {
            List<DependencyProperty> result = new List<DependencyProperty>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(obj,
                new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }))
            {
                DependencyPropertyDescriptor dpd =
                    DependencyPropertyDescriptor.FromProperty(pd);

                if (dpd != null)
                {
                    result.Add(dpd.DependencyProperty);
                }
            }

            //result=  result.OrderBy(i=>i.Name).ToList();

            return result;
        }
    }
}