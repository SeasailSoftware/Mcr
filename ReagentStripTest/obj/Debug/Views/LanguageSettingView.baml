   M S B A M L   `   `   `  ����M  IReagentStripTest, Version=2.0.925.1, Culture=neutral, PublicKeyToken=null?$clr-namespace:ReagentStripTest.ViewsReagentStripTest.Views  5      0    *ReagentStripTest.Views.LanguageSettingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      2local$clr-namespace:ReagentStripTest.Views   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      $	��350��5
        � WindowStartupLocation$  CenterOwner=�5       � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � SizeToContent$ Height=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $��10q�5       �� SelectionMode$ Single=�5      ��5      �� *
Cultures+ �� SelectedItem 5      �� *	Culture+��5      �� 5      .R�5      	� $	��200��5      $��50��5      
������ $��12(�5"      $��	UltraBold"�5#      ��5!      �� *Value+ d� 	IsChecked 5$      ��  �� Path$ 
IsSelected�	 �� RelativeSource	 �� 
 �� AncestorType"
 ��5%      ��5'      �� 5(      .R�5)      A�  A� Columns$ 1��6"   5*      5,      �� 5-   
   $��1��5.      $��Right=�5/       �� Orientation$ 
Horizontal=�50      .+�51      �� $	��100��52      $��5q�53      ��54      �� *AcceptCommand+.��55      �� $ 
Horizontal=�6   .+�56      �� $��15��57      $��15��58      $��3q�59      ��5:      4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� 5<      $��3q�5=      
���   �   s_Accept#���   5?      5@      5A      �� 5B      $	��100��5C      $��5q�5D       �� IsCancel . .��5F      �� $ 
Horizontal=�6   .+�5G      �� $��15��5H      $��15��5I      $��3q�5J      ��5K        $ Close=�
 �  ���� 5M      $��3q�5N      
���   �  s_Cancel#���  5P      5Q      5R      5S      5T      5U      