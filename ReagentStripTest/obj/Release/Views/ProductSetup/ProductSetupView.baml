   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MasTools.Views.ProductSetupMasTools.Views.ProductSetup  2    ,MasTools.Views.ProductSetup.ProductSetupView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^ ZCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  :d2http://schemas.microsoft.com/expression/blend/2008  g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   7local)clr-namespace:MasTools.Views.ProductSetup   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    � 	Resources   �� 
TargetType �� Property �� Value   MahApps.Styles.TextBlock �� BasedOn  MahApps.Brushes.Accent2  MahApps.Styles.TextBox  MahApps.Styles.ComboBox  MahApps.Brushes.Accent �� 
TargetType ��Name �� CornerRadius ��Name	 J� Opacity
 �� Orientation ��Name
 � Tag �� TextAlignment �� Triggers P� Property P� Value �� 
TargetName��%�  '��       '�� %     7   '�� �     7  '��     7  '�� Z    �� " ��.���  � ��$
 10,5q��� 8   " ��.���  � ��$ Center=���  � ��$ 3,5,3,3q���  � ��$ 20(���  � ��# �  �� 8   " ��.���  � ��$ Center=���  � ��$ 3q���  � ��$ 20(���  � ��$ Light"��� 8   " ��.���  � ��$ 20(���  � ��$ 3q���  � ��$	 200���� " ��.���  �@�FocusVisualStyle Z� ��  � ��$ 20(���  �@J�Opacity$ 1Q���  � ��# �  ��  � ��$ Center=���  � ��$ Stretch=���  � ��$ 微软雅黑)���  � ��$ Stretch=���  � ��$ 	ExtraBold"���  � �� �� *DisplayName+��  � �� �� " ��.R�� $��1,0,0,0q�
������ .+��� $ 	PART_GRID��
�������$ 	10,10,0,0���� $ x��$
��25,5q�$��Center=�
������ #	 z	 $
 
Horizontal=�.+��� $ Content��$��Center=�$��Center=�$��Header���� $��0,0,0,0q�$��Center=�#��z��#��z��#��z $
 Left=� P�  �@J�IsMouseOver . . ��� $ 	PART_GRID��# �   � ����  � ��
 �����P�  �@��
IsSelected . . ���  � ��
 ������� $ 	PART_GRID��# �   � ����  �@J�Opacity$	 0.8Q�.��� $��0�� � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��5q�
�������
�������$��1q�$ 10��  DropShadowEffect J� Effect# [ ._��� $��10q�.+��� $��内部型号:���� {�.  ���� *InternalModels+ �� SelectedItem �� *InternalModel+���� .R��� $��3q�����   StringTranslateConverter �� 	Converter# [ �� $��定制型号:���� -    ��Name$ DisplayModel��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ 请输入定制型号���� $��SN码:���� -   $ SN��$ 请输入SN码���� $��硬件版本:���� -   $ HardwareVersion��$ 请输入硬件版本号���� $��默认语言:���� $ 请选择默认语言������ *	Languages+ �� *InstrumentLanguage+���� .R��� $��3q����� # [ A� $��1�� A� Columns$ 1��.+��� $��70��$
��0,10q����� *ReadProductInfoCommand+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$
 Read=��� $	��5,0q�$��Center=�$��20(�$��读取产品信息���� $��70��$
��0,10q����� *WriteProductInfoCommand+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���  $	 Cog=��� $	��5,0q�$��Center=�$��20(�$��设置产品信息���� $��1��$��5q�.@��� ���� $
 
Horizontal=�.+��� $��3q���  $ ChartBellCurve=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 ������� $��3q�
�������$��光栅感应器系数��.��� $��1�� �� �� .+��� ���� $��16(�$��45°光栅感应器系数��#���  .��� $��10q� � RowDefinitions �� $��* ��� $
��Auto �.+��� $��0��  Caliburn.Micro.View   Model �� *GratingCoefficientsModel_45+A� $��1�� A� Rows$ 1��.+��� $��50��$��5q����� *+)ImportGratingCoefficientsFromExcelCommand+���� *GratingCoefficientsModel_45+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���  $ 
FileImport=��� $	��5,0q�$��Center=�$��20(�$��从Excel导入���� $��50��$��5q����� *" SetGratingCoefficients_45Command+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���  $ CogTransfer=��� $	��5,0q�$��Center=�$��20(�$��设置光栅系数���� $��1������ $��16(�$��15°光栅感应器系数��#���  .��� $��10q� �� $��* ��� $
��Auto �.+��� $��0�� �� *GratingCoefficientsModel_15+A� $��1��$ 1��.+��� $��50��$��5q����� *+)ImportGratingCoefficientsFromExcelCommand+���� *GratingCoefficientsModel_15+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���  $ 
FileImport=��� $	��5,0q�$��Center=�$��20(�$��从Excel导入���� $��50��$��5q����� *" SetGratingCoefficients_15Command+.���� $
 
Horizontal=�.+��� $��30��$	��5,0q���  $ CogTransfer=��� $	��5,0q�$��Center=�$��20(�$��设置光栅系数���� ���� $
 
Horizontal=�.+��� $��3q���  $ AspectRatio=�
 ������� $��3q�
�������$��白板参数��.��� $
��20,5q� �� $
��Auto ��� $��* ��� $
��Auto �.+��  �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��14(�$��白板编号���� $��1��$��14(����� *Whiteboard.SN+�� $��2��$��14(�$��内部编号���� $��3��$��14(����� *Whiteboard.InnerNumber+�� $��1�� �� *
Whiteboard+A� $��2��$ 1��.+��� $��50��$��5q����� *&$ImportWhiteboardDataFromExcelCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,0q���  $ 
FileImport=�# [ �� $	��5,0q�$��Center=�$��20(�$��从Excel导入���� $��50��$��5q����� *SetWhiteboardDataCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,0q���  $ CogTransfer=�# [ �� $	��5,0q�$��Center=�$��20(�$��设置白板参数��