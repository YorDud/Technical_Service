   M S B A M L   `   `   `  ����   WpfApp40    *WpfApp4.MiniWindows.Types_TO_Create_Window   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$H  BТехническое обслуживание (создание)��$	��601��$	��600��.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��0��$��0,0,0,10q�.+��� $(��"Записать и закрыть��
��� ���$��0,0,10,0q�
���   ��� $��Записать���� $��1��.+�� $��0,0,0,10q� � ColumnDefinitions �� $
��Auto ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Код:��$��Center=�$	��0,5q��� $��1��$��0��$��	5,2,276,2q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  �� $��Наименование:��$��1��$��Center=�$	��0,5q��� $��1��$��1��$	��5,2q�$��30��$��3q�#��[  �� $&�� Тип оборудования:��$��2��$��Center=�$	��0,5q��� $��1��$��2��$	��5,2q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ �� $��Расписание:��$��3��$��Center=�$	��0,5q��� $��1��$��3��$	��5,2q�$��30��$��3q�#��[  �� $&�� Порядковый номер:��$��4��$��Center=�$	��0,5q��� $��1��$��4��$	��5,2q�$��1��$��30��$��3q�#��[  �� $��Описание:��$��5��$��Center=�$	��0,5q��� $��1��$��5��$	��5,2q� �� AcceptsReturn . $��30��$��3q�#��[  �� $"��Перечень работ:��$��0,0,0,10q��� $ 
Horizontal=�$��0,0,0,10q�.+��� $��Добавить��$��0,0,5,0q�&   System.Windows.Controls.DataGrid     AutoGenerateColumns .     HeadersVisibility$ Column=�$	��239��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ N��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 50   $ Наименование��$	 * 