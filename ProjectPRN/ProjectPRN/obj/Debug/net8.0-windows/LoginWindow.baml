   M S B A M L   `   `   `  ����E  AProjectPRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ProjectPRN
ProjectPRN  5   	       ProjectPRN.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ProjectPRN     5   	     � Title$  LoginWindow��5   	    � WindowState$ 	Maximized=�6    � WindowStyle$
 None=�65   ���#ECEFF1.��5
      � ��5   
   �� 5       �� ImageSource$= 7/LogoImage/—Pngtree—milk tea pearl milk_1481123.jpg��6    �� Opacity$	 0.1Q�5       j� Stretch$ UniformToFill=�6'   .+�5   
   �� ���#1A237E$��60��6&   $	��Top=�62    �� CornerRadius$ 8,8,0,0��6J   ._�5      � .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   $��Center=�6Q   .+�5      �� $��/LogoImage/logo.png��6   $��40��69   $��40��6D   $��20,0,0,0q�6P   �� 5      $��Milk Tea POS��6    
�������$��20(�6G   $��Segoe UI)�5      $��SemiBold"�65   $��10,0,0,0q�6K   5      �� 5      -   5        $��30��5      $��30��6$   $��Right=�60   $��0,0,20,0q�6L   
������ $��0q�5   2    � Cursor$
 Hand��6c   .��5      �� $��/LogoImage/close.png��6   $��16��6:   $��16��6E   	 J� Opacity$		 0.8Q�6Q   ��5      �� 5      
 �� 
TargetType"
 ��6$   .�5   "   ��  �� Property �@J�Opacity �� Value$	 0.8Q�6<    �� Triggers 5   "   P� 5    &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5!   *   ��  �@J�Opacity$ 1Q�6D   5"   '   5$      5&      5'      5(      5)      �� 5,   
   $��Center=�6   $��Center=�6.   $	��400��6I   
�������$ 8��5-   $    �� Padding$ 30q�65   ���#E0E0E0$��1q�6X   ._�5.      �� .+�50      �� $��/LogoImage/logo.png��6   $��80��65   $��80��6@   $��0,0,0,20q�6L   �� 53      $��Welcome!��6   ���#1A237E$��24(�6A   $��Segoe UI)�54      $��SemiBold"�61   $��Center=�55      $��0,0,0,5q�68   �� 56      $)��#Please login to use the application��6   ���#616161$��14(�57      $��Segoe UI)�6)   $��Center=�58      $��0,0,0,20q�68   �� 5;      $
��0,10q�6   .+�5<      �� $��Username��6    ���#1A237E$��12(�6E   $��Segoe UI)�5=      $��Medium"�65   $��0,0,0,5q�6I   �� 5>      -   5         ��Name$ txtUsername��5>      $��36��63   $��16(�5?      $��Segoe UI)�6+   ���#B0BEC5$��1q�5@   3   
�������6G   5A      �� 5D      $
��0,10q�6   .+�5E      �� $��Password��6    ���#1A237E$��12(�6E   $��Segoe UI)�5F      $��Medium"�65   $��0,0,0,5q�6I   G� 5G      -   5         G�Name$ txtPassword��5G   "   $��36��67   $��16(�5H   !   $��Segoe UI)�6/   ���#B0BEC5$��1q�5I   7   
�������6K   5J      �� 5M      -   5        $��Login��5M      ���#1A237E
�������$��14(�5N      $��Segoe UI)�6'   $��Medium"�6=   $	��200��5O      $��40��6%   $��	0,20,0,10q�61   $��Center=�6D   $��0q�5P   .   $
 Hand��6B   ��5Q      �� 5R      "
 ��6    .�5S      ��  � �� �#1A237E6;    5T      P� 5U   "    �@J�IsMouseOver$
 True��6A   . �5V   &   ��  � �� �#2835936C   5W   #   5Y      5[      �� 5^      -   5        $��Forgot Password?��5^      ���#1A237E$��12(�6I   $��Segoe UI)�5_      $��Medium"�62   $��	Underline{�6F   $��Center=�5`      $
 Hand��69   6G   5a      5b      5c      5d      