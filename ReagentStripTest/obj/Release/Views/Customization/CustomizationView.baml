   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MasTools.Views.CustomizationMasTools.Views.Customization  4    .MasTools.Views.Customization.CustomizationView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   8local*clr-namespace:MasTools.Views.Customization   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006     � 	Resources   +  'MahApps.Styles.TextBlock.AutoCollapsing �� BasedOn �� 
TargetType �� Property �� Value  MahApps.Styles.ComboBox  MahApps.Styles.TextBox��%T  '��       7   '�� w     7  '�� �     7  �� 8   " ��.���  � ��$ 20(���  � ��$	 5,3q���  � ��$ 
UltraLight"���  � ��$ Center=��� 8   " ��.���  � ��$ 20(���  � ��$	 5,3q���  � ��$	 280���� 8   " ��.���  � ��$ 20(���  � ��$	 5,3q���  � ��$ 
UltraLight"�.���� .��]� .+��� $��Center=�$	��Top=�$
��Bold"�$`��Z注意：如果将定制型号留空，点击写入后会将仪器型号置为默认值。��� $��Center=�$��Center=� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��仪器型号���� $��0��$��1�� �� DisplayMemberPath$ 	ModelName�� �� 
IsEditable . 	 �� SelectedValuePath$	 	ModelName������ **(CustomizationDataProvider.Customizations+
 �� Text
 �� *CustomizedModelName+�� $��1��$��0��$��Logo 路径���� $��1��$��1�� �� TextWrapping$
 Wrap=����� *LogoFilePath+�� -    ��Name$ SelectLogoFile��$��1��$��2�� ) %MahApps.Styles.Button.Calendar.Header#��[ .����  �� Orientation$ 
Horizontal=�.+��� $��3q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ 
FolderOpen=��� $��14(�
���   �$��浏览���� $��2��$��0��$��3��$	��320��$	��320��$��Center=����� *Logo+A� $��4��$��0��$��3��.+��� $
��10,3q����� *ReadCommand+.���� $ 
Horizontal=�.+��� ��  $
 Read=�  MahApps.Brushes.Accent8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush# [ �� $��从仪器读取���� $
��10,3q����� *WriteCommand+.���� $ 
Horizontal=�.+��� ��  $ Pencil=�# [ �� $��写入仪器��