   M S B A M L   `   `   `  ����H  DKyrsovaRobota, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:KyrsovaRobotaKyrsovaRobota  5   	       KyrsovaRobota.Window8   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:KyrsovaRobota     5   	     � Title$=  7Запланувати екзамен для групи��5   	   $��448.8��6/   $	��800��6>    � 
ResizeMode$ NoResize=�6J    � WindowStartupLocation$ CenterScreen=�6`   .��5	      � .+�5
   
   �� $��Оберіть групу��6   $
��Left=�6(   $	��Top=�6C   $��	10,10,0,0q�6[   $	��178��6n   �� 5   
   -   5         ��Name$	 Cb1��5      $
��Left=�6    $	��Top=�6;   $	��178��6S   $��	10,41,0,0q�6_   &   System.Windows.Controls.Calendar  5   
   -   5          Name$
 Data��5      $
��Left=�6!   $	��Top=�6<   $��
10,103,0,0q�6T   �� 5   
   $��Оберіть дату��6   $
��Left=�6'   $	��Top=�6B   $��	10,72,0,0q�6Z   $	��178��6m   �� 5   
   $#��Оберіть предмет��6   $
��Left=�6*   $	��Top=�6E   $��
10,267,0,0q�6]   $	��178��6q   �� 5   
   -   5        $	 Cb2��5      $
��Left=�6    $	��Top=�6;   $	��178��6S   $��
10,298,0,0q�6_   �� 5   
   $2��,Введіть номер аудиторії��6   $
��Left=�62   $	��Top=�6M   $��
10,325,0,0q�6e   $	��178��6y   �� 5   
   -   5         ��Name$	 Tb1��5      $
��Left=�6   $��23��6:    �� TextWrapping$
 Wrap=�6F   $	��Top=�6Z   $	��178��6r   $��
10,356,0,0q�6~   �� 5   
   -   5        $+��%Запланувати екзамен��5      $
��Left=�6/   $	��Top=�6J   $	��178��6b   $��
10,384,0,0q�6n   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $	��403��6@   $	��Top=�6M   $	��591��6e   $��	193,0,0,0q�6q   5      5      