   M S B A M L   `   `   `  ����   WpfApp4,    &WpfApp4.MiniWindows.DeviceCreateWindow   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     � Title$  Оборудование��$	��588��$	��700�� � 
ResizeMode$ NoResize=�.��� 
�������.+��� 
�������$��15,15,15,15q� �� CornerRadius$ 15�� �� Padding$ 10q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 30Q�   Color$
 Gray��   ShadowDepth$ 0Q�._�� $��10q� � RowDefinitions �� $��50 ��� $
��auto ��� $
��Auto �.+��� $��0��.+��� $��0��$/��)Добавить оборудование��$��Center=�$��Center=�$��10q�$��24(�$
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
��auto �	 � ColumnDefinitions	 �� $
��Auto ��� $��* �.+��� $8��2Наименование оборудования:��$��0��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   
 ��Name$
 Name_Device��$��0��$��5q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  $��1���� $&�� Тип оборудования:��$��1��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Device_Type��$��1��$��5q�$��30��$��3q�#��[  $��1���� $��Модель:��$��2��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Model��$��2��$��5q�$��30��$��3q�#��[  $��1���� $"��Серийный номер:��$��3��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -    ��Name$ 
Ser_Number��$��3��$��5q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ $��1���� $��Год выпуска:��$��4��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $ Year_Create_Device��$��4��$��5q�$��30��$��3q�#��[ $��1���� $��Инв номер:��$��5��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Inventory_Number��$��5��$��5q�$��30��$��3q�#��[  $��1���� $��Помещение:��$��6��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Location��$��6��$��5q�$��30��$��3q�#��[  $��1���� $0��*Наименование бух. учет:��$��7��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Name_Buh_Uch��$��7��$��5q�$��30��$��3q�#��[  $��1���� -	   $��9��$��Добавить��$��5q�$��2��