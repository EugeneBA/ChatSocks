   M S B A M L   `   `   `  ����D  @ChatSocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ChatSocks	ChatSocks  5   	   5clr-namespace:ChatSocks.customsChatSocks.customs  5   	       ChatSocks.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ChatSocks     5   	   3customsclr-namespace:ChatSocks.customs     5   	     � Title$  
MainWindow��5	   	   $	��600��6   $
��1000��6)    � WindowStyle$
 None=�66    � AllowsTransparency . 
������  � 
ResizeMode$ NoResize=�6|   .��5
      ��  �� CornerRadius$ 15��6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF64307A��6    � Offset$ 0Q�61   � 5      $ 	#FF9C24CD��6   $ 1Q�61   5      ._�5   
   � .+�5      �� $ 15��6   $	��300��6'   ���	#FF571B65$
��Left=�6J   ._�5      �� -   5        $��AccountStack��5      $
��Left=�61   $	��300��6M   .+�5      �� $
��CHAT��6   $��Roboto)�6+   $
��Bold"�6?   $��14(�6Q   
�������$��Center=�6r   $��
0,20,20,10q�6�   (   "ChatSocks.customs.addAccountCustom  5      (   "ChatSocks.customs.addAccountCustom	   	MouseDown$ 	 addAccountCustom_MouseDown��6G   $��40��6/   $	��290��6;   6G   5      5      �� 5      -   5        $��
messageBox��5      $��Right=�6+   $	��700��6G   5      �� 5      -   5        
 ��Name$
 openSettings��5      $��20��6*   $��20��66   $��943,8,25,560q�6A   $��art/cog-1.png��6W   6n   $   ChatSocks.customs.configCustom  5      -   5          Name$ settingsControl��5   #   $	��245��6<   $��Right=�6H   $	��220��6d   $��
0,25,5,355q�6q    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      ��  �� ScaleX$	 0.8Q�6)    �� ScaleY$	 0.8Q�66   �� 5       �� 5!      T� 5"      5#      5%      "   ChatSocks.customs.composeBox  5&      -   5          Name$ messageComposer��5&   !   $��Center=�6:   $��40��6W   $	��350��6c   $��
60,0,0,-40q�6p   $��Bottom=�6�   5'      5(      5)      