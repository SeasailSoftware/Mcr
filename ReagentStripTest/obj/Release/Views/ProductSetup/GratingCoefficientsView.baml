   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MasTools.Views.ProductSetupMasTools.Views.ProductSetup  9    3MasTools.Views.ProductSetup.GratingCoefficientsView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:MasTools.Views.ProductSetup     T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  .���   � RowDefinitions  �� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$��3q�$��Center=�$��14(�
�������$��感应器编号:���� $��0��$��1��$��3q�$��Center=�$��14(����� *SN+�� $��1��$��2������ *
FloatItems+���� .R�� $��5q� �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��3q�$��Center=�$��16(�$��Light"�
�������$
��Item���� $��1��$��3q�$��Center=�$��16(�$��Light"�
����������� *Index+*  $MahApps.Metro.Controls.NumericUpDown  $��2��$��3q�$��Center=�$��16(�   HideUpDownButtons .    StringFormat$ E��   Value �� *Value+