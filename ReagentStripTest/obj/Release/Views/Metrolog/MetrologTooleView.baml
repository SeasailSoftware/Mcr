   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MasTools.Views.MetrologMasTools.Views.Metrolog  /    )MasTools.Views.Metrolog.MetrologTooleView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   3local%clr-namespace:MasTools.Views.Metrolog   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006    � 	Resources   �� 
TargetType2  ,MahApps.Metro.Controls.HeaderedControlHelper �� Property �� Value   IndicationTableItemStyle ' #MahApps.Styles.DataGridColumnHeader �� BasedOn=  7System.Windows.Controls.Primitives.DataGridColumnHeader �Name � RowDefinitions � ColumnDefinitions ��Name	 � DataContext
 �� Triggers P� Property P� Value �� 
TargetName  MahApps.Brushes.Accent �� 
TargetType �� CornerRadius ��Name J� Opacity �� Orientation ��Name
 � Tag �� TextAlignment��%�  '��       &	  J     7  '�� �    �� " ��.���  �@ HeaderFontSize$ 14Q���  � ��
 �   ��� 8   "  .���  � �� �� .R�� $ _Grid�� �� $��* ��� $��* � �� $��40 ��� $��40 ��� $��60 �.+��� $ _Border1��$��3��
�������$��0.5,1,0.5,0.5q�._��� $��Center=�#��z	 �� $ _Border2��$��1��$��0��
�������$��0.5,1,0.5,0.5q�._��� $��Center=�$��下限���� $ _Border3��$��1��$��1��
�������$��0.5,1,0.5,0.5q�._��� $��Center=�$��上限���� $ _Border4��$��1��$��2��
�������$��0.5,1,0.5,1q�._��� $��Center=�$	��值��
 P�  �@J�IsMouseOver . . ��� $ _Grid�� � ��
 ������� " ��.���  �@�FocusVisualStyle Z� ��  �@J�Opacity$ 1Q���  � ��# �  ��  � ��$ Center=���  � ��$ Stretch=���  � ��$ 微软雅黑)���  � ��$ Stretch=���  � �� �� *DisplayName+��  � �� �� " ��.R�� $��1,0,0,0q�
������ .+��� $ 	PART_GRID��
�������$ 	10,10,0,0���� $ x��$
��25,5q�$��Center=�
������ # z $ 
Horizontal=�.+��� $ Content��$��Center=�$��Center=�$��Header���� $��0,0,0,0q�$��Center=�#��z��#��z��#��z $
 Left=�
 P�  �@J�IsMouseOver . . ��� $ 	PART_GRID��# �   � ����  � ��
 �����P�  �@��
IsSelected . . ���  � ��
 ������� $ 	PART_GRID��# �   � ����  �@J�Opacity$	 0.8Q�.���  �� $
��Auto ��� $��* �.+�o� $��0��.@�m�  
 s_File#���   m� Icon �� $��15����4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$
 File=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush# [ .@�m�   
s_FileOpen#���   m� Command �� *OpenCommand+ �� $��15����  $ 	OpenInNew=�# [ m�  
 s_Save#���   �� *SaveCommand+ �� $��15����  $ ContentSave=�# [ m� $��	另存为�� �� *SaveAsCommand+ �� $��15����  $ ContentSaveOff=�# [ �� m� $��清除测量数据数据�� �� *ClearTestDatasCommand+ �� $��15����  $ NotificationClearAll=�# [ �� m�   s_Close#���   �� *CloseCommand+ �� $��15����  $ Close=�# [ m�   s_Instrument#���   �� $��15����  $ Monitor=�# [ .@�m� $��仪器信息�� �� *InstrumentInformationCommand+ /  )MahApps.Metro.IconPacks.MaterialExtension     Kind$ Information=��� m� $��连接仪器�� �� *ConnectCommand+   $ 
LanConnect=�m� $��断开连接�� �� *DisconnectCommand+   $ LanDisconnect=��� m� $��仪器校正�� �� *CalibrationCommand+�� m� $��自动生成测量数据�� �� *AutoMeasureCommand+m�   
s_Settings#���   �� $��15����  $	 Cog=�# [ .@�m�  	 s_MetewandsManagement#��� 	  �� *StandardsManagementCommand+m� $��计量测量参数配置�� �� *MeasurementSettingCommand+m� $��数据存储设置�� �� *DataStorageSettingCommand+m� $��报表�� �� $��15����  $ MicrosoftWord=�# [ .@�m� $��打印中文报告�� �� *PrintChineseReportCommand+m� $��打印英文报告�� �� *PrintEnglishReportCommand+m�  

 s_Help#��� 
  �� $��15����  $ 
HelpCircle=�# [ .@�m�   s_About#���   �� *AboutCommand+ �� $��15����  $ Information=�# [ �� $��1��$��0,12,0,0q�-  'MahApps.Metro.Controls.TabControlHelper   
Underlined$ TabPanel=�  MahApps.Styles.TabControl#��[ .@��� ���� $ 
Horizontal=�.+��� ��  $ InvertColors=�
 ������� $
��10,3q�$��Center=�$��16(�$
��Bold"�$��颜色计量��.���  �� $
��Auto �$	��250���� $��* ��� $
��Auto �$	��300��.+��� $��5q�$��标准色砖��.���  �� $��* ��� $
��Auto �.+��� $��5q����� *ColorSwatches+ �� SelectedItem �� *ColorSwatch+���� .R�	�  $  MahApps.Styles.RadioButton.Win10#��[  d� 	IsChecked ��  �� RelativeSource ��  �� AncestorType$ ListBoxItemL�  �� Path$  
IsSelected�.��� $	��220�� �� $
��Auto ��� $��35 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��3q�$��Center=�$��序号:���� $��1��$��3q�$��Center=����� *Index+�� $��2��$��3q�$��Center=�$��名称:���� $��3��$��3q�$��Center=����� *
SwatchName+�� $��4��$��15��$��3q����� *
IsPassed+  BooleanToImageConverter! �� 	Converter#! [ �� $��1��$��40��$��5q�#��[ ���� *$"MeasureColorIndicationErrorCommand+.���� $ 
Horizontal=�.+��� $��30����  $ TapeMeasure=�
 �   ��� $	��5,3q�$��Center=�$��16(�
�������$��测量示值误差���� $��1��$
��5,10q�$��示值误差��.���� $
��Auto=�$��Hidden=�.��� $��1��$
��5,10q� �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  "   AutoGenerateColumns" .  #   CanUserAddRows# .  $   CanUserReorderColumns$ .  %   CanUserSortColumns% .  &   SelectionMode$& Single=����� *'%ColorSwatch.IndicationErrorTableItems+'   Columns' 0  *System.Windows.Controls.DataGridTextColumn  ,	  &System.Windows.Controls.DataGridColumn( 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$
( Auto
 ) 	  Header$) 序号��1  +System.Windows.Controls.DataGridBoundColumn*   Binding* �� *Index+  $
( Auto
 $) 角度��* �� *Angle+  * �� *Lab0.L++ �� StringFormat$+ F2��) �� $��L*���� $��Palatino Linotype)�$�0��, K� Variants$, Superscript=�  * �� *Lab0.a+$+ F2��) �� $��a*���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *Lab0.b+$+ F2��) �� $��b*���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *XYZ0.Y+$+ F2��) �� $��Y*���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *XYZ0.x+$+ F4��) �� $��x*���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *XYZ0.y+$+ F4��) �� $��y*���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� $  dL�$+ F2��) �� $	��ΔL���� $��Palatino Linotype)�$�*��$, Superscript=�  * �� $  da�$+ F2��) �� $	��Δa���� $��Palatino Linotype)�$�*��$, Superscript=�  * �� $  db�$+ F2��) �� $	��Δb���� $��Palatino Linotype)�$�*��$, Superscript=�4  .System.Windows.Controls.DataGridTemplateColumn  - 	  HeaderStyle#- [  ) �� $	��ΔY���� $��Palatino Linotype)�$�*��$, Superscript=�.   CellTemplate. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dY.Lower+$+ F3���� $��1��$��Center=����� *
dY.Upper+$+ F3���� $��2��$��Center=����� *dY.IsPassed+/ �� Mode$/ OneWay=�   BooleanToForegroundConverter#! [ ���� *
dY.Value+$+ F4��  #- [  ) �� $	��Δx���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dx.Lower+$+ F3���� $��1��$��Center=����� *
dx.Upper+$+ F3���� $��2��$��Center=����� *dx.IsPassed+$/ OneWay=�#! [ ���� *
dx.Value+$+ F4��  #- [  ) �� $	��Δy���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dy.Lower+$+ F3���� $��1��$��Center=����� *
dy.Upper+$+ F3���� $��2��$��Center=����� *dy.IsPassed+$/ OneWay=�#! [ ���� *
dy.Value+$+ F4��  #- [  ) �� $	��ΔE���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dE.Lower+$+ F2���� $��1��$��Center=����� *
dE.Upper+$+ F2���� $��2��$��Center=����� *dE.IsPassed+$/ OneWay=�#! [ ���� *
dE.Value+$+ F2��  #- [  ) �� $	��ΔT���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dT.Lower+$+ F2���� $��1��$��Center=����� *
dT.Upper+$+ F2���� $��2��$��Center=����� *dx.IsPassed+$/ OneWay=�#! [ ���� *
dT.Value+$+ F2���� $��2��$
��5,10q�$��	重复性��.���  �� $��* ��� $
��Auto �.+�  " .  ���� *ColorRepeatabilityTableItems+'   $) 序号��* �� *Index+  $) 角度��* �� *Angle+  * �� $  dY.Value�$+ F4��) �� $	��ΔY���� $��Palatino Linotype)�$�*��$, Superscript=�0 	  	CellStyle0 ��   MahApps.Styles.DataGridCell# [ *  $System.Windows.Controls.DataGridCell"  .���  � �� �� *dY.IsPassed+$/ OneWay=�#! [   * �� $  dx.Value�$+ F4��) �� $	��Δx���� $��Palatino Linotype)�$� ��$, Superscript=�0 �� # [ "  .���  � �� �� *dx.IsPassed+$/ OneWay=�#! [   * �� $  dy.Value�$+ F4��) �� $	��Δy���� $��Palatino Linotype)�$� ��$, Superscript=�0 �� # [ "  .���  � �� �� *dy.IsPassed+$/ OneWay=�#! [   * �� $  dE.Value�$+ F2��) �� $	��ΔE���� $��Palatino Linotype)�$� ��$, Superscript=��� $��1��$��40��$��5q�#��[ ���� *" MeasureColorRepeatabilityCommand+.���� $ 
Horizontal=�.+��� $��30����  $ TapeMeasure=�
 �   ��� $	��5,3q�$��Center=�$��16(�
�������$��重复性测量���� ���� $ 
Horizontal=�.+��� ��  $	 Spa=�
 ������� $
��10,3q�$��Center=�$��16(�$
��Bold"�$��	效果色��.���� $��1��$��1q�$��效果色计量��.���  �� $
��Auto �$	��250���� $��* ��� $
��Auto �$	��350��.+��� $��5q�$��标准色砖��.���  �� $��* ��� $
��Auto �.+��� $��5q����� *SparkleSwatches+ �� *SparkleSwatch+���� .R�	� #��[  ��  �� $ ListBoxItemL�$  
IsSelected�.��� $	��250�� �� $
��Auto ��� $��35 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��3q�$��Center=�$��序号:���� $��1��$��3q�$��Center=����� *Index+�� $��2��$��3q�$��Center=�$��名称:���� $��3��$��3q�$��Center=����� *
SwatchName+�� $��4��$��15��$��3q����� *
IsPassed+#! [ �� $��1��$��40��$��5q�#��[ ���� *&$MeasureSparkleIndicationErrorCommand+.���� $ 
Horizontal=�.+��� $��30����  $ TapeMeasure=�
 �   ��� $	��5,3q�$��Center=�$��16(�
�������$��测量���� $��1��$
��5,10q�$��示值误差��.��� $��1��$
��5,10q� �� $��* ��� $
��Auto �.+�  " .  # .  $ .  % .  $& Single=����� *)'SparkleSwatch.IndicationErrorTableItems+'   $
( Auto
 $) 序号��* �� *Index+  $
( Auto
 $) 角度��* �� *Angle+  * �� *Sparkle0.SparklingIntensity+$+ F4��) �� $��Si���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *Sparkle0.SparklingArea+$+ F4��) �� $��Sa���� $��Palatino Linotype)�$�0��$, Superscript=�  * �� *Sparkle.SparklingIntensity+$+ F4��) �� $��Si���� $��Palatino Linotype)�$� ��$, Superscript=�  * �� *Sparkle.SparklingArea+$+ F4��) �� $��Sa���� $��Palatino Linotype)�$� ��$, Superscript=�  #- [  ) �� $
��ΔSi���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *	dSi.Lower+$+ F2���� $��1��$��Center=����� *	dSi.Upper+$+ F2���� $��2��$��Center=����� *dSi.IsPassed+$/ OneWay=�#! [ ���� *	dSi.Value+$+ F4��  #- [  ) �� $
��ΔSa���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *	dSa.Lower+$+ F2���� $��1��$��Center=����� *	dSa.Upper+$+ F2���� $��2��$��Center=����� *dSa.IsPassed+$/ OneWay=�#! [ ���� *	dSa.Value+$+ F4��  #- [  ) �� $	��ΔT���� $��Palatino Linotype)�$�*��$, Superscript=�. �� .R��  �� $��40 ��� $��40 ��� $��* �.+��� $��Center=����� *
dT.Lower+$+ F2���� $��1��$��Center=����� *
dT.Upper+$+ F2���� $��2��$��Center=����� *dx.IsPassed+$/ OneWay=�#! [ ���� *
dT.Value+$+ F2���� $��2��$
��5,10q�$��	重复性��.���  �� $��* ��� $
��Auto �.+�  " .  ���� * SparkleRepeatabilityTableItems+'   $
( Auto
 $) 序号��* �� *Index+  $
( Auto
 $) 角度��* �� *Angle+  * �� $  	dSi.Value�$+ F4��) �� $
��ΔSi���� $��Palatino Linotype)�$� ��$, Superscript=�0 �� # [ "  .���  � �� �� *dSi.IsPassed+$/ OneWay=�#! [   * �� $  	dSa.Value�$+ F4��) �� $
��ΔSa���� $��Palatino Linotype)�$� ��$, Superscript=�0 �� # [ "  .���  � �� �� *dSa.IsPassed+$/ OneWay=�#! [ �� $��1��$��40��$��5q�#��[ ���� *$"MeasureSparkleRepeatabilityCommand+.���� $ 
Horizontal=�.+��� $��30����  $ TapeMeasure=�
 �   ��� $	��5,3q�$��Center=�$��16(�
�������$��重复性测量��