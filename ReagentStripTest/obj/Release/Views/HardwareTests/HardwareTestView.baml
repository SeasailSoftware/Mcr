   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MasTools.Views.HardwareTestsMasTools.Views.HardwareTests  3    -MasTools.Views.HardwareTests.HardwareTestView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^ ZCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  :d2http://schemas.microsoft.com/expression/blend/2008  6local*clr-namespace:MasTools.Views.HardwareTests  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  .���   � ColumnDefinitions  �� $
��Auto �$	��200���� $��* �.+���    DropShadowEffect J� Effect# [  
��������� 
������ ���� *Items+ �� SelectedItem �� *
ActiveItem+���� .R��� $��Center=����� *DisplayName+����  �� 
TargetType" ��.���  �� Property � �� �� Value
 ���� ��  � ��$ 18(���  � ��$ 60����  � �� ��  �� 
TargetType" ��.R��  �Name$ 	Container��  �� $��* ��� $
��Auto �.+��� $��2��
�������$��1q��� $
��20,5q�$��Center=���  ��Name$ 
RightSider��$��1��$��5��$��Stretch=�
��� ���$y�Hidden=�	 �� Triggers	 P� 
 P� Property
 �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ 	Container��  MahApps.Brushes.Accent4# �   � ���� $ 
RightSider�� � y�$ Visible=��� $ 
RightSider�� � ��
 � �����  � ��
 �����P� 
 �@��
IsSelected . . ��� $ 	Container��  MahApps.Brushes.Accent2# �   � ���� $ 
RightSider�� � y�$ Visible=��� $ 
RightSider�� � ��
 � �����  � ��
 ������� $��1��  Caliburn.Micro.View   Model �� *
ActiveItem+