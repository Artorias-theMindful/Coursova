   M S B A M L   `   `   `  ����H  DKyrsovaRobota, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:KyrsovaRobotaKyrsovaRobota  5   	       KyrsovaRobota.Window7   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:KyrsovaRobota     5   	     � Title$I  CЗапланувати консультацію для потоку��5   	   $��442.8��65   $	��800��6D    � WindowStartupLocation$ CenterScreen=�6P    � 
ResizeMode$ NoResize=�6u   .��5	      � .+�5
   
   �� $#��Оберіть кафедру��6   $
��Left=�6*   $	��Top=�6E   $	��178��6]   $��	10,15,0,0q�6i   �� 5   
   -   5         ��Name$	 Cb1��5      $
��Left=�6    $	��Top=�6;   $	��178��6S   $��	10,41,0,0q�6_   6r   �� 5   
   $��Оберіть дату��6   $
��Left=�6'   $	��Top=�6B   $	��178��6Z   $��	10,68,0,0q�6f   &   System.Windows.Controls.Calendar  5   
   -   5          Name$
 Data��5      $
��Left=�6!   $	��Top=�6<   $��	10,90,0,0q�6T   �� 5   
   $#��Оберіть предмет��6   $
��Left=�6*   $	��Top=�6E   $	��178��6]   $��
10,260,0,0q�6i   �� 5   
   -   5        $5��/Запланувати консультацію��5      $
��Left=�64   $	��Top=�6O   $	��178��6g   $��
10,375,0,0q�6s   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $	��384��6@   $	��Top=�6M   $	��591��6e   $��
193,10,0,0q�6q   �� 5   
   $2��,Введіть номер аудиторії��6   $
��Left=�62   $	��Top=�6M   $	��178��6e   $��
10,321,0,0q�6q   �� 5   
   -   5         ��Name$	 Tb1��5      $
��Left=�6   $��23��6:    �� TextWrapping$
 Wrap=�6F   $	��Top=�6Z   $	��178��6r   $��
10,347,0,0q�6~   �� 5   
   -   5        $	 Cb2��5      $
��Left=�6    $	��Top=�6;   $	��178��6S   $��
10,294,0,0q�6_   5      5      