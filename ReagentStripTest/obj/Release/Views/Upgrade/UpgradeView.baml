   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=null?$clr-namespace:MasTools.Views.UpgradeMasTools.Views.Upgrade  (    "MasTools.Views.Upgrade.UpgradeView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   2local$clr-namespace:MasTools.Views.Upgrade   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    � 	Resources     MahApps.Styles.Label �� BasedOn �� 
TargetType �� Property �� Value  MahApps.Styles.TextBox  MahApps.Styles.Button��%U  '��       7   '�� W     7  '�� �     7  '�� �     �� 8   " ��.���  � ��$ 20(���  � ��$ 5q���  � ��$ Center=��� 8   " ��.���  � ��$ 20(���  � ��$ 5q��� 8   " ��.���  � ��$ 5q���  � ��$	 150����  � ��$ 50����  � ��$ 16(��� " ��.���  � ��$ 10q�.���  � RowDefinitions �� $��* ��� $
��Auto �.+��� $��固件升级��.��� $��20,10q� �� $
��Auto ��� $��* ��� $
��Auto �.+��  � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��0��$��固件��
��������� $��0��$��1��$��3q�$��Center=�*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$5 /请打开固件升级文件(.bin或.dfu或.hex)�� �� 
IsReadOnly . ���� 	 �� Path$	 FirmwarePath��� $��0��$��2��$��5q� ) %MahApps.Styles.Button.Calendar.Header#��[ 
 � ToolTip$
 浏览..������ *BrowseFirmwareFileCommand+.���� $��25��$	��5,0q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ FolderOpenOutline=�  MahApps.Brushes.Accent8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush# [ �� $��1��$��0��$��2��
�������$��1q� �� CornerRadius$ 8��._��� $��20q�$��16(�  MahApps.Styles.ListView#��[ ���� $	 
DfuDevices� �� SelectedIndex �� $	 CurrentUpgrading� �� Mode$ OneWay=� �� View �� .P��� $��编号�� �� DisplayMemberBinding �� $	 ID��� $��描述�� �� $	 Description��� $��路径�� �� $
	 Path��� $��2��$��2��$��Right=� �� Orientation$ 
Horizontal=�.+��� $��5q����� *RefreshCommand+.���� $ 
Horizontal=�.+��� $��20��$	��5,0q���  $ FileRefresh=�# [ �� $	��5,0q�$��Center=�#��[ $��刷新���� $��5q����� *UpgradeCommand+.���� $ 
Horizontal=�.+��� $��20��$	��5,0q���  $ ArrowUpBoldCircle=�# [ �� $	��5,0q�$��Center=�#��[ $��升级���� $��1��$��下载资源��.���� $��20,10q�.+��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��固件��
��������� -    ��Name$ ResourceFile��$��1��$��3q�$��Center=�$! 请打开资源文件(.bin)�� . �� $��0��$��2��$��5q�#��[ $
 浏览..������ *BrowseResourceFileCommand+.���� $��25��$	��5,0q���  $ FolderOpenOutline=�# [ �� $��3��$��5q����� *DownloadResourceCommand+.���� $ 
Horizontal=�.+��� $��20��$	��5,0q���  $ Download=�# [ �� $	��5,0q�$��Center=�#��[ $��下载资源��