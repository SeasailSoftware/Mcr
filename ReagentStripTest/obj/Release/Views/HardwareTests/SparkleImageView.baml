   M S B A M L   `   `   `  ���� E  AMasTools, Version=2.0.0.908, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MasTools.Views.HardwareTestsMasTools.Views.HardwareTests  3    -MasTools.Views.HardwareTests.SparkleImageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   8local*clr-namespace:MasTools.Views.HardwareTests   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  .���   � RowDefinitions  �� $��* ��� $
��Auto �.+�A�  A� Columns$ 4��.+��� $��20q�
������� �� CornerRadius$ 5,5,0,0��   DropShadowEffect J� Effect# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=�����  �� Path$ Value� �� ElementName$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[0]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��①���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[1]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��②���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[2]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��③���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[3]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��④���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[4]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��⑤���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[5]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��⑥���� $��20q�
�������$ 5,5,0,0��# [  ._��   �� $��* ��� $
��Auto �.+��� $��UniformToFill=����� $ Value�$ imgWidth������ $ Value�$ 	imgHeigth������ *	Images[6]+�� $��1��
�������.+��� $��1��$��5q�$��Center=�$��20(�
�������$	��⑦���� $��Center=�$��Center=�.+��� $��10q� �� Orientation$ 
Horizontal=�.+��� $��3q�$��Center=�$��16(�$��图片宽度��*  $MahApps.Metro.Controls.NumericUpDown  -     Name$ imgWidth��$	��100��$��3q�$��16(�   Value �� *
ImageWidth+�� $��10q�$ 
Horizontal=�.+��� $��3q�$��Center=�$��16(�$��图片高度��  -   $ 	imgHeigth��$	��100��$��3q�$��16(� �� *ImageHeigth+A� $��1��	 A� Rows$	 1��.+��� $��1��$��20q����� *ClearCommand+.���� $ 
Horizontal=�.+��� $��35��$��35����4  .MahApps.Metro.IconPacks.MaterialImageExtension  
   Kind$
 Close=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 �  ���� $
��10,5q�$��Center=�$��40(�$��清除图片���� $��1��$��20q����� *SaveCommand+.���� $ 
Horizontal=�.+��� $��35��$��35����  $
 ContentSave=�
 ��  ��� $
��10,5q�$��Center=�$��40(�$��保存图片��