   M S B A M L   `   `   `  ���� M  IReagentStripTest, Version=2.0.925.2, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ReagentStripTest.Views.Calibrate ReagentStripTest.Views.Calibrate  9    3ReagentStripTest.Views.Calibrate.WhiteCalibrateView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   <local.clr-namespace:ReagentStripTest.Views.Calibrate   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  .��� 
�������.+��� .+��� $
��10,3q�  �� Orientation$  
Horizontal=�.+��� $��15��$��15��$��3q�
������� �� CornerRadius$ 10���� $��0��$��3q�$��Center=�$��14(�$
��Bold"�
�������$��	黑校正���� $��15��$	��5,3q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� $
��10,3q�$  
Horizontal=�.+��� $��15��$��15��$��3q�   MahApps.Brushes.Accent#��[  $ 10���� $��0��$��5q�$��Center=�$��14(�$��DemiBold"�#��[  $��	白校正���� $	��300��$��5q�
�������$ 10��  DropShadowEffect J� Effect# [ ._��� $��20q�$��Center=�$��Center=�$��16(�
�������$H��B请把白板放置测量口,然后点击[校准]按钮开始校准��$
��Wrap=��� $	��300��$	��220��$��10q�$&�� ../../Images/white_calibrate.jpg��$
��Fill=��� -    ��Name$ ErrorMsg��$��5q�$��Center=�$��Center=�$��15(�
���  ���� y��� *IsCalibrating+   BooleanToVisibilityConverter �� 	Converter# [ .+��� $��Center=�$��Center=�#���     s_CalibratingPrompt#���  -  'MahApps.Metro.Controls.MetroProgressBar  $��3q�#���    � IsIndeterminate �� *IsCalibrating+�� $��1��$��5q�$��Center=�$  
Horizontal=� � 	Resources   MahApps.Styles.TextBlock	 �� BasedOn
 �� 
TargetType �� Property �� Value��%�   '��       7  7   '�� g     �� 8	   "
 ��.���  � ��$ 15(���  � ��8  ��  � ��$
 Bold"���  � ��$ 5q��� "
 ��.���  � ��$ 20����  � ��$ 20��.+��� $	��120��$
��10,5q����� *CalibrateCommand+.���� $  
Horizontal=�.+��� ��  $ Target=�# [  �� #��[  $��校正���� $	��120��$
��10,5q����� *CancelCommand+.���� $  
Horizontal=�.+��� ��  $ Cancel=�# [  �� #��[    s_Cancel#��[ 