   M S B A M L   `   `   `  ����  WpfApp4,    &WpfApp4.MiniWindows.Crash_Create_Wndow   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$'  !Типы оборудования��5   	   $	��320��6#   $	��600��60   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ PrimaryLight=�5	   	    � 
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
���  ��$��0,-14,-15,0q�5!   &   $��39��5"   .   $��50��6:   �� 5$      $��0��6   .+�5%      �� -   5         ��Name$ 
TitleLabel��5%      $��0��60   $(��"Сообщить о поломке��6=   $��Center=�6W   $��Center=�6r   $��10q�6�   $��24(�6�   $
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
��auto �6$    � ColumnDefinitions 56      �� 57      $
��Auto �6'   �� 58      $��* �6'   .+�5;      �� -   5         ��Name$ LocationLabel��5;      $��Помещение:��6/   $��0��6D   $��0��6Q   $��Center=�6a   $��5q�6|   $��15(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 5<      -   5         ��Name$ Location��5<      $��0��6-   $��1��6:   $��5q�6J   $��30��6U   $��3q�6a    "  MaterialDesignOutlinedComboBox#��[  5=      6G   �� 5>      -   5        $ DeviceLabel��5>      $0��*Название оборудования:��6-   $��1��6N   $��0��6[   $��Center=�6k   $��5q�6�   $��15(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 5?      -   5        $ Device��5?      $��1��6+   $��1��68   $��5q�6H   $��30��6S   $��3q�6_   #��[  5@      �� 5A      -   5        $ CommentLabel��5A      $��Описание:��6.   $��2��6B   $��0��6O   $��Center=�6_   $��5q�6z   $��15(�6�   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 5B      -   5         ��Name$ Comment��5B      $��2��6+   $��1��68   $��5q�6H   $��30��6S   $��3q�6_    ! MaterialDesignOutlinedTextBox#��[ 5C      �� 5F      -	   5        $ StartWorkButton��5F      $��10��62   $��Сообщить��6@   $��5q�6S   $��2��6^   5G   	   5H      5I      5J      5K      