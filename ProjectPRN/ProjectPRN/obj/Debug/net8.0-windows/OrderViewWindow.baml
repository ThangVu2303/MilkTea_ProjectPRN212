   M S B A M L   `   `   `  ����E  AProjectPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectPRN
ProjectPRN  5   	        ProjectPRN.OrderViewWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectPRN     5   	     � Title$  OrderViewWindow��5   	    � WindowState$ 	Maximized=�6!    � WindowStyle$
 None=�69   .��5	      �  � RowDefinitions 5
   
   �� 5      $��70 �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   ���#007ACC$��70��67   $	��Top=�6C   .+�5      �� $
��Left=�6   $��/LogoImage/logo.png��6*   $��46��6G   $��45��6R   $��10q�6^   $
��Left=�6j   �� 5      $
��Left=�6   $��Milk Tea��6*   
�������$��28(�6P   $
��Bold"�6^   $��Center=�6p   $
��Left=�6�   �� 5      -   5        $��Right=�5      $��/LogoImage/close.png��6+   $��30��6I   $��30��6T   $��10q�6`    � Cursor$
 Hand��6l   $��Right=�6z   $��Center=�6�   6�   �� 5      -   5         ��Name$ txtUsername��5   +   $��Right=�6   
�������$��16(�6T   $��Right=�6b   $��Center=�6~   $��10q�6�   �� 5      -   5        $ txtTime��5   +   $��Right=�6   $��HH:MM:SS DD-MM-YYYY��6<   
�������$��16(�6m   $
��Bold"�6{   $��Center=�6�   o� 5      $��Right=�6   $��16(�6*   $
��Bold"�68   
������ 
�������$��Center=�6v   $��Center=�6�   .@�5      m� -   5        $��Back to Order��5      62   5      5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5       �� 5!      .+�5#      �� -   5         ��Name$ lvOrders��5#      $��0��6*   $��10q�6g    �� View 5$      �� 5%      .P�5&      �� $��Order ID��6)   	 �� DisplayMemberBinding	 6;   �� *	OrderId+�� 5'      $��
Staff Name��6)   	 6=   �� *Staff.Account.FullName+�� 5(      $��Customer Name��6)   	 6@   �� *Customer.Account.FullName+�� 5)      $��Total��6)   	 68   �� *Total+�� 5*      $��Point��6)   	 68   �� *Point+�� 5+      $
��Date��6)   	 67   �� *
DateCreate+
 �� StringFormat$
 
dd/MM/yyyy��5,      5.      �� 51      -   5        $ lvOrderDetails��51      $��1��6/   $��10q�6?   ��52      �� 53       55      �� 56      .P�57      �� $��Order ID��6)   	 6;   �� *	OrderId+�� 58      $��Product Name��6)   	 6?   �� *Product.ProductName+�� 59      $��Image��6)    �� Width$	 100��68    �� CellTemplate 5:      �� 5;   "   .R�5<   &   �� $��80��6-   $��80��68   ��6D   �� *Product.Image+5=   #   5?      �� 5@      $��Quantity��6)   	 6;   �� *
Quantity+�� 5A      $��Price��6)   	 68   �� *Price+5B      5D      5E      5H      5I      