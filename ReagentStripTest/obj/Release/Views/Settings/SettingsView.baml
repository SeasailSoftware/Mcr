   M S B A M L   `   `   `  ���� M  IReagentStripTest, Version=2.0.925.2, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ReagentStripTest.Views.SettingsReagentStripTest.Views.Settings  2    ,ReagentStripTest.Views.Settings.SettingsView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   ;local-clr-namespace:ReagentStripTest.Views.Settings   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006     
s_Settings  � Title#  �   $	��350�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � SizeToContent$ Height=� � WindowStartupLocation$ CenterOwner=� � 	Resources   MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property	 �� Value  MahApps.Styles.ComboBox  EnumDescriptionConverter
 �� 	Converter��%�   '��       7  '�� A     7  7  �� 8   " ��.���  � ��$	 3q���  � ��$	 Center=��� 8   " ��.���  � ��$	 3q���  � ��$
	 Left=���  � ��	 �� .R��� ���� 8
  .��� $��20,10q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� .+���   s_Illuminant#���  �� $��0��$��1��$��2������ *Illuminants+ �� SelectedItem �� *
Illuminant+�� $��1��  
s_Observer#���  �� $��1��$��1��$��2������ *	Observers+ �� *
Observer+�� $��2��  s_SpecifiedWaveLength#���  �� $��2��$��1��$��2������ *WaveLengths+ �� *SpecifiedWaveLength+�� $��3��  s_Lower#���  *  $MahApps.Metro.Controls.NumericUpDown  $��3��$��1��$��2��$��3q�   HideUpDownButtons .    StringFormat$ F2��   TextAlignment$
 Left=�   Value �� *Lower+�� $��4��  s_Upper#���    $��4��$��1��$��2��$��3q� . $ F2��$
 Left=� �� *Upper+�� $��5�� 	 s_MeasurementDuration#��� 	   $��5��$��1��$��2��$��3q� . $
 {0}s��$
 Left=� �� *MeasurementDuration+�� $��6�� 
 s_MeasurementInterval#��� 
   $��6��$��1��$��2��$��3q� . $ {0}ms��$
 Left=� �� *MeasurementInterval+�� $��7��  s_DataChannel#���  �� $��7��$��1��$��2��$��3q� �� SelectedIndex �� *DataChannel+.@���   s_DataChannel1#���  ��   s_DataChannel2#���  ��   s_DataChannel3#���  �� $��8��  s_ReflectanceFactor#���  -  'MahApps.Metro.Controls.VisibilityHelper   	IsVisible �� *IsDataChannel3+�� $��8��$��1��$��2��$��80��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$0 *31个反射率数据,中间用逗号隔开�� �� TextWrapping$
 Wrap=�$��3q� �� TextAlignment$
 Left=� �� *IsDataChannel3+���� *ReflectanceFactors+A� $��9��$��3��$	��0,5q�$��Right=� A� Rows$ 1��.+��� $��95��$
��10,3q����� *AcceptCommand+.����  �� Orientation$ 
Horizontal=�.+��� $��15��$��3q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� $��3q�$��Center=�  s_Accept#���  �� $
��10,3q� �� IsCancel . .���� $ 
Horizontal=�.+��� $��15��$��3q���  $ Close=�
 �  ���� $��3q�$��Center=�  s_Cancel#���  