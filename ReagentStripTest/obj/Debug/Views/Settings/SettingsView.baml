   M S B A M L   `   `   `  ����M  IReagentStripTest, Version=2.0.925.1, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ReagentStripTest.Views.SettingsReagentStripTest.Views.Settings  5      2    ,ReagentStripTest.Views.Settings.SettingsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      ;local-clr-namespace:ReagentStripTest.Views.Settings   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	         
s_Settings  � Title#  �   5
      $	��350��5       � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � SizeToContent$ Height=�5       � WindowStartupLocation$ CenterOwner=�5       � 	Resources 5        MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property	 �� Value  MahApps.Styles.ComboBox  EnumDescriptionConverter
 �� 	Converter��%m  '��       7  '�� t     7  7  �� 5   
   8   " ��6D   .�5      ��  � ��$	 3q�6'   �� 5       � ��$	 Center=�62   5      �� 5   
   8   " ��6C   .�5      ��  � ��$	 3q�6'   �� 5       � ��$
	 Left=�6;   �� 5       � ��	 5      �� 5      .R�5      �� ��6$   �� 8
  5      5      5       5!      .��5"      � $��20,10q�6    � ColumnDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $��* �6   �� 5&      $
��Auto �6    � RowDefinitions 5(   
   �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      �� 50      �� 51      �� 52      �� 53      �� 54      .+�56   
   ��   s_Illuminant#���  6   �� 57   
   $��0��58      $��1��59      $��2��5:      ��5;      �� *Illuminants+ �� SelectedItem 5<      �� *
Illuminant+�� 5=   
   $��1��6     
s_Observer#���  6!   �� 5>   
   $��1��5?      $��1��5@      $��2��5A      ��5B      �� *	Observers+ 5C      �� *
Observer+�� 5D   
   $��2��6     s_SpecifiedWaveLength#���  6!   �� 5E   
   $��2��5F      $��1��5G      $��2��5H      ��5I      �� *WaveLengths+ 5J      �� *SpecifiedWaveLength+�� 5K   
   $��3��6     s_Lower#���  6!   *  $MahApps.Metro.Controls.NumericUpDown  5L   
   $��3��5M      $��1��5N      $��2��5O      $��3q�5P         HideUpDownButtons .    StringFormat$ F2��5R         TextAlignment$
 Left=�5S         Value 5T      �� *Lower+�� 5U   
   $��4��6     s_Upper#���  6!     5V   
   $��4��5W      $��1��5X      $��2��5Y      $��3q�5Z       . $ F2��5\      $
 Left=�5]       5^      �� *Upper+�� 5`   
   $��5��6    	 s_MeasurementDuration#��� 	 6!     5a   
   $��5��5b      $��1��5c      $��2��5d      $��3q�5e       . $
 {0}s��5g      $
 Left=�5h       5i      �� *MeasurementDuration+�� 5j   
   $��6��6    
 s_MeasurementInterval#��� 
 6!     5k   
   $��6��5l      $��1��5m      $��2��5n      $��3q�5o       . $ {0}ms��5q      $
 Left=�5r       5s      �� *MeasurementInterval+�� 5t   
   $��7��6     s_DataChannel#���  6!   �� 5u   
   $��7��5v      $��1��5w      $��2��5x      $��3q�5y       �� SelectedIndex 5z      �� *DataChannel+.@�5{      ��   s_DataChannel1#���  6   �� 5|        s_DataChannel2#���  6   �� 5}        s_DataChannel3#���  6   5~      �� 5   
   $��8��5�        s_ReflectanceFactor#���  5�      -  'MahApps.Metro.Controls.VisibilityHelper   	IsVisible 5�      �� *IsDataChannel3+5�      �� 5�   
   $��8��5�      $��1��5�      $��2��5�      $��80��5�      *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$0 *31个反射率数据,中间用逗号隔开��5�       �� TextWrapping$
 Wrap=�5�      $��3q�5�       �� TextAlignment$
 Left=�5�       5�      �� *IsDataChannel3+��5�      �� *ReflectanceFactors+A� 5�   
   $��9��5�      $��3��5�      $	��0,5q�5�      $��Right=�5�       A� Rows$ 1��5�      .+�5�      �� $��95��5�      $
��10,3q�5�      ��5�      �� *AcceptCommand+.��5�      ��  �� Orientation$ 
Horizontal=�6   .+�5�      �� $��15��5�      $��3q�5�      ��5�      4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� 5�      $��3q�5�      $��Center=�5�        s_Accept#���  5�      5�      5�      �� 5�      $
��10,3q�6    �� IsCancel . .��5�      �� $ 
Horizontal=�6   .+�5�      �� $��15��5�      $��3q�5�      ��5�        $ Close=�
 �  ���� 5�      $��3q�5�      $��Center=�5�        s_Cancel#���  5�      5�      5�      5�      5�      5�      