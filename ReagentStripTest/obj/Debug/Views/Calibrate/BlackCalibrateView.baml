   M S B A M L   `   `   `  ����  ReagentStripTest9    3ReagentStripTest.Views.Calibrate.BlackCalibrateView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5
      � 
�������.+�5   
   �� .+�5      �� $
��10,3q�6     �� Orientation$  
Horizontal=�6'   .+�5      �� $��15��5      $��15��5      $��3q�5      $��Center=�5         MahApps.Brushes.Accent#��[  5       �� CornerRadius$ 15��5      �� 5      $��0��5      $��3q�5      $��Center=�5      $��14(�5      $��DemiBold"�5      #��[  5      $��	黑校正��5      5      �� 5      $	��300��5      $��3q�5      
�������$ 5��5!        DropShadowEffect J� Effect# [ 5"      ._�5#      �� $��20q�5$      $��Center=�5%      $��Center=�5&      $��16(�5'      
�������$H��B请把黑板放置测量口,然后点击[校准]按钮开始校准��5)      $
��Wrap=�5*      5+      �� 5,      $
��20,3q�6   $  
Horizontal=�6'   .+�5-      �� $��10��5.      $��10��5/      
�������$ 10��51      �� 52      $��0��53      $��5q�54      $��Center=�55      $��14(�56      $��DemiBold"�57      
�������$��	白校正��59      5:      �� 5;      $	��300��5<      $	��220��5=      $��10q�5>      $&�� ../../Images/black_calibrate.jpg��5?      $
��Fill=�5@      �� 5A      y�6   �� *IsCalibrating+   BooleanToVisibilityConverter �� 	Converter# [ .+�5B      �� $��Center=�5C      $��Center=�5D      #���   5E        s_CalibratingPrompt#���  5F      -  'MahApps.Metro.Controls.MetroProgressBar  5G      $��3q�5H      #���   5I       � IsIndeterminate 5J      �� *IsCalibrating+5K      �� 5L      $��1��5M      $��5q�5N      $��Center=�5O      $  
Horizontal=�5P       � 	Resources 5Q        MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property	 �� Value��%>  '��       7  7   '�� �     �� 5R      8   " ��6P   .�5S      ��  � ��$	 15(�65   �� 5T       � ��8	  67   �� 5U       � ��$
	 Bold"�67   �� 5V       � ��$	 5q�63   5W      �� 5X      " ��6   .�5Y      ��  � ��$	 20��63   �� 5Z       � ��$	 20��62   5[      5\      .+�5]      �� $	��120��5^      $
��10,5q�5_      ��5`      �� *CalibrateCommand+.��5a      �� $  
Horizontal=�6!   .+�5b      �� ��6    4  .MahApps.Metro.IconPacks.MaterialImageExtension  
   Kind$
 Target=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush# [  �� 5c      #��[  6$   $��校正��6Y   5d      5e      �� 5f      $	��120��5g      $
��10,5q�5h      ��5i      �� *CancelCommand+.��5j      �� $  
Horizontal=�6!   .+�5k      �� ��6      $
 Cancel=�# [  �� 5l      #��[  6$     s_Cancel#��[ 6Y   5m      5n      5o      5p      5q      5r      