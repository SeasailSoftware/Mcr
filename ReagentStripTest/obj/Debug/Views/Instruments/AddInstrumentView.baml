   M S B A M L   `   `   `  ����M  IReagentStripTest, Version=2.0.925.1, Culture=neutral, PublicKeyToken=nullW0clr-namespace:ReagentStripTest.Views.Instruments"ReagentStripTest.Views.Instruments  5      :    4ReagentStripTest.Views.Instruments.AddInstrumentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      >local0clr-namespace:ReagentStripTest.Views.Instruments   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	        � Title$  添加设备��5
      $	��300��5      $	��165��5       � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5       � 	Resources 5         MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property �� Value  MahApps.Styles.TextBox��%�   '��       7   '�� v     7  �� 5   
   8   " ��6D   .�5      ��  � ��$ Center=�62   �� 5       � ��$	 5,3q�6'   5      �� 5   
   8   " ��6B   .�5      ��  � ��$	 5,3q�6'   5      5      .��5      � $��20,10q�6   	 � ColumnDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   
 � RowDefinitions
 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   .+�5$   
   �� $��设备名称��6   �� 5%   
   $��1��6   ��6"   �� *Name+�� 5&   
   $��1��6   $��SN��6!   �� 5'   
   $��1��5(      $��1��5)      ��5*      �� *SN+A� 5+   
   $��2��5,      $��2��5-      $��Right=�5.      .+�5/      �� $��90��50      $	��5,3q�51      ��52      �� *AcceptCommand+.��53      ��  �� Orientation$ 
Horizontal=�6   .+�54      .  (MahApps.Metro.IconPacks.PackIconMaterial  $��20��55      $��20��56      $��3q�57         Kind$ Check=�58      �� 59      $��接受��6    5:      5;      �� 5<      $	��5,3q�6    �� IsCancel . .��5=      �� $ 
Horizontal=�6   .+�5>        $��20��5?      $��20��5@      $��3q�5A      $ Close=�5B      �� 5C      $��取消��6    5D      5E      5F      5G      5H      