   M S B A M L   `   `   `  ����  WpfApp4/    )WpfApp4.MiniWindows.Work_List_Edit_Window   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$'  !Типы оборудования��5   	   $	��389��6#   $	��600��60   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryLight=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   #   .��5      �� 
�������$��15,15,15,15q�6     �� CornerRadius$ 15��5       �� Padding$ 10q�5       J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 15Q�6   
   Color$
 	#FF673AB7��6/      ShadowDepth$ 0Q�6A   6S   ._�5   
   � $��
10,0,10,10q�6    � RowDefinitions 5      �� 5      $��50 �6    �� 5      $
��auto �6    �� 5      $
��Auto �6    .+�5      �� -   5         ��Name$ 
ExitButton��5      $��×��6)   $��Right=�5      $	��Top=�61   $��30(�5      $
��Bold"�6#   
������ 
������ 
���  ��$��0,-14,-15,0q�5!   &   $��39��5"   .   $��50��6:   �� 5$      $��0��6   .+�5%      �� $��0��6   $*��$Изменить вид работы��6)   $��Center=�6D   $��Center=�6_   $��10q�6|   $��24(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   5&      � 5(      $��1��6   $��
0,10,0,126q�6     5)      �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   �� 5.      $
��auto �6$   �� 5/      $
��auto �6$   �� 50      $
��auto �6$   �� 51      $
��auto �6$   �� 52      $
��auto �6$   �� 53      $��30 �6$   �� 54      $
��auto �6$   �� 55      $
��auto �6$    � ColumnDefinitions 57      �� 58      $
��Auto �6'   �� 59      $��* �6'   .+�5<      �� $��Вид работы:��6   $��0��6.   $��0��6;   $��Center=�6K   $��5q�6f   $��15(�6q   $
��Bold"�6   $��Yu Gothic UI)�6�   �� 5=      -   5         ��Name$ 	Work_List��5=      $��0��6-   $��5q�6:   $��30��6F   $��3q�6R    !  MaterialDesignOutlinedTextBox#��[  6^   $��1��6�   �� 5?      $8��2Наименование оборудования:��6   $��1��6=   $��0��6J   $��Center=�6Z   $��5q�6u   $��15(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 5@      -   5         ��Name$ 
DeviceType��5@       �� 
IsEditable . $��1��6B   $��1��6O   $��5q�6_   $��30��6j   $��3q�6v    " MaterialDesignOutlinedComboBox#��[ 6�   �� 5B      $��Норм. Час:��6   $��2��6-   $��0��6:   $��Center=�6J   $��5q�6e   $��15(�6p   $
��Bold"�6~   $��Yu Gothic UI)�6�   �� 5C      -   5        $ NormHour��5C      $��2��6,   $��5q�69   $��30��6E   $��3q�6Q   #��[  6]   $��1��6�   �� 5E      -   5        $��10��5E      $��Сохранить��6'   $��5q�6;   $��2��6F   6Z   �� 5F      -   5        $��11��5F      $(��"Удалить вид работы��6'   $��5q�6D   $��2��6O   ���	#FFDC2C2C6�   5G      5H      5I      5J      