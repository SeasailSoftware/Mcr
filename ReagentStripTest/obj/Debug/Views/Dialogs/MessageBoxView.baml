   M S B A M L   `   `   `  ����M  IReagentStripTest, Version=2.0.925.1, Culture=neutral, PublicKeyToken=nullY1clr-namespace:ReagentStripTest.ViewModels.Dialogs#ReagentStripTest.ViewModels.Dialogs  5      3    -ReagentStripTest.Views.Dialogs.MessageBoxView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Edialogs1clr-namespace:ReagentStripTest.ViewModels.Dialogs     5      g cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconpacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      $	��400��5      $
��1000��5      (  "MahApps.Metro.Controls.MetroWindow)    "RightWindowCommandsOverlayBehavior$  HiddenTitleBar=�5         ShowCloseButton .  � ShowInTaskbar .     ShowMaxRestoreButton .     ShowMinButton .   � SizeToContent$ WidthAndHeight=�5       � WindowStartupLocation$ CenterOwner=�5       � Title 5
      �� *DisplayName+ � Icon 5      4  .MahApps.Metro.IconPacks.MaterialImageExtension  	   Kind$	 Message=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension
   Brush

 ����� � 	Resources 5         AccentColor  WarningColor �� Color  
ErrorColor  	InfoColor  TopRectWarningBrush � Offset � Color  TopRectErrorBrush  TopRectInfoBrush  TopRectQuestionBrush  BoolToVisibilityConverter��%�  &	        &	 !     &	 A     &	 ^     &	 �     7   &	 �     7   &	 V    7   &	 �    7   &	 4    ��5   
   	#FF252525��6/   �� 5   
   $ Orange��6/   �� 5   
   $	 Red��6-   �� 5   
   $ 	LightBlue��6,   �� 5   
   .��5      � $ 0Q�6   $ Orange��6&   � 5      $ 1Q�6   8   6&   5       �� 5!   
   .��5"      � $ 0Q�6   $	 Red��6&   � 5#      $ 1Q�6   8   6&   5$      �� 5%   
   .��5&      � $ 0Q�6   $ 	LightBlue��6&   � 5'      $ 1Q�6   8   6&   5(      �� 5)   
   .��5*      � $ 0Q�6   $ 	LightBlue��6&   � 5+      $ 1Q�6   8   6&   5,      �� 5-   
   6'   5.         RightWindowCommands 50      +  %MahApps.Metro.Controls.WindowCommands  51   
   $��1��6   .@�52      �� -  'MahApps.Metro.Controls.VisibilityHelper   	IsVisible 6   �� *IsHelpVisible+��6N   �� *HelpCommand+.��53      /  )MahApps.Metro.IconPacks.MaterialExtension     Kind$ 
HelpCircle=�6%   54      55      .��57      � $��10q�6    � RowDefinitions 58   
   �� 59      $��* �6   �� 5:      $
��Auto �6   �� 5;      $
��Auto �6   .+�5>   
   ]� $��0��6   ��. .+�5@      �� $��Center=�6   |�.  ��5A      �� 5B       �� 
TargetType" ��6    �� Triggers 5C      �� 5D       �� Binding 6*   �� *Icon+ �� Value Warning6C   .a�5E   "   ��  �� Property � �� �� Value 5F   &   �� 5H   *   $��50��5I   -   $��50��5J   -   ��5K   -     $	 Information=� 	 MahApps.Brushes.Accent#
 [	 5N   #   5O      �� 5P       6*   �� *Icon+	 Error6C   .a�5Q   "   ��  � �� 5R   &   �� 5T   *   $��50��5U   -   $��50��5V   -   ��5W   -     $	 Information=�

 �  ��5Z   #   5[      �� 5\       6*   �� *Icon+ Information6C   .a�5]   "   ��  � �� 5^   &   �� 5`   *   $��50��5a   -   $��50��5b   -   ��5c   -     $	 Information=�#
 [	 5f   #   5g      �� 5h       6*   �� *Icon+ Question6C   .a�5i   "   ��  � �� 5j   &   �� 5l   *   $��50��5m   -   $��50��5n   -   ��5o   -     $	 ChatQuestion=�#
 [	 5r   #   5s      5u      5w      �� 5z      $��20,0,0,0q�5{      $��Disabled=�5|      $
��Auto=�5}      .��5~      �� $��	0,20,0,20q�5      $��Center=�5�      $��Right=�5�      $��16(�5�       �� 
LineHeight$ 20��5�      $
��Wrap=�5�      ��5�      �� *	Message+5�      5�      5�      �� 5�   
   $��1��5�      $	��600��5�      $��5q�5�      $��12(�5�      $ �� HorizontalScrollBarVisibility$
 Auto=�5�       �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�5�       5�      �� *IsHelpTextVisible+��5�      �� *
HelpText+5�      �� 5�   
   $��2��5�      $��0,3,0,0q�5�      $��Right=�5�      |�.  ��5�      �� *ButtonItems+��5�      �� 5�      .R�5�      ��  �� Orientation$ 
Horizontal=�6!   5�      ��5�      �� 5�       �� DataType$' !dialogs:MessageBoxButtonViewModel��6   .R�5�      ��  ��Name$ _button��5�      $��72��5�      $��8,8,0,0q�5�      |�. $��14(�5�       
 SquareButtonStyle#��� 
 5�      ��5�      �� *	Command+��5�      �� *DisplayName+  �� IsCancel  5�      �� *
IsCancel+! �� 	IsDefault! 5�      �� *	IsDefault+5�      " �� Triggers" 5�      �� 5�      $
 True��6D    6&   �� *	IsDefault+.a�5�      �� # �� 
TargetName$# _button��6%     AccentedSquareButtonStyle# �  6K    � ��5�      5�      5�      5�      5�      