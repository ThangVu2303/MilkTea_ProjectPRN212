   M S B A M L   `   `   `  ����E  AProjectPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectPRN
ProjectPRN  5   	       ProjectPRN.AdminEmployee   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectPRN     5   	   -   5          N� Title$  Employee Management��5   	    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value&   System.Windows.Controls.DataGrid��%�  '��       '�� �    '�� m    '      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6(   �� 5       � ��$ 1q�60   �� 5       � �� �#CCCCCC6,   �� 5       � ��
 �����6+   �� 5       � ��$ Center=�69   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 3��5   !   ._�5      �� $ PART_ContentHost��6+   5      5      5      5      �� 5"   
   " ��6   .�5#      ��  � ��$ 5q�6(   �� 5$       � ��$ 1q�60   �� 5%       � �� �#CCCCCC6,   �� 5&       � ��
 �����6+   5'      �� 5*   
   " ��6   .�5+      ��  � �� �#4A90E26+   �� 5,       � ��
 �����6+   �� 5-       � ��$
 10,5q�6(   �� 5.       � ��$ 5,0,5,0q�6'   �� 5/       � ��$ 0q�60   �� 50       � ��$ 14(�6)   �� 51       � �� 52      �� 53      " ��6&   .R�54      �� #��z��6!   $ 5��55   !   #��z��56   !   ._�57      �� $��Center=�6/   $��Center=�6L   58       59      P� 5:      	 �@J�IsMouseOver$

 True��6=   . �5;   "   ��  � �� �#357ABD6?   5<      P� 5=      	 � ��$

 True��6;   . �5>   "   ��  � �� �#2A60996?   5?      5A      5C      5D      �� 5G   
   "  6   .�5H      ��  � ��
 �����6+   �� 5I       � �� �#E0E0E06,   �� 5J       � ��$ 1q�60   �� 5K       �@ RowBackground �#FFFFFF6.   �� 5L        �@ AlternatingRowBackground �#F9F9F969   �� 5M       �@ HeadersVisibility$ Column=�62   5N      5O       N� 
Background 5P      �� 5Q   
    �� ImageSource$= 7/LogoImage/—Pngtree—milk tea pearl milk_1481123.jpg��6   .��5T      � $��20q�6    � RowDefinitions 5U   
   �� 5V      $
��Auto �6   �� 5W      $��* �6   �� 5X      $
��Auto �6   .+�5\   
   �� $��0��6   
�������$ 8��5^      ���#E0E0E0$��1q�5`       �� Padding$ 15q�5a      $��0,0,0,20q�5b      ._�5c      �  � ColumnDefinitions 5d      �� 5e      $
��Auto �6'   �� 5f      $��* �6'   �� 5g      $
��Auto �6'   �� 5h      $��* �6'    5j      �� 5k      $
��Auto �6$   �� 5l      $
��Auto �6$   �� 5m      $
��Auto �6$   �� 5n      $
��Auto �6$   �� 5o      $
��Auto �6$   �� 5p      $
��Auto �6$   .+�5s      �� $��0��6   $��0��6%   $��User ID:��65   $��14(�6H   ���#3333336V   �� 5t      -   5         ��Name$ 	txtUserId��5t      $��0��6-   $��1��6:   $��5q�6J   $	��250��6U    �� 
IsReadOnly$
 True��6a   �� 5v      $��1��6   $��0��6%   $��	Username:��65   $��14(�6I   ���#3333336W   �� 5w      -   5        $ txtUsername��5w      $��1��6/   $��1��6<   $��5q�6L   $	��250��6W   �� 5y      $��2��6   $��0��6%   $��	Password:��65   $��14(�6I   ���#3333336W   �� 5z      -   5        $ txtPassword��5z      $��2��6/   $��1��6<   $��5q�6L   $	��250��6W   �� 5|      $��3��6   $��0��6%   $��Role:��65   $��14(�6E   ���#3333336S   �� 5}      -   5         ��Name$ 	cboRoleId��5}      $��3��6.   $��1��6;   $��5q�6K   $	��250��6V    �� DisplayMemberPath$ RoleName��5~       �� SelectedValuePath$ RoleId��68   �� 5�      $��4��6   $��0��6%   $��
Full Name:��65   $��14(�6J   ���#3333336X   �� 5�      -   5        $ txtFullName��5�      $��4��6/   $��1��6<   $��5q�6L   $	��250��6W   �� 5�      $��5��6   $��0��6%   $��Phone:��65   $��14(�6F   ���#3333336T   �� 5�      -   5        $ txtPhone��5�      $��5��6,   $��1��69   $��5q�6I   $	��250��6T   5�      5�      �� 5�   
   $��1��6   
�������$ 8��5�      ���#E0E0E0$��1q�5�      $��0,0,0,20q�5�      ._�5�        -   5          Name$ dataGridView1��5�         AutoGenerateColumns$ False��5�         
IsReadOnly$
 True��5�      $��10q�5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ User ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *	AccountId+6X     5�      $ Username��6)   $ * 6X    6;   �� *
Username+6X     5�      $ Password��6)   $ * 6X    6;   �� *
Password+6X     5�      $ Role ID��6)   $
 Auto 6U    6:   �� *RoleId+6U     5�      $ 	Full Name��6)   $ * 6Y    6<   �� *
FullName+6Y     5�      $ Phone��6)   $ * 6R    68   �� *Phone+6R   5�      5�      �� 5�   
   $��2��6    �� Orientation$ 
Horizontal=�5�      $��Right=�5�      $��0,10,0,0q�5�      .+�5�      �� -	   5        $	��Add��5�      $	��100��6#   6/   �� 5�      -
   5        $��Update��5�      $	��100��6&   62   �� 5�      -   5        $��Delete��5�      $	��100��6&   ���#E74C3C6J   �� 5�      -   5        $��Cancel��5�      $	��100��6&   ���#95A5A66J   5�      5�      5�      