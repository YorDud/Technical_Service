   M S B A M L   `   `   `  ����  WpfApp43    -WpfApp4.MiniWindows.MonitorNaryad_Edit_Window   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Статус наряда��5   	   $	��309��6   $	��546��6,   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryLight=�68    � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5	      .��5   
   �� 
�������$��15,15,15,15q�6$    �� CornerRadius$ 15��5       �� Padding$ 10q�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 15Q�6#   
   Color$
 	#FF673AB7��63      ShadowDepth$ 0Q�6E   6W   ._�5      � $��10q�6    � RowDefinitions 5      �� 5      $��50 �6$   �� 5      $
��Auto �6$   .+�5      �� -   5         ��Name$ 
ExitButton��5      $��×��6)   $��Right=�5   	   $	��Top=�6%   $��30(�5   	   $
��Bold"�6   
������ 
������ 
���  ��$��0,-14,-15,0q�5      $��39��5   "   $��50��6.   �� 5      $��0��6   .+�5      �� $0��*Изменить статус наряда��6    $��Center=�6>   $��Center=�6Y   $��10q�5        $��24(�6,   $
��Bold"�6:   $��Yu Gothic UI)�6L   5!      � 5#      $��1��6   $��	0,10,0,20q�6$    5$      �� 5%      $
��Auto �6(   �� 5&      $
��Auto �6(   �� 5'      $
��Auto �6(   �� 5(      $
��Auto �6(   �� 5)      $
��Auto �6(   �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   �� 5.      $
��Auto �6(   �� 5/      $
��Auto �6(   �� 50      $
��Auto �6(   �� 51      $��30 �6(   �� 52      $
��Auto �6(   �� 53      $
��Auto �6(   �� 54      $
��Auto �6(   �� 55      $
��Auto �6(   �� 56      $
��Auto �6(    � ColumnDefinitions 58      �� 59      $
��Auto �6+   �� 5:      $��* �6+   .+�5>      �� $��Статус:��6   $��6��6.   $��0��6;   $��Center=�6K   $��5q�6f   $��20(�5?      $
��Bold"�6   $��Yu Gothic UI)�6(   �� 5@      -   5         ��Name$ Status��5@      $��15(�6/   $
��Bold"�6=   $��Yu Gothic UI)�6O   $��6��6i   $��1��6v   $��5q�6�   $��35��6�   $��3q�6�    "  MaterialDesignOutlinedComboBox#��[  6�   �� 5E      -   5        $��Сохранить��5E      $��13��61   $��2��6?   $��50��6S   $��5q�6_   $��24(�6j   $
��Bold"�6x   $��Yu Gothic UI)�6�   6�   5H      5I      5J      5K      