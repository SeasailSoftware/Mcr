   M S B A M L   `   `   `  ���� M  IReagentStripTest, Version=2.0.925.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:ReagentStripTest.ViewsReagentStripTest.Views  &     ReagentStripTest.Views.ShellView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ^ ZCaliburn.Micro.Platform, Version=4.0.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  :d2http://schemas.microsoft.com/expression/blend/2008  g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   2local$clr-namespace:ReagentStripTest.Views   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  $
��1200��$	��800��  Caliburn.Micro.Message    Attach$6  0[Event MouseDown]=[Action MouseDown($eventArgs)]��(  "MahApps.Metro.Controls.MetroWindow   TitleBarHeight$ 95��   TitleCharacterCasing$ Normal=� � WindowStartupLocation$ CenterScreen=� � Title �� *DisplayName+ � 	Resources  �� 
TargetType   defaultRadioStyle �� Property �� Value	 �� 
TargetType
 ��Name ��Name J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  MahApps.Brushes.Accent2  MahApps.Brushes.Accent �� CornerRadius ��Name J� Opacity �� Orientation
 � Tag �� TextAlignment �� Triggers��%�  '��       &	  
     7  '�� �    �� " ���� " 	�.���  � ��
 ���� ��  � ��$
 15,0q���  � ��$ 0q���  �@�FocusVisualStyle Z� ��  � ��$ Center=���  � ��$ Center=���  � ��$ Center=���  � ��$ Center=���  � ��
 �������  � �� �� "	 	�.R�� .+��� $
 border��$��0q��� $ contentPresenter��$��1��#��z��#��z��#��z��|�.  ��. # z  P�  �@d�	IsChecked$
 TrueY�. ���  � ��
 � �����  � ��$ 14(��� $ border�� � ��$ 1q���  � ��$
 Bold"��� 8    � ���� $ border�� � ��
 � ����� $ border�� � ��$ 0,0,0,4q�P�  �@d�	IsChecked$ FalseY�. ��� $ border�� � ��$ 0q��� " ��.���  �@�FocusVisualStyle Z� ��  � ��$ 12(���  �@J�Opacity$ 1Q���  � ��# �  ��  � ��$ Center=���  � ��$ Stretch=���  � ��$ 微软雅黑)���  � ��$ Stretch=���  � ��$ 	ExtraBold"���  � �� �� *DisplayName+��  � �� �� "	 ��.R�� $��1,0,0,0q�
������ .+��� $
 	PART_GRID�����#F0F0F0$ 	10,10,0,0���� $ x��$
��20,5q�$��Center=�
������ # z $ 
Horizontal=�.+��� $ Content��$��Center=�$��Center=�$��Header���� $��0,0,0,0q�$��Center=�#��z��#��z��#��z $
 Left=� P�  �@J�IsMouseOver . . ��� $ 	PART_GRID��# �   � ����  � ��
 �����P�  �@��
IsSelected . . ���  � ��
 ������� $ 	PART_GRID��# �   � ����  �@J�Opacity$	 0.8Q� P�  �@��
IsSelected .  . ���  �@J�Opacity$	 0.9Q�P�  �@��
IsSelected . . ���  �@J�Opacity$ 1Q�   IconTemplate �� .R��� $��50����4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ TestTube=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 �����   RotationAngle$ 45Q�   TitleTemplate �� .R�� $��5,5,5,10q�$��Bottom=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� .+��� $��Center=�$ 
Horizontal=�.+��� $��0,0,0,0q�$��Bottom=�$��25(�$
��Bold"�
�������$��试纸测试工具���� $��1��$��5q�$��Bottom=�$��12(�$��Light"�
�������$��Reagent Strips Test Tools��.���  �� $
��Auto ��� $
��Auto ��� $��* ��� $��45 �.+�o� .@�m�   s_Instrument#���  .@�m�   s_Calibrate#���   m� Command �� *CalibrateCommand+m�  
 s_Data#���  .@�m�   s_Rename#���   �� *RenameCommand+ m� Icon /  )MahApps.Metro.IconPacks.MaterialExtension      Kind$  	RenameBox=�m�   s_Delete#���   �� *DeleteCommand+   $  Delete=�m�   s_Clear#���   �� *ClearCommand+   $  CloseBoxMultiple=�m� $��清除所有�� �� *ClearAllCommand+   $  NotificationClearAll=�m�  	 s_ExportToExcel#��� 	  �� *ExportToExcelCommand+   $  	FileExcel=�m�  
 
s_Settings#��� 
  �� *SettingsCommand+m�   s_About#���   �� *AboutCommand+`� $��1��.�c� ! c� 	BandIndex$! 0��.@��� ���� *CalibrateCommand+.���� .+��� $��3q���  $ VectorDifference=�# [ �� $��3q�#���  c� $! 1��.@��� ���� *RenameCommand+.���� .+��� $��3q���  $ 	RenameBox=�# [ �� $��3q�#���  �� ���� *DeleteCommand+.���� .+��� $��3q���  $ Delete=�# [ �� $��3q�#���  �� ���� *ClearCommand+.���� .+��� $��3q���  $ CloseBoxMultiple=�# [ �� $��3q�#���  �� ���� *ClearAllCommand+.���� .+��� $��3q���  $ NotificationClearAll=�# [ �� $��3q�$��清除所有���� ���� *ExportToExcelCommand+.���� .+��� $��3q���  $ 	FileExcel=�# [ �� $��3q�#��� 	 c� $! 2��.@��� ���� *SettingsCommand+.���� .+��� $��3q���  $	 Cog=�# [ �� $��3q�  Toolbar_Settings#���  �� $��1��$��Bottom=�� $��2��" � ColumnDefinitions" �� $
��Auto �$	��200��$	��300���� $
��Auto ��� $��* �.+��� $��设备列表��.���  �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�c� .@��� $	��5,3q����� *AddInstrumentCommand+.���� $ 
Horizontal=�.+�.  (MahApps.Metro.IconPacks.PackIconMaterial  $��3q�$��Center=�#   Kind$# FilePlus=��� $	��5,3q�$��Center=�$��添加���� $	��5,3q����� *DeleteInstrumentCommand+.���� $ 
Horizontal=�.+�  $��3q�$��Center=�$# Delete=��� $	��5,3q�$��Center=�$��删除���� $��1��$	��0.8���� $��2������ *InstrumentModels+$ �� SelectedItem$ �� *InstrumentModel+���� .R��� $ 
Horizontal=�.+��� $��30��$��30��$��5q����� *IsConnected+% �� Mode$% OneWay=�  BooleanToImageConverter& �� 	Converter#& [ �� $��5q�$��Center=�.+��� $	��5,3q��� $�编号:�� �� ��� *Id+�� $	��5,3q��� $�
序列号:�� �� ��� *SN+A� $��3��' A� Rows$' 1��.+��� $��5q�#��[  ) %MahApps.Styles.Button.Calendar.Header#��[ ���� *StartCommand+.���� $ 
Horizontal=�.+��� $��3q���  $
 Play=�
 � � ��� $��3q�
���   �$��全部开始���� $��5q�#��[ ���� *StopCommand+.���� $ 
Horizontal=�.+��� $��3q���  $
 Stop=�
 �  ���� $��3q�
���  ��$��全部停止���� $��1��,  &MahApps.Metro.Controls.MetroTabControl  $��2��.@��� $��实时图表��.���� ���� *InstrumentModels+���� .R�� $
��10,5q� �� $
��Auto ��� $��* �.+�� 
�������" �� $
��Auto ��� $��* ��� $
��Auto �.+��� $ 
Horizontal=�.+��� $	��5,5q�$��Center=�$��Normal"�
���   �$��编号:���� $	��5,3q�$��Center=�$��Normal"�
���   ����� *Id+-  'MahApps.Metro.Controls.MetroProgressBar  $��1��( � IsIndeterminate( �� *	IsRunning+�� $��2��$ 
Horizontal=�.+��� $��30,3,3,3q�$��Center=�$��Normal"�
���   �$��时间:���� $	��5,3q�$��Center=�$��Normal"�
���   ����� *ExecutedTime+�� $��20,3,3,3q�$��Center=�$��Normal"�
���   �$��温度:���� $	��5,3q�$��Center=�$��Normal"�
���   ����� *Temperature+) �� StringFormat$) 
{0:F2} ℃���� $��1��	  Caliburn.Micro.View* 	  Model* �� *DateTimeChart+���� .R�A� + A� Columns$+ 2��$' 3���� $��数据��.��� " �� $
��Auto �$	��200��$	��300���� $
��Auto ��� $��* �.+��� $��10q����� *InstrumentModel.RecordModels+$ �� *InstrumentModel.RecordModel+���� .R��� $ 
Horizontal=�.+��� ���� *Index+�� $��  ���� ���� *Name+�� $��  ���� ���� *
DateTime+�� $��1��� $��2�� �� $��* ��� $
��Auto �.+�&
   System.Windows.Controls.DataGrid
  -   , 
 Name$, SampleDataGrid��- 
  AutoGenerateColumns- .  . 
  CanUserAddRows. .  / 
  SelectionMode$/ Single=����� *%#InstrumentModel.RecordModel.Samples+$ �� *InstrumentModel.SampleModel+0 
  Columns0 0  *System.Windows.Controls.DataGridTextColumn    s_Index,  &System.Windows.Controls.DataGridColumn1   Header#1 �  1  +System.Windows.Controls.DataGridBoundColumn2   Binding2 �� *Index+    
s_DateTime#1 �  2 �� *
DateTime+    s_Illuminant#1 �  2 �� *
Illuminant+    
s_Observer#1 �  2 �� *
Observer+4  .System.Windows.Controls.DataGridTemplateColumn    s_PseudoColor#1 �  3   CellTemplate3 �� .R�� .+��� $��50��$��20������ *PseudoColor+  $1 温度��2 �� *Temperature+$) 
{0:F2} ℃��    CIELab_L#1 �  2 �� *L+$) F2��    CIELab_a#1 �  2 �� *a+$) F2��    CIELab_b#1 �  2 �� *b+$) F2��    s_SpecifiedWaveLength#1 �  2 �� *SpecifiedWaveLength+    s_Reflectance#1 �  2 �� *Data+�� $��1��$	��200��$��10q�* �� * InstrumentModel.ChartViewModel+�� $��3��.@��� .���� $��Center=����� *
StatusText+�� $
��10,5q�$��Right=�.���� $ 
Horizontal=�.+��� $��5q����� *IsConnected+#& [ �� $��Center=����� *IsConnected+   BooleanToForegroundConverter#& [ ���� *ConnectionStatus+