   M S B A M L   `   `   `  ���� M  IReagentStripTest, Version=2.0.925.2, Culture=neutral, PublicKeyToken=nullY1clr-namespace:ReagentStripTest.ViewModels.Dialogs#ReagentStripTest.ViewModels.Dialogs  3    -ReagentStripTest.Views.Dialogs.MessageBoxView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Edialogs1clr-namespace:ReagentStripTest.ViewModels.Dialogs     g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconpacks-http://metro.mahapps.com/winfx/xaml/iconpacks   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  $	��400��$
��1000��(  "MahApps.Metro.Controls.MetroWindow)    "RightWindowCommandsOverlayBehavior$  HiddenTitleBar=�   ShowCloseButton .  � ShowInTaskbar .     ShowMaxRestoreButton .     ShowMinButton .   � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterOwner=� � Title �� *DisplayName+ � Icon 4  .MahApps.Metro.IconPacks.MaterialImageExtension  	   Kind$	 Message=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension
   Brush

 ����� � 	Resources    AccentColor  WarningColor �� Color  
ErrorColor  	InfoColor  TopRectWarningBrush � Offset � Color  TopRectErrorBrush  TopRectInfoBrush  TopRectQuestionBrush  BoolToVisibilityConverter��%�  &	        &	      &	 %     &	 4     &	 I     7   &	 }     7   &	 �     7   &	 �     7   &	     ��	#FF252525���� $ Orange���� $	 Red���� $ 	LightBlue���� .��� $ 0Q�$ Orange��� $ 1Q�8   �� .��� $ 0Q�$	 Red��� $ 1Q�8   �� .��� $ 0Q�$ 	LightBlue��� $ 1Q�8   �� .��� $ 0Q�$ 	LightBlue��� $ 1Q�8   ��    RightWindowCommands +  %MahApps.Metro.Controls.WindowCommands  $��1��.@��� -  'MahApps.Metro.Controls.VisibilityHelper   	IsVisible �� *IsHelpVisible+���� *HelpCommand+.��/  )MahApps.Metro.IconPacks.MaterialExtension     Kind$ 
HelpCircle=�.��� $��10q� � RowDefinitions �� $��* ��� $
��Auto ��� $
��Auto �.+�]� $��0����. .+��� $��Center=�|�.  ����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *Icon+ �� Value Warning.a���  �� Property � �� �� Value �� $��50��$��50����  $	 Information=� 	 MahApps.Brushes.Accent#
 [	 ��  �� *Icon+	 Error.a���  � �� �� $��50��$��50����  $	 Information=�

 �  ����  �� *Icon+ Information.a���  � �� �� $��50��$��50����  $	 Information=�#
 [	 ��  �� *Icon+ Question.a���  � �� �� $��50��$��50����  $	 ChatQuestion=�#
 [	 �� $��20,0,0,0q�$��Disabled=�$
��Auto=�.���� $��	0,20,0,20q�$��Center=�$��Right=�$��16(� �� 
LineHeight$ 20��$
��Wrap=����� *	Message+�� $��1��$	��600��$��5q�$��12(�$ �� HorizontalScrollBarVisibility$
 Auto=� �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=� �� *IsHelpTextVisible+���� *
HelpText+�� $��2��$��0,3,0,0q�$��Right=�|�.  ���� *ButtonItems+���� .R���  �� Orientation$ 
Horizontal=�����  �� DataType$' !dialogs:MessageBoxButtonViewModel��.R���  ��Name$ _button��$��72��$��8,8,0,0q�|�. $��14(� 
 SquareButtonStyle#��� 
 ���� *	Command+���� *DisplayName+  �� IsCancel  �� *
IsCancel+! �� 	IsDefault! �� *	IsDefault+" �� Triggers" �� $
 True�� �� *	IsDefault+.a��� # �� 
TargetName$# _button��  AccentedSquareButtonStyle# �   � ��