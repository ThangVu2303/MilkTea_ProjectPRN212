   M S B A M L   `   `   `  ����E  AProjectPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectPRN
ProjectPRN  5   	   %    ProjectPRN.CreateCustomerWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectPRN     5   	     � Title$  CreateCustomerWindow��5   	    � WindowState$ 	Maximized=�6&    � WindowStyle$
 None=�6>   .��5	      � .+�5
   
   � ��5      �� 5       �� ImageSource$= 7/LogoImage/—Pngtree—milk tea pearl milk_1481123.jpg��6    � RowDefinitions 5      �� 5      $��70 �6    �� 5      $��* �6    .+�5      ]� $��0��6   ���#007ACC$��70��6;   $	��Top=�6G   .+�5      �� $
��Left=�6   $��/LogoImage/logo.png��6.   $��46��6K   $��45��6V   $��10q�6b   $
��Left=�6n   �� 5      $
��Left=�6   $��Milk Tea��6.   
�������$��28(�6T   $
��Bold"�6b   $��Center=�6t   $
��Left=�6�   �� 5      -   5        $��Right=�5      $��/LogoImage/close.png��6/   $��30��6M   $��30��6X   $��10q�6d    � Cursor$
 Hand��6p   $��Right=�6~   $��Center=�6�   6�   �� 5      -   5         ��Name$ txtUsername��5   /   $��Right=�6   
�������$��16(�6X   $��Right=�6f   $��Center=�6�   $��10q�6�   �� 5      -   5        $ txtTime��5   /   $��Right=�6   $��HH:MM:SS DD-MM-YYYY��6@   
�������$��16(�6q   $
��Bold"�6   $��Center=�6�   $��Center=�6�   5      �� 5      -   5         ��Name$ CustomerForm��5   /   $��1��5      $��20q�6"    �� Padding$ 10q�6.   
�������$��1q�6N   	 �� CornerRadius$	 5��5   	   ���#F5F5F5$��Center=�6E   $��Center=�6b   ._�5      �� .+�5      �� $��Create New Customer��6   $��18(�6:   $
��Bold"�6H   �� 5!      
 �� Orientation$
 
Horizontal=�6!   $��0,5,0,5q�6:   .+�5"      �� $��
Full Name:��6    $	��100��65   �� 5#      -   5         ��Name$ txtFullName��5#   "   $	��250��67   $��25��6C   5$      �� 5&      $
 
Horizontal=�6!   $��0,5,0,5q�6:   .+�5'      �� $��	Username:��6    $	��100��64   �� 5(      -   5        $ txtUser��5(   "   $	��250��63   $��25��6?   5)      �� 5+      $
 
Horizontal=�6!   $��0,5,0,5q�6:   .+�5,      �� $��	Password:��6    $	��100��64   �� 5-      -   5        $ txtPassword��5-   "   $	��250��67   $��25��6C   5.      �� 51      $
 
Horizontal=�6!   $��0,5,0,5q�6:   .+�52      �� $��Phone:��6    $	��100��61   �� 53      -   5        $ txtPhone��53   "   $	��250��64   $��25��6@   54      �� 57      $
 
Horizontal=�6!   $��0,5,0,5q�6:   .+�58      �� $��Points:��6    $	��100��62   �� 59      -	   5        $ 	txtPoints��59   "   $	��250��65   $��25��6A   5:      �� 5=      $
 
Horizontal=�6!   $��
10,10,0,10q�6:   .+�5>      �� -
   5        $��Create��5>   !   $��10q�62   $��10,0,0,0q�6?   6Q   �� 5?      -   5        $��Cancel��5?   !   $��10q�62   $��10,0,0,0q�6?   6Q   5@      5A      5B      5C      5D      5E      