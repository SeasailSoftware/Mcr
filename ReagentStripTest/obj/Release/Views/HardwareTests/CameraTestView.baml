   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MasTools.Views.HardwareTestsMasTools.Views.HardwareTests  1    +MasTools.Views.HardwareTests.CameraTestView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^ ZCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  :d2http://schemas.microsoft.com/expression/blend/2008  g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   8local*clr-namespace:MasTools.Views.HardwareTests   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    � 	Resources     MahApps.Styles.TextBlock �� BasedOn �� 
TargetType �� Property �� Value  MahApps.Styles.DataGrid&   System.Windows.Controls.DataGrid  MahApps.Brushes.Accent �� 
TargetType ��Name �� CornerRadius ��Name	 J� Opacity
 �� Orientation ��Name
 � Tag �� TextAlignment �� Triggers P� Property P� Value �� 
TargetName��%  '��       7   '  q     7  '�� �     '�� �     �� 8   " ��.���  � ��$ Center=���  � ��$ 20(���  � ��$	 5,3q���  � ��
 ������� 8   "  �� " ��.���  � ��
 ������� " ��.���  �@�FocusVisualStyle Z� ��  � ��$ 14(���  �@J�Opacity$ 1Q���  � ��# �  ��  � ��$ Center=���  � ��$ Stretch=���  � ��$ 微软雅黑)���  � ��$ Stretch=���  � ��$ 	ExtraBold"���  � �� �� *DisplayName+��  � �� �� " ��.R�� $��1,0,0,0q�
������ .+��� $ 	PART_GRID��
�������$ 	10,10,0,0���� $ x��$
��25,5q�$��Center=�
������ #	 z	 $
 
Horizontal=�.+��� $ Content��$��Center=�$��Center=�$��Header���� $��0,0,0,0q�$��Center=�#��z��#��z��#��z $
 Left=� P�  �@J�IsMouseOver . . ��� $ 	PART_GRID��# �   � ����  � ��
 �����P�  �@��
IsSelected . . ���  � ��
 ������� $ 	PART_GRID��# �   � ����  �@J�Opacity$	 0.8Q�.��� .+��� $
��10,5q�.@��� $��中心定位��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� $
 
Horizontal=�.+��� $	��5,3q�$��模式切换��	� -    	�Name$ RadioButtonPreview��$
��10,3q�$��0��$��预览模式 ������ *SetCameraModeCommand+	� -   $ RadioButtonSparkle��$
��10,3q�$��1��$��效果色模式������ *SetCameraModeCommand+� $��1��.+��� $��20q�$��预览模式��y���  �� ElementName$ RadioButtonPreview�� �� Path$ 	IsChecked�   BooleanToVisibilityConverter �� 	Converter# [ .��� .+��� $��Center=�$��Center=�.+��� $	��240��$	��240��
���   �.+�� .+��� $��30��$��30��$��Center=�$��Center=� K� ZIndex$ 1����4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$
 Plus=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 �  ���� $	��240��$	��240��$��Uniform=����� *PreviewImage+�� $
��0,10q����� *GetPreviewImageCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,3q���  $ Image=�# [ �� $	��5,3q�$��获取图像���� $
��0,10q�$��Center=�$
 
Horizontal=�.+��� $��X:��*  $MahApps.Metro.Controls.NumericUpDown  $��80��$��Center=�   Maximum$ 65535Q�   Minimum$ 0Q�   Value �� *	Canvas0_X+�� $��1��$
��10,3q�$��Y:��  $��80��$��Center=�$ 65535Q�$ 0Q� �� *	Canvas0_Y+�� $
��0,10q����� *SetPreviewCameraCenterCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,3q���  $
 Play=�# [ �� $	��5,3q�$��写入仪器���� $��1��$��20q�$��效果色模式��y��� $ RadioButtonSparkle��$ 	IsChecked�# [ .��� .+��  � AllowsTransparency . $��LeftToRight=�$��Mouse=�$
��Fade=� � 	StaysOpen . ���� *PopupIsOpen+.���� $��90��$��30��$��10(����� *CurrentPoint+�� $��Center=�$��Center=�.+��� $	��240��$	��240��$ 1��  Caliburn.Micro.Message   Attach$� �[Event MouseMove]=[Action MouseMove($eventArgs)];                                 [Event MouseLeave]=[Action MouseLeave($eventArgs)];                                 [Event MouseDown]=[Action MouseDown($eventArgs)]��
���   �.+��� $	��240��$	��240��$
��Fill=����� *Image+�� $
��0,20q�$��Center=�$
 
Horizontal=�.+��� $��X:���� $��80��$��Center=�  �� 
IsReadOnly  . ���� *	Canvas1_X+�� $��1��$��Y:���� $��1��$��1��$��80��$��Center=�  . ���� *	Canvas1_Y+�� $
��0,10q����� *GetSparkleImageCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,3q���  $ Image=�# [ �� $	��5,3q�$��获取图像���� $
��0,10q����� *SetSparkleCameraCenterCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,3q���  $
 Play=�# [ �� $	��5,3q�$��写入仪器���� $
��0,10q����� *SaveSparkleImageCommand+.���� $
 
Horizontal=�.+��� $��30��$��30��$	��5,3q���  $ ContentSave=�# [ �� $	��5,3q�$��保存图像���� $��积分时间参数设置��.���� .+��� $��30q�$��Center=�$��Center=�$��30(�$��50%灰度卡测试��A� $��Center=�$��Center=�! A� Rows$! 1��.+��� .+��� $��20,50q�$��0�� ) %MahApps.Styles.Button.Calendar.Header#��[ ���� *IntegralTimeCommand+.���� .+��� $	��150��$	��150����  $ WindowMinimize=�
 �   ��� $��5q�$��Center=�$��Center=�$��	最小值���� $��50��$��50������ *BlackVelvetStatus+  BooleanToImageConverter# [ �� .+��� $��20,50q�$��1��#��[ ���� *IntegralTimeCommand+.���� .+��� $	��150��$	��150����  $ CursorDefault=�
 ������� $��5q�$��Center=�$��Center=�$��	默认值���� $��50��$��50������ *MetalCardStatus+# [ �� .+��� $��20,50q�$��2��#��[ ���� *IntegralTimeCommand+.���� .+��� $	��150��$	��150����  $ WindowMaximize=�
 ������� $��5q�$��Center=�$��Center=�$��	最大值���� $��50��$��50������ *GrayCardStatus+# [ �� $��效果色系数调整��.���  �� $��* ��� $
��Auto �.+�� $��5q�" � ColumnDefinitions" �� $
��Auto ��� $��* �.+��� $��标准效果色板��.���  �� $��* ��� $
��Auto �.+��� ���� *SwatchDatas+# �� SelectedItem# �� *
SwatchData+���� .R��� $
��10,5q�$��18(����� *
SwatchName+A� $��1��$
��5,10q�$ A� Columns$$ 1��.+��� $��5q����� *StandardSettingCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $ StandardDefinition=�# [ �� $��16(�$��基准设置���� $��5q����� *MeasureCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $ TapeMeasure=�# [ �� $��16(�$��测  量���� $��5q����� *RefreshDataCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $ Refresh=�# [ �� $��16(�$��刷  新���� $��5q����� *WriteSparkleFactorsCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $
 Play=�# [ �� $��16(�$��写入仪器���� $��5q����� *OpenFileCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $ 
FolderOpen=�# [ �� $��16(�$��打开文件���� $��5q����� *SaveFileCommand+.���� $
 
Horizontal=�.+��� $��20��$��20����  $ ContentSave=�# [ �� $��16(�$��保存数据��� $��1��.+��� .���� .+��� $��	Sparkle_I��.���� .+�  $��0��$��5q�%   AutoGenerateColumns% .  &   CanUserAddRows& .  $��12(����� *SITableItems+'   Columns' 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn(   Width5  /System.Windows.Controls.DataGridLengthConverter$( * )   Header$) 序号��*   
IsReadOnly* . 1	  +System.Windows.Controls.DataGridBoundColumn+ 	  Binding+ �� *Index+  $( * $) 色板名称��* . + �� *
SwatchName+  $( * $) -45(1#)��+ �� *[0]+  $( * $) -45(std)��* . + �� *Standard[0]+  $( * $) -30(2#)��+ �� *[1]+  $( * $) -30(std)��* . + �� *Standard[1]+  $( * $) -15(3#)��+ �� *[2]+  $( * $) -15(std)��* . + �� *Standard[2]+  $( * $) 15(5#)��+ �� *[3]+  $( * $) 15(Std)��* . + �� *Standard[3]+  $( * $) 45(6#)��+ �� *[4]+  $( * $) 45(Std)��* . + �� *Standard[4]+  $( * $) 80(7#)��+ �� *[5]+  $( * $) 80(Std)��+ �� *Standard[5]+�  �� $
��Auto ��� $��* �" �� $
��Auto ��� $��* �.+��� $��0��$��1������ *SparkleColumnHeaders+���� .R�A� $! 1���� $��1������ *SpakleRowHeaders+���� .R��� $��50��$��Center=�$��Center=����� ���� .R�A� $$ 1���� $��1��$��1��$��12(�$��Light"�
���   ����� *Sparkle_I_Factors+, �� Mode$, OneWay=����� - � 	Resources-   MahApps.Styles.TextBox��%�   '��       '�� 9     7  �� " ��.���  �@��BaselineAlignment$ Center=��� 8   " ��.���  � ��$ 3q���  � ��
 ��  ���  � ��$
 Bold"�.R�A� $! 1��.+���   . ���� *Coefficients[5]+��   . ���� *Coefficients[4]+��   . ���� *Coefficients[3]+��   . ���� *Coefficients[2]+��   . ���� *Coefficients[1]+��   . ���� *Coefficients[0]+�� . �� SelectedIndex. �� *
Polynomial+.@��� $��2次多项式���� $��3次多项式������ .R�A� $$ 1��$! 6���� $��	Sparkle_A��.���� .���� .+�  $��5q�% .  & .  $��12(����� *SATableItems+'   $( * $) 序号��* . + �� *Index+  $( * $) 色板名称��* . + �� *
SwatchName+  $( * $) -45(1#)��+ �� *[0]+  $( * $) -45(std)��* . + �� *Standard[0]+  $( * $) -30(2#)��+ �� *[1]+  $( * $) -30(std)��* . + �� *Standard[1]+  $( * $) -15(3#)��+ �� *[2]+  $( * $) -15(std)��* . + �� *Standard[2]+  $( * $) 15(5#)��+ �� *[3]+  $( * $) 15(Std)��* . + �� *Standard[3]+  $( * $) 45(6#)��+ �� *[4]+  $( * $) 45(Std)��* . + �� *Standard[4]+  $( * $) 80(7#)��+ �� *[5]+  $( * $) 80(Std)��+ �� *Standard[5]+�  �� $
��Auto ��� $��* �" �� $
��Auto ��� $��* �.+��� $��0��$��1������ *SparkleColumnHeaders+���� .R�A� $! 1���� $��1������ *SpakleRowHeaders+���� .R��� $��50��$��Center=�$��Center=����� ���� .R�A� $$ 1���� $��1��$��1��$��12(�$��Light"�
���   ����� *Sparkle_A_Factors+$, OneWay=����� - ��%�   '��       '�� 9     7  �� " ��.���  �@��BaselineAlignment$ Center=��� 8   " ��.���  � ��$ 3q���  � ��
 ��  ���  � ��$
 Bold"�.R�A� $! 1��.+���   . ���� *Coefficients[5]+��   . ���� *Coefficients[4]+��   . ���� *Coefficients[3]+��   . ���� *Coefficients[2]+��   . ���� *Coefficients[1]+��   . ���� *Coefficients[0]+�� . �� *
Polynomial+.@��� $��2次多项式���� $��3次多项式������ .R�A� $$ 1��$! 6���� $��Coarse��.���� .���� .+�  $��2��% .  & .  $��12(����� *CoarseTableItems+'   $) 序号��* . + �� *Index+  $( * $) 色板名称��* . + �� *
SwatchName+  $( * $) 
Coarse(8#)��+ �� *[0]+  $( * $) Coarse(Std)��* . + �� *Standard[0]+$, OneWay=��  �� $
��Auto ��� $��* �" �� $
��Auto ��� $��* �.+��� $��0��$��1������ *SparkleColumnHeaders+���� .R�A� $! 1���� $��1��$��1��$��12(�$��Light"�
���   ����� *Coarse_Factors+$, OneWay=����� - ��%�   '��       '�� 9     7  �� " ��.���  �@��BaselineAlignment$ Center=��� 8   " ��.���  � ��$ 3q���  � ��
 ��  ���  � ��$
 Bold"�.R�A� $! 1��.+���   . ���� *Coefficients[5]+��   . ���� *Coefficients[4]+��   . ���� *Coefficients[3]+��   . ���� *Coefficients[2]+��   . ���� *Coefficients[1]+��   . ���� *Coefficients[0]+�� . �� *
Polynomial+.@��� $��2次多项式���� $��3次多项式������ .R�A� $$ 1��$! 6��