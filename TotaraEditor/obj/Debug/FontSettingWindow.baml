   M S B A M L   `   `   `  ����G  CTotaraEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TotaraEditorTotaraEditor  5   	   $    TotaraEditor.FontSettingWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TotaraEditor     5   	     � Title$  FontSettingWindow��5   	   $	��300��6#   $	��360��60    � Icon$ Resources/Totara_logo.png��6<    � WindowStyle$ 
ToolWindow=�6]    � 	Resources 5	       S� ObjectInstance!  System.Windows.Media.Colors S� 
MethodName   	ColorList��%L   &	        S� 5
   
   "  5      $ GetProperties��5      5      5      .��5      � 
���   � � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��80 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��90 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6    $��Font Family��6-   $��Center=�6C   �� 5   
   $��1��6   $��0��6#   $��22��60   $	��240��6<   $��Center=�5      $
��Left=�6.      System.Windows.Media.Fonts   SystemFontFamilies#��Z 5       	 �� SelectedValuePath$	 Source��5"      
 �� DisplayMemberPath$
 Source��5#       �� SelectedValue 5!      �� *
FontFamily+ �� Mode$ TwoWay=�5#      �� 5&   
   $��0��6   $��1��6    $��
Font Color��6-   $��Center=�6B   �� 5(   
   $��1��6   $��1��6$   $��22��61   $	��240��6=   $��Center=�5)      $
��Left=�6/   $

 Name��5,      $
	 Name��5-      ��5*      ��  �� Source# [   5+      �� *	FontColor+$ TwoWay=�5-      �� 5/   
   $��0��6   $��2��6    $��	Font Size��6-   $��Center=�6A   � 51   
   $��1��6   $��2��6   $��Center=�6,    53      �� 54      $��* �6    �� 55      $��26 �6     57      �� 58      $��30 �6#   �� 59      $��* �6#   �� 5:      $��30 �6#   .+�5=      �� $��0��6   $��0��6!   $
��Left=�61   $	��Top=�6L   .��6e   106i   �� 5>      -   5         ��Name$ fontSizeSlider��5>      $	��200��6-   $��0��69   $��1��6F   $��Center=�6V   $��Center=�6s    � Minimum$ 10Q�5?       � Maximum$ 30Q�6    �� TickFrequency$ 1Q�6+    �� IsSnapToTickEnabled .  � Value 5@      �� *
FontSize+$ TwoWay=�6:   �� 5A      $��0��6   $��2��6!   $��Right=�61   $	��Top=�6M   .��6f   306j   �� 5C      $��1��6   $��1��6!   $��Center=�61   ��6N   ��  �� ElementName$ fontSizeSlider�� �� Path$ Value�6�   5D      5F      5G      