   M S B A M L   `   `   `  ���� M  IReagentStripTest, Version=2.0.925.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:ReagentStripTest.ViewsReagentStripTest.Views  0    *ReagentStripTest.Views.LanguageSettingView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   2local$clr-namespace:ReagentStripTest.Views   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  $	��350��  � WindowStartupLocation$  CenterOwner=� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � SizeToContent$ Height=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��10q� �� SelectionMode$ Single=����� *
Cultures+ �� SelectedItem �� *	Culture+���� .R�	� $	��200��$��50��
������ $��12(�$��	UltraBold"����� *Value+ d� 	IsChecked ��  �� Path$ 
IsSelected�	 �� RelativeSource	 �� 
 �� AncestorType"
 ������ .R�A�  A� Columns$ 1���� $��1��$��Right=� �� Orientation$ 
Horizontal=�.+��� $	��100��$��5q����� *AcceptCommand+.���� $ 
Horizontal=�.+��� $��15��$��15��$��3q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� $��3q�
���   �   s_Accept#���   �� $	��100��$��5q� �� IsCancel . .���� $ 
Horizontal=�.+��� $��15��$��15��$��3q���  $ Close=�
 �  ���� $��3q�
���   �  s_Cancel#���  