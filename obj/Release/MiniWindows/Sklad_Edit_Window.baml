   M S B A M L   `   `   `  ����   WpfApp4+    %WpfApp4.MiniWindows.Sklad_Edit_Window   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  
Склад��$	��626��$	��700�� � 
ResizeMode$ NoResize=�.��� 
�������.+��� 
�������$��15,15,15,15q� �� CornerRadius$ 15�� �� Padding$ 10q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 30Q�   Color$
 Gray��   ShadowDepth$ 0Q�._�� $��10q� � RowDefinitions �� $��50 ��� $
��auto ��� $
��Auto �.+��� $��0��.+��� $��0��$#��Изменить деталь��$��Center=�$��Center=�$��10q�$��24(�$
��Bold"�$��Yu Gothic UI)�� $��1��$��
0,10,0,126q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��30 ��� $
��auto ��� $
��auto �	 � ColumnDefinitions	 �� $
��Auto ��� $��* �.+��� $��Тип детали:��$��0��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   
 ��Name$
 Deteil_Types��$��0��$��5q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  $��1���� $��Модель:��$��1��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Model��$��1��$��5q�$��30��$��3q�#��[  $��1���� $!��Производитель:��$��2��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Proizvod��$��2��$��5q�$��30��$��3q�#��[  $��1���� $��Поставщик:��$��3��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -    ��Name$ Postav��$��3��$��5q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ $��1���� $��Оборудование:��$��4��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $ 
Devices_ID��$��4��$��5q�$��30��$��3q�#��[ $��1���� $��Изображение:��$��5��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Image��$��5��$��5q�$��30��$��3q�#��[  $��1���� $"��Место хранения:��$��6��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Location��$��6��$��5q�$��30��$��3q�#��[  $��1���� $��Количество:��$��7��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Kolich��$��7��$��5q�$��30��$��3q�#��[  $��1���� -	   $��9��$��Сохранить��$��5q�$��2���� -
   $��10��$!��Удалить деталь��$��5q�$��2�����	#FFDC2C2C