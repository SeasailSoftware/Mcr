   M S B A M L   `   `   `  ����M  IReagentStripTest, Version=2.0.925.1, Culture=neutral, PublicKeyToken=nullS.clr-namespace:ReagentStripTest.Views.Calibrate ReagentStripTest.Views.Calibrate  5      9    3ReagentStripTest.Views.Calibrate.WhiteCalibrateView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      <local.clr-namespace:ReagentStripTest.Views.Calibrate   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      .��5      � 
�������.+�5   
   �� .+�5      �� $
��10,3q�6     �� Orientation$  
Horizontal=�6'   .+�5      �� $��15��5      $��15��5      $��3q�5      
������� �� CornerRadius$ 10��5      �� 5      $��0��5      $��3q�5      $��Center=�5      $��14(�5      $
��Bold"�5      
�������$��	黑校正��5      �� 5      $��15��5      $	��5,3q�5      ��5      4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � �5!      �� 5"      $
��10,3q�6   $  
Horizontal=�6'   .+�5#      �� $��15��5$      $��15��5%      $��3q�5&         MahApps.Brushes.Accent#��[  5'      $ 10��5(      �� 5)      $��0��5*      $��5q�5+      $��Center=�5,      $��14(�5-      $��DemiBold"�5.      #��[  5/      $��	白校正��50      51      �� 52      $	��300��53      $��5q�54      
�������$ 10��56        DropShadowEffect J� Effect# [ 57      ._�58      �� $��20q�59      $��Center=�5:      $��Center=�5;      $��16(�5<      
�������$H��B请把白板放置测量口,然后点击[校准]按钮开始校准��5>      $
��Wrap=�5?      5@      �� 5A      $	��300��5B      $	��220��5C      $��10q�5D      $&�� ../../Images/white_calibrate.jpg��5E      $
��Fill=�5F      �� 5G      -   5         ��Name$ ErrorMsg��5H      $��5q�5I      $��Center=�5J      $��Center=�5K      $��15(�5L      
���  ��5M      �� 5N      y�6   �� *IsCalibrating+   BooleanToVisibilityConverter �� 	Converter# [ .+�5O      �� $��Center=�5P      $��Center=�5Q      #���   5R        s_CalibratingPrompt#���  5S      -  'MahApps.Metro.Controls.MetroProgressBar  5T      $��3q�5U      #���   5V       � IsIndeterminate 5W      �� *IsCalibrating+5X      �� 5Y      $��1��5Z      $��5q�5[      $��Center=�5\      $  
Horizontal=�5]       � 	Resources 5^        MahApps.Styles.TextBlock	 �� BasedOn
 �� 
TargetType �� Property �� Value��%>  '��       7  7   '�� �     �� 5_      8	   "
 ��6P   .�5`      ��  � ��$ 15(�65   �� 5a       � ��8  67   �� 5b       � ��$
 Bold"�67   �� 5c       � ��$ 5q�63   5d      �� 5e      "
 ��6   .�5f      ��  � ��$ 20��63   �� 5g       � ��$ 20��62   5h      5i      .+�5j      �� $	��120��5k      $
��10,5q�5l      ��5m      �� *CalibrateCommand+.��5n      �� $  
Horizontal=�6!   .+�5o      �� ��6      $ Target=�# [  �� 5p      #��[  6$   $��校正��6Y   5q      5r      �� 5s      $	��120��5t      $
��10,5q�5u      ��5v      �� *CancelCommand+.��5w      �� $  
Horizontal=�6!   .+�5x      �� ��6      $ Cancel=�# [  �� 5y      #��[  6$     s_Cancel#��[ 6Y   5z      5{      5|      5}      5~      5      