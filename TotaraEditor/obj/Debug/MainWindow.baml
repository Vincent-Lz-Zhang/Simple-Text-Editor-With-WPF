   M S B A M L   `   `   `  ����G  CTotaraEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TotaraEditorTotaraEditor  5   	       TotaraEditor.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TotaraEditor     5   	     � Title$  Untitled - Totara Editor��5   	   $	��350��6*   $	��525��67    � Icon$ Resources/Totara_logo.png��6C    � WindowStyle$ 
ToolWindow=�6d    � 	Resources 5	      (  "TotaraEditor.FontFamilyConversions   FontFamilyConversions'  !TotaraEditor.FontColorConversions  FontColorConversions��%:   &	        &	        5
   
   6&     5   
   6%   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.New��5      6S     5   
   -   5        $ ApplicationCommands.Open��5      6<     5   
   -   5        $ ApplicationCommands.Save��5      6U     5   
   -   5        $  ApplicationCommands.SaveAs��5      6Y     5   
   -   5        $  ApplicationCommands.Delete��5      6Y   .��5      �  � RowDefinitions 5   
   �� 5      $��68 �6   �� 5      $��21 �6   �� 5      $��* �6   �� 5      $��28 �6   .+�5   
   � $��0��6   ���#FEE000$��LeftToRight=�66   .+�5       �� $��68��6   $��68��6    $��Resources/Totara_logo.png��6+   $
��Fill=�6N   5!      ]� 5$   
   $��1��6   
���   �.+�5%      o� $
��Left=�6   ���	#FF181818���	#FFABABAB 5&       �� Property �� Value	 �� Triggers
 P� Property P� Value��%  'm�       �� 5'      �5(      �� 5)       � �� �	#FF1818186D   �� 5*       � �� �	#FFABABAB6D   	 5,      P� 5-      
 �@J�IsMouseOver . . �5.   "   ��  � ��
 � � �6H   �� 5/   "    � ��
 �  ��6H   50      56      57      .@�58      m� $
��File��6   .@�59      m� $	��New��6    m� Command$ ApplicationCommands.New��6,   6P   m� 5:      $
��Open��6   $ ApplicationCommands.Open��6-   6R   m� 5;      $
��Save��6   $ ApplicationCommands.Save��6-   6R   m� 5<      $��Save As��6   $  ApplicationCommands.SaveAs��60   6W   �� 5=      ���	#FF181818���	#FFABABAB6P   m� 5>      $��Delete��6   $  ApplicationCommands.Delete��6/   6V   m� 5?      -   5        $
��Quit��5?      6K   5@      m� 5A      -   5        $��Format��5A      6L   m� 5B      $��About��6   6-   5C      �� 5E      -   5         ��Name$ status��5E      $��Right=�6$   $��Right=�6;   $��Center=�6W   $��14(�6r   $��0, -5, 0, 0q�6�   6�   5F      �� 5I   
   -	   5         ��Name$ editor��5I      $��2��6"    �� TextWrapping$
 Wrap=�6/    �� AcceptsReturn .  �� 
AcceptsTab . " �� VerticalScrollBarVisibility$
 Auto=�5K      ��5L      �� *
FontFamily+ �� Mode$ OneWay=� �� 	Converter# [  ��5M      �� *	FontColor+$ OneWay=�# [ ��5N      �� *
FontSize+$ OneWay=�.��5O      5P      � 5S   
   $��3��6   ���#03A9DD.+�5T      �� 
�������$'��!Vincent, Copyright reserved 2016.��6,   5U      5W      5X      