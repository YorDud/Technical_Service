   M S B A M L   `   `   `  ����   WpfApp4+    %WpfApp4.MiniWindows.NarTOCreateWindow   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     � Title$-  'Наряд на ТО (создание)��$	��838��$
��1668��.��� .+��� .+���  �� Orientation$ 
Horizontal=�$��10q�.+��� $>��8Печать присвоенного штрихкода��$��0,0,10,0q��� $'��!Изменить штрихкод��$��0,0,10,0q��� $2��,Печать штрихкода старый��� $��10q�$	��544��$
��Left=� � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Номер:��$��Center=�$	��0,5q��� $��1��$��0��$��6q�$��30��$��3q� !  MaterialDesignOutlinedTextBox#��[  �� $��	Дата:��$��1��$��Center=�$	��0,5q�(  "System.Windows.Controls.DatePicker  $��1��$��1��$��6q�$��30��$��3q� $  MaterialDesignOutlinedDatePicker#��[ �� $)��#Дата проведения ТО:��$��2��$��Center=�$	��0,5q�  $��1��$��2��$��6q�$��30��$��3q�#��[ �� $��Оборудование:��$��3��$��Center=�$	��0,5q��� $��1��$��3��$��6q�$��30��$��3q� " MaterialDesignOutlinedComboBox#��[ �� $��Плановое ТО:��$��4��$��Center=�$	��0,5q��� $��1��$��4��$��6q�$��30��$��3q�#��[ �� $&�� Состояние наряда:��$��5��$��Center=�$	��0,5q��� $��1��$��5��$��6q�$��30��$��3q�#��[ �� $6��0Дата начала выполнения ТО:��$��6��$��Center=�$	��0,5q�  $��1��$��6��$��6q�$��30��$��3q�#��[ �� $<��6Дата окончания выполнения ТО:��$��7��$��Center=�$	��0,5q�  $��1��$��7��$��6q�$��30��$��3q�#��[ �� $%��Дата закрытия ТО:��$��8��$��Center=�$	��0,5q�  $��1��$��8��$��6q�$��30��$��3q�#��[ �� $��Скрыть:��$��9��$��Center=�$	��0,5q��� $��1��$��9��$��6q��� $��10q�$
��Left=�$
��Left=�.@��� $!��Перечень работ��.��� $
��1626��$
��Left=� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Добавить��$��0��$��10,10,1513,0q�&   System.Windows.Controls.DataGrid  $��1��$	��200��$
��auto��$��10q��� $%��Требует запчасти���� $��Наладчики���� $��Комментарий���� $)��#Документы списания���� $1��+Параметры оборудования���� $��История���� $'��!Предыдущие наряды���� $ 
Horizontal=�$
��Left=�$��Bottom=�$��10q�.+��� $��⇓ Записать��$��5q��� $#��☑ Закрыть наряд��$��5q��� $/��)☓ Отменить проведение��$��5q��� $.��(▶️ Начать выполнение��$��5q��� $1��+✔ Закончить выполнение��$��5q��� $)��#⎙ Печать документа��$��5q��� $+��%✎ Изменить документ��$��5q��� $6��0⎙ Печать документа старый��$��5q�