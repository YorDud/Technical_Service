   M S B A M L   `   `   `  ����   WpfApp44    .WpfApp4.MiniWindows.Nomekluatura_Create_Window   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$1  +Номенклатура (создание)��$	��468��$	��933��.��� $��10q� � RowDefinitions �� $��40 ��� $��* �.+���  �� Orientation$ 
Horizontal=�$��0��.+��� $(��"Записать и закрыть��$��0,0,5,0q�
��� ���
���   ��� $��Записать��$��0,0,5,0q��� $>��8Печать присвоенного штрихкода��$��0,0,5,0q��� $'��!Изменить штрихкод��$��0,0,5,0q��� $2��,Печать штрихкода старый��$��0,0,5,0q�� $��1��$��0,10,0,0q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��Код:��$��0��$��0��$��Center=�$	��0,5q��� $��0��$��1��$��	0,5,548,5q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  �� $��Наименование:��$��1��$��0��$��Center=�$	��0,5q��� $��1��$��1��$��	0,5,246,5q�$��30��$��3q�#��[  �� $��Артикул:��$��2��$��0��$��Center=�$	��0,5q��� $��2��$��1��$��	0,5,246,5q�$��30��$��3q�#��[  �� $,��&Полное наименование:��$��3��$��0��$��Center=�$	��0,5q��� $��3��$��1��$��	0,5,246,5q�$��30��$��3q�#��[  �� $7��1Базовая единица измерения:��$��4��$��0��$��Center=�$	��0,5q��� $��4��$��1��$��	0,0,246,0q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ �� $��Услуга:��$��5��$��0��$��Center=�$	��0,5q��� $��5��$��1��$��	0,5,246,5q��� $.��(Критическое значение:��$��6��$��0��$��Center=�$	��0,5q��� $��6��$��1��$��	0,5,540,5q�$��0�� J� RenderTransformOrigin$ 0.5,0.5&�$��30��$��3q�#��[  �� $��Группа:��$��7��$��0��$��Center=�$	��0,5q��� $��7��$��1��$��	0,0,246,0q�$��30��$��3q�#��[ �� $;��5Найти номенклатуру на складе��$
��Left=�$��0,10,0,0q�$��8��