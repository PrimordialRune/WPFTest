   M S B A M L   `   `   `  ����@  <Games, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Games.ViewsGames.Views  5   	   3clr-namespace:Games.ViewModelsGames.ViewModels  5   	   #    Games.Views.AddGameWindowView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Games.Views     5   	   -vmclr-namespace:Games.ViewModels     5   	     � Title$  AddGameWindowView��5	   	   $	��200��6#   $	��400��60   .��5
      �� .+�5   
   � $��8q�6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� $��0��6   $��
Game Name:��6%   �� 5      -   5         ��Name$ _addGame��5      $��0��6(   $��1��65   ��6E   ��  �� Path$ SelectedItem.Name��� 5      $��1��6   $��Console:��6%   �� 5      -   5         ��Name$ _addConsole��5      $��1��6,   $��1��6<   $
��auto��6I   $��Right=�6V    �� SelectedItem 6r   �� $ SelectedPlatform���6�   �� $ Consoles���5      �� 5      .R�5      �� ��6$   �� $
 Name�5      5       �� 5!      $��2��6   $��Release Date:��6%   (  "System.Windows.Controls.DatePicker  5"      $��2��6   $��1��6&      Text 66   �� $ SelectedItem.ReleaseDate� �� UpdateSourceTrigger$ PropertyChanged=�5#      �� 5$   
   	 �� Orientation$	 
Horizontal=�6   $��Center=�6.   .+�5%      �� -   5        
 ��Name$
 _addGameBtn��5%      $��Aggiungi��6K   ��6*   �� $ AddGame�6K   5&      5'      5(      