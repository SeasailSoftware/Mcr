   M S B A M L   `   `   `  ����   ReagentStripTest9    3ReagentStripTest.Views.Calibrate.BlackCalibrateView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  .��� 
�������.+��� .+��� $
��10,3q�  �� Orientation$  
Horizontal=�.+��� $��15��$��15��$��3q�$��Center=�   MahApps.Brushes.Accent#��[   �� CornerRadius$ 15���� $��0��$��3q�$��Center=�$��14(�$��DemiBold"�#��[  $��	黑校正���� $	��300��$��3q�
�������$ 5��  DropShadowEffect J� Effect# [ ._��� $��20q�$��Center=�$��Center=�$��16(�
�������$H��B请把黑板放置测量口,然后点击[校准]按钮开始校准��$
��Wrap=��� $
��20,3q�$  
Horizontal=�.+��� $��10��$��10��
�������$ 10���� $��0��$��5q�$��Center=�$��14(�$��DemiBold"�
�������$��	白校正���� $	��300��$	��220��$��10q�$&�� ../../Images/black_calibrate.jpg��$
��Fill=��� y��� *IsCalibrating+   BooleanToVisibilityConverter �� 	Converter# [ .+��� $��Center=�$��Center=�#���     s_CalibratingPrompt#���  -  'MahApps.Metro.Controls.MetroProgressBar  $��3q�#���    � IsIndeterminate �� *IsCalibrating+�� $��1��$��5q�$��Center=�$  
Horizontal=� � 	Resources   MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property	 �� Value��%�   '��       7  7   '�� g     �� 8   " ��.���  � ��$	 15(���  � ��8	  ��  � ��$
	 Bold"���  � ��$	 5q��� " ��.���  � ��$	 20����  � ��$	 20��.+��� $	��120��$
��10,5q����� *CalibrateCommand+.���� $  
Horizontal=�.+��� ��4  .MahApps.Metro.IconPacks.MaterialImageExtension  
   Kind$
 Target=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush# [  �� #��[  $��校正���� $	��120��$
��10,5q����� *CancelCommand+.���� $  
Horizontal=�.+��� ��  $
 Cancel=�# [  �� #��[    s_Cancel#��[ 