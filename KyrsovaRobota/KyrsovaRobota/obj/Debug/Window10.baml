   M S B A M L   `   `   `  ����H  DKyrsovaRobota, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:KyrsovaRobotaKyrsovaRobota  5   	       KyrsovaRobota.Window10   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:KyrsovaRobota     5   	     � Title$,  &Звіт про зарахованих��5   	   $	��450��6&   $	��800��63    � 
ResizeMode$ NoResize=�6?    � WindowStartupLocation$ CenterScreen=�6U   .��5	      � .+�5
   
   �� $'��!Оберіть факультет��6   $
��Left=�6,   $	��Top=�6G   $��	10,10,0,0q�6_   �� 5   
   -   5         ��Name$	 Cb1��5      $
��Left=�6    $	��Top=�6;   $	��110��6S   $��	10,41,0,0q�6_   6r   �� 5   
   $#��Оберіть кафедру��6   $
��Left=�6*   $	��Top=�6E   $��	10,68,0,0q�6]   $	��110��6p   �� 5   
   -   5        $	 Cb2��5      $
��Left=�6    $	��Top=�6;   $	��110��6S   $��	10,99,0,0q�6_   6r   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $	��400��6@   $	��Top=�6M   $	��491��6e   $��
293,10,0,0q�6q   �� 5   
   -   5         ��Name$	 Lb1��5      $:��4Прохідний бал по факультету:��6   $
��Left=�6D   $	��Top=�6_   $��
10,192,0,0q�6w   �� 5   
   -   5        $	 Lb2��5      $P��JК-сть студентів, що пройшли на факультет:��6   $
��Left=�6Q   $	��Top=�6l   $��
10,223,0,0q�6�   $	��278��6�   �� 5   
   -   5        $	 Lb3��5      $G��AПрохідний бал на інститут в цілому: ��6   $
��Left=�6L   $	��Top=�6g   $��
10,254,0,0q�6   $	��278��6�   �� 5   
   -   5        $	 Lb4��5      $X��RЗагальна к-ть студентів вступила в інститут: ��6   $
��Left=�6U   $	��Top=�6p   $��
10,285,0,0q�6�   $	��278��6�   �� 5   
   -   5        $	 Cb3��5      $
��Left=�6    $	��Top=�6;   $	��120��6S   $��
10,165,0,0q�6_   6s   �� 5   
   $#��Оберіть предмет��6   $
��Left=�6*   $	��Top=�6E   $��
10,134,0,0q�6]   5      5      