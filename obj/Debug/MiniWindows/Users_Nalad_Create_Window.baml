   M S B A M L   `   `   `  ����  WpfApp43    -WpfApp4.MiniWindows.Users_Nalad_Create_Window   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Сотрудники��5   	   $	��450��6   $	��700��6)   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryLight=�65    � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5	      .��5   
   �� 
�������$��15,15,15,15q�6$    �� CornerRadius$ 15��5       �� Padding$ 10q�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   
BlurRadius$	 15Q�6#   
   Color$
 	#FF673AB7��63      ShadowDepth$ 0Q�6E   6W   ._�5      � $��10q�6    � RowDefinitions 5      �� 5      $��50 �6$   �� 5      $
��auto �6$   �� 5      $
��Auto �6$   .+�5      �� -   5         ��Name$ 
ExitButton��5      $��×��6-   $��Right=�5   	   $	��Top=�6%   $��30(�5   	   $
��Bold"�6   
������ 
������ 
���  ��$��0,-14,-15,0q�5      $��39��5    "   $��50��6.   �� 5"      $��0��6   .+�5#      �� $��0��6    $8��2Добавить нового сотрудника��6-   $��Center=�6O   $��Center=�6j   $��10q�6�   $��24(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   5$      � 5(      $��1��6   $��
0,10,0,126q�6$    5)      �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   �� 5.      $
��auto �6(   �� 5/      $
��auto �6(   �� 50      $��30 �6(   �� 51      $
��auto �6(    � ColumnDefinitions 53      �� 54      $
��Auto �6+   �� 55      $��* �6+   .+�58      �� $��ФИО:��6   $��0��6+   $��0��68   $��Center=�6H   $��5q�6c   $��15(�6n   $
��Bold"�6|   $��Yu Gothic UI)�6�   �� 59      -   5         ��Name$	 FIO��59      $��0��6+   $��5q�68   $��30��6D   $��3q�6P    !  MaterialDesignOutlinedTextBox#��[  6\   $��1��6�   �� 5;      $��Логин:��6   $��1��6-   $��0��6:   $��Center=�6J   $��5q�6e   $��15(�6p   $
��Bold"�6~   $��Yu Gothic UI)�6�   �� 5<      -   5        $	 Log��5<      $��1��6+   $��5q�68   $��30��6D   $��3q�6P   #��[  6\   $��1��6�   �� 5=      $��Пароль:��6   $��2��6.   $��0��6;   $��Center=�6K   $��5q�6f   $��15(�6q   $
��Bold"�6   $��Yu Gothic UI)�6�   �� 5>      -   5        $
 Pass��5>      $��2��6,   $��5q�69   $��30��6E   $��3q�6Q   #��[  6]   $��1��6�   �� 5?      $��Должность:��6   $��3��6-   $��0��6:   $��Center=�6J   $��5q�6e   $��15(�6p   $
��Bold"�6~   $��Yu Gothic UI)�6�   �� 5@      -   5         ��Name$
 Role��5@      $��3��6)   $��5q�66   $��30��6B   $��3q�6N    " MaterialDesignOutlinedComboBox#��[ 6Z   $��1��6�   .@�5A      �� .��6#   admin6*   �� 5B      .��6#   nalad6*   5C      �� 5F      $��Смена:��6   $��4��6-   $��0��6:   $��Center=�6J   $��5q�6e   $��15(�6p   $
��Bold"�6~   $��Yu Gothic UI)�6�   �� 5G      -   5        $ Smena��5G      $��4��6*   $��5q�67   $��30��6C   $��3q�6O   #��[ 6[   $��1��6�   .@�5H      �� .��6#   16&   �� 5I      .��6#   26&   �� 5J      .��6#   Пятидневка6/   �� 5K      .��6#   1 (ночь)6-   �� 5L      .��6#   2 (ночь)6-   5M      �� 5O      $��Телефон:��6   $��5��6+   $��0��68   $��Center=�6H   $��5q�6c   $��15(�6n   $
��Bold"�6|   $��Yu Gothic UI)�6�   �� 5P      -   5        $ Phone��5P      $��5��6-   $��5q�6:   $��30��6F   $��3q�6R   #��[  6^   $��1��6�   �� 5S      -   5        $��7��5S      $��Добавить��6*   $��5q�6=   $��2��6H   6\   5U      5V      5[      5_      