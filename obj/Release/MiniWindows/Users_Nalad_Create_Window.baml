   M S B A M L   `   `   `  ����   WpfApp43    -WpfApp4.MiniWindows.Users_Nalad_Create_Window   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Сотрудники��$	��500��$	��700�� � 
ResizeMode$ NoResize=�.��� 
�������.+��� 
�������$��15,15,15,15q� �� CornerRadius$ 15�� �� Padding$ 10q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 30Q�   Color$
 Gray��   ShadowDepth$ 0Q�._�� $��10q� � RowDefinitions �� $��50 ��� $
��auto ��� $
��Auto �.+��� $��0��.+��� $��0��$8��2Добавить нового сотрудника��$��Center=�$��Center=�$��10q�$��24(�$
��Bold"�$��Yu Gothic UI)�� $��1��$��
0,10,0,126q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��auto ��� $
��auto ��� $��30 ��� $
��auto �	 � ColumnDefinitions	 �� $
��Auto ��� $��* �.+��� $��ФИО:��$��0��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   
 ��Name$	
 FIO��$��0��$��5q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  $��1���� $��Логин:��$��1��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $	
 Log��$��1��$��5q�$��30��$��3q�#��[  $��1���� $��Пароль:��$��2��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $

 Pass��$��2��$��5q�$��30��$��3q�#��[  $��1���� $��Должность:��$��3��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -    ��Name$
 Role��$��3��$��5q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ $��1���� $��Смена:��$��4��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $ Smena��$��4��$��5q�$��30��$��3q�#��[ $��1���� $��Телефон:��$��5��$��0��$��Center=�$��5q�$��15(�$
��Bold"�$��Yu Gothic UI)��� -   $
 Phone��$��5��$��5q�$��30��$��3q�#��[  $��1���� -   $��7��$��Добавить��$��5q�$��2��