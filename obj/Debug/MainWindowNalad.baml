   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp4WpfApp4  5   	       WpfApp4.MainWindowNalad   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp4     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  MainWindowNalad��5	   	   $
��1080��6!   $
��1920��6/    � WindowStartupLocation$ CenterScreen=�6<    � WindowState$ 	Maximized=�6a    � 	Resources 5
         WpfApp4.Lab_RezDataSet   lab_RezDataSet  test_tableViewSource �� Source �� Source��%[   &	        &	      7     5   
   6   �� 5   
    6<   �� *
test_table+8   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $��* �6   .+�5   
   �� ���#f0f0f0$��0��6*   .+�5      �� -   5        $$��🏠 Мониторинг ТО��5      $��16(�60   $��10q�6>   $��10q�6J   $��45��6W   $��Stretch=�5      63   �� 5      -   5        $.��(📈 Мониторинг поломок��5      $��13(�65   $��10q�6C   $��10q�6O   $��45��6\   $��Stretch=�5   	   6'   �� 5%      $��❓ Помощь��6   $��16(�6(   $��10q�66   $��10q�6B   $��45��6O   $y�Hidden=�6[   $��Stretch=�5&      5'      �� 5*   
   -   5         ��Name$ MainContent��5*      $��1��6.   $y�Visible=�6>   .��5+      �  � RowDefinitions 5,      �� 5-      $��70 �6$   �� 5.      $
��Auto �6$   �� 5/      $
��Auto �6$   �� 50      $
��Auto �6$   .+�52      �� $��0��6   $3��-Мониторинг нарядов на ТО��6)   $��Center=�6I   $��10,0,0,0q�6d   $��24(�6v   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 53      $��Дата начала:��6   $
��Left=�60   $��Center=�6K   $��1��6f   $��	10,0,10,0q�6s   (  "System.Windows.Controls.DatePicker  54      -   5        $��StartDatePicker��54      $��1��64   $
��Left=�6A   $��Center=�6\   $��90,0,0,0q�6w   $	��100��6�   $��30��6�   $��3q�6�    $  MaterialDesignOutlinedDatePicker#��[ 6�   6�   �� 56      $��Дата конца:��6   $
��Left=�6/   $��Center=�6J   $��1��6e   $��
230,0,10,0q�6r     57      -   5        $��EndDatePicker��57      $��1��62   $
��Left=�6?   $��Center=�6Z   $��	310,0,0,0q�6u   $	��100��6�   $��30��6�   $��3q�6�   #��[ 6�   6�   &   System.Windows.Controls.DataGrid  5;      -   5        	  Name$	 dataGridMonitorNaryad��5;      
   
IsReadOnly
 .    AutoGenerateColumns .  $��10q�6h   $��2��6t   $	��900��6�      Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата ТО��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6>   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *	Date_TO+ �� StringFormat$ {0:dd.MM.yyyy HH:mm}��  5>      $ Номер наряда��6-   $
 auto 6C    6P   �� *ID+  5?      $ Устройство��6-   $
 auto 6A    6N   �� *Device_Name+  5@      $
 ТО��6-   $
 auto 69    6F   �� *Types_TO_Name+  5A      $ Список работ��6-   $
 auto 6C    6P   �� *Types_TO_Work_List+  5B      $ Сотрудник��6-   $
 auto 6@    6M   �� *	Users_FIO+  5C      $0 *Дата начала выполнения��6-   $
 auto 6M    6Z   �� *
Date_Start+$ {0:dd.MM.yyyy HH:mm}��  5D      $8 2Дата завершения выполнения��6-   $
 auto 6Q    6^   �� *
Date_End+$ {0:dd.MM.yyyy HH:mm}��  5E      $ Статус��6-   $
 auto 6=    6J   �� *Status+  5F      $ Запчасти��6-   $
 auto 6?    6L   �� *Sklad_Deteil_ID+  5G      $- 'Количество запчастей��6-   $
 auto 6K    6X   �� *Sklad_Kolich+  5H      $ Документация��6-   $
 auto 6C    6P   �� *Documentation_Name_ID+  5J      $ Комментарий��6-   $
 auto 6B    6O   �� *	Comment+5L      5N      5O      �� 5Q   
   -   5        $ MonitorCrash��5Q      $��1��6/   $y�Hidden=�6?   .��5R      �  5S      �� 5T      $��70 �6$   �� 5U      $
��Auto �6$   �� 5V      $
��Auto �6$   �� 5W      $
��Auto �6$   .+�5Y      �� $��0��6   $B��<Мониторинг поломок оборудования��6)   $��Center=�6P   $��10,0,0,0q�6k   $��24(�6}   $
��Bold"�6�   $��Yu Gothic UI)�6�   �� 5[      -	   5        $��1��5[      $��Создать��6&   $��10,0,0,0q�68   $
��Left=�6J   $��Center=�6e   $	��100��6�   6�     5]      -
   5        $	 dataGridMonitorCrash��5]      
 .  .  $��10q�6g   $��2��6s   $	��900��6�    5^        5_      $ 
Номер��6-   $
 auto 6<    6I   �� *ID+  5`      $( "Дата/Время поломки��6-   $
 auto 6I    6V   �� *
Date_Crash+$ {0:dd.MM.yyyy HH:mm}��  5a      $ ФИО технолога��6-   $
 auto 6D    6Q   �� *
FIO_Tech+  5b      $ Помещение��6-   $
 auto 6@    6M   �� *
Location+  5c      $ Устройство��6-   $
 auto 6A    6N   �� *Device+  5d      $ Описание��6-   $
 auto 6?    6L   �� *	Comment+  5e      $ ФИО наладчика��6-   $
 auto 6D    6Q   �� *	FIO_Nalad+  5f      $( "Дата/Время починки��6-   $
 auto 6I    6V   �� *Date_Complete+$ {0:dd.MM.yyyy HH:mm}��  5g      $) #Выполненные работы��6-   $
 auto 6I    6V   �� *Opisan_Complete+  5h      $ Статус��6-   $
 auto 6=    6J   �� *Status+5j      5l      5m      5o      5p      